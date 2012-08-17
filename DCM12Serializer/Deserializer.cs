/*
 * Copyright (c) 2006-2012 Francesco De Vittori, Board International SA
 *
 * Distributed under the MIT license.
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DMC12Serializer
{
    /// <summary>
    /// Hydrates an Universal Data Type serialized with the VB6 Put method.
    /// </summary>
    internal class Deserializer
    {
        internal object Deserialize(Type targetType, BinaryReader reader)
        {
            return Deserialize(targetType, -1, reader);
        }

        private object Deserialize(Type targetType, int constantStringLength, BinaryReader reader)
        {
            return Deserialize(targetType, ArrayDefinition.Default, constantStringLength, reader);
        }

        private object Deserialize(Type targetType, ArrayDefinition arrayDefinition, int constantStringLength, BinaryReader reader)
        {
            if (targetType.IsArray && targetType.GetArrayRank() == 1)
            {
                // a 1-dimensional array
                return ReadArray(targetType.GetElementType(), arrayDefinition, constantStringLength, reader);
            }
            else if (targetType.IsArray && targetType.GetArrayRank() == 2)
            {
                // a 2-dimensional ([,]) array
                return ReadArray2(targetType.GetElementType(), constantStringLength, reader);
            }
            else if (targetType.IsSimpleType())
            {
                // a simple type, including String and Object.
                return HydrateSimpleObject(targetType, constantStringLength, reader);
            }
            else
            {
                // a complex object
                return HydrateStructure(targetType, reader);
            }
        }

        private object HydrateSimpleObject(Type targetType, int constantStringLength, BinaryReader reader)
        {
            if (targetType == typeof(byte))
                return ReadByte(reader);
            else if (targetType == typeof(bool))
                return ReadBool(reader);
            else if (targetType == typeof(byte))
                return ReadByte(reader);
            else if (targetType == typeof(Single))
                return ReadFloat(reader);
            else if (targetType == typeof(Int16))
                return ReadShort(reader);
            else if (targetType == typeof(Int32))
                return ReadInt(reader);
            else if (targetType == typeof(double))
                return ReadDouble(reader);
            else if (targetType == typeof(string))
                return constantStringLength >= 0 ? ReadCString(constantStringLength, reader) : ReadString(reader);
            else if (targetType == typeof(DateTime))
                return ReadDate(reader);
            else if (targetType == typeof(Object))
                return ReadObject(reader);
            else
                throw new SerializationException(string.Format("Type '{0}' is not supported.", targetType.Name));
        }

        private object HydrateStructure(Type targetType, BinaryReader reader)
        {
            if (targetType.IsSystemClass())
                throw new SerializationException(string.Format("Type '{0}' is not supported.", targetType.Name));

            var hydrated = targetType.CreateInstance();

            foreach (var property in targetType.GetProperties())
            {
                if (property.GetSetMethod() == null || property.MustIgnore())
                    continue;

                var arrayDefinition = property.GetArrayDefinition();
                var constantStringLength = property.GetConstantLengthString();

                var value = Deserialize(property.PropertyType, arrayDefinition, constantStringLength, reader);
                property.SetValue(hydrated, value, null);
            }
            return hydrated;
        }

        #region Arrays

        private Array ReadArray(Type elementType, ArrayDefinition arrayDefinition, int constantStringLength, BinaryReader reader)
        {
            var len = 0;

            if (arrayDefinition.IsFixedSize)
            {
                len = arrayDefinition.FixedLength;
            }
            else
            {
                var d = ReadArrayRank(reader);

                // null arrays are just 00 00 (2 bytes)
                if (d == 0)
                    return null;

                if (d != 1)
                    throw new SerializationException(string.Format("Expected a one-dimensional array, but found a {0}-dimensional one", d));

                len = ReadArrayLength(reader);
            }

            return ReadFixedArray(elementType, len, constantStringLength, reader);
        }

        private Array ReadFixedArray(Type elementType, int length, int constantStringLength, BinaryReader reader)
        {
            var values = Array.CreateInstance(elementType, length);

            for (var i = 0; i < length; i++)
                values.SetValue(Deserialize(elementType, constantStringLength, reader), i);

            return values;
        }

        private Array ReadArray2(Type elementType, int constantStringLength, BinaryReader reader)
        {
            var d = ReadArrayRank(reader);

            // null arrays are just 00 00 (2 bytes)
            if (d == 0)
                return null;

            if (d != 2)
                throw new SerializationException(
                    string.Format("Two-dimensional array expected, but found a {0}-dimensional one.", d));

            var j = ReadArrayLength(reader);
            var i = ReadArrayLength(reader);

            var values = Array.CreateInstance(elementType, i, j);
            for (var jj = 0; jj < j; jj++)
            {
                for (var ii = 0; ii < i; ii++)
                    values.SetValue(Deserialize(elementType, constantStringLength, reader), ii, jj);
            }
            return values;
        }

        private short ReadArrayRank(BinaryReader reader)
        {
            // first part of the array header: 2 bytes for the rank
            return reader.ReadInt16();
        }

        private int ReadArrayLength(BinaryReader reader)
        {
            // second part of the array header:

            // 4 bytes for the length
            var len = reader.ReadInt32();

            // 4 bytes for the start index (unused in .net)
            reader.ReadBytes(4);

            return len;
        }

        #endregion

        #region Simple Types

        private byte ReadByte(BinaryReader reader)
        {
            return reader.ReadByte();
        }

        private bool ReadBool(BinaryReader reader)
        {
            // .NET bool, VB6 Boolean: 32 / 16 bits
            // VB6: true=-1 (0xFFFF), false=0  .NET: true=1, false=0
            reader.ReadByte();
            return (reader.ReadByte() != 0);
        }

        private float ReadFloat(BinaryReader reader)
        {
            // .NET float, VB6 Single: 32 bits
            return reader.ReadSingle();
        }

        private short ReadShort(BinaryReader reader)
        {
            // .NET short, VB6 integer: 16 bits
            return reader.ReadInt16();
        }

        private int ReadInt(BinaryReader reader)
        {
            // .NET int, VB6 long: 32 bits
            return reader.ReadInt32();
        }

        private double ReadDouble(BinaryReader reader)
        {
            // 64 bits
            return reader.ReadDouble();
        }

        private string ReadString(BinaryReader reader)
        {
            // 16 bits for the string length
            // 32 bits/char in .NET, 16 bits for ASCII in VB6, 32 bits for Unicode in VB6.

            var len = reader.ReadInt16();

            if (len < 0)
                throw new SerializationException(
                    string.Format("Attempting to read {0} chars at position {1}", len, reader.BaseStream.Position));
            
            // there's no difference between null and empty strings
            if (len == 0)
                return string.Empty;

            return new string(reader.ReadChars(len));
        }

        private string ReadCString(int len, BinaryReader reader)
        {
            // constant-length string:
            // 32 bits/char in .NET, 16 bits for ASCII in VB6, 32 bits for Unicode in VB6.
            return new string(reader.ReadChars(len));
        }

        private DateTime ReadDate(BinaryReader reader)
        {
            // 64 bits, as a double.
            // Integral part represents days from Jan 1, 100.
            // Decimal part represents the fraction of the day.

            var d = reader.ReadDouble();
            var ts = TimeSpan.FromDays(d);
            var dt = new DateTime(1899, 12, 30);
            return dt.Add(ts);
        }

        private object ReadObject(BinaryReader reader)
        {
            // .NET object, VB6 Variant.
            // 16 bytes in memory, here seems to be just 2 bytes of datatype, followed by the content.

            var variantType = ReadShort(reader);
            switch ((Variants)variantType)
            {
                case Variants.VbEmpty:
                    return null;
                case Variants.VbNull:
                    return null;
                case Variants.VbInteger:
                    return reader.ReadInt16();
                case Variants.VbLong:
                    return reader.ReadInt32();
                case Variants.VbSingle:
                    return reader.ReadSingle();
                case Variants.VbDouble:
                    return reader.ReadDouble();
                case Variants.VbString:
                    return reader.ReadString();
                case Variants.VbBoolean:
                    return reader.ReadBoolean();
                case Variants.VbByte:
                    return reader.ReadByte();
                case Variants.VbComplex:
                    throw new SerializationException("Complex objects inside variants are not supported.");
                case Variants.VbArray:
                    throw new SerializationException("Arrays inside variants are not supported.");
                default:
                    // not implemented:
                    // vbCurrency
                    // vbDate
                    // vbObject
                    // vbError
                    // vbVariant
                    throw new SerializationException(string.Format("Variant type {0} is not supported.", variantType));
            }
        }

        #endregion
    }
}
