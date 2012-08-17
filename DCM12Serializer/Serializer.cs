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
using System.Reflection;
using System.Runtime.Serialization;

namespace DMC12Serializer
{
    /// <summary>
    /// Serializes a .NET object in a binary representation compatible with the VB6 Get method.
    /// </summary>
    internal class Serializer
    {
        internal void Serialize(object obj, BinaryWriter writer)
        {
            Serialize(obj, -1, writer);
        }

        private void Serialize(object obj, int constantStringLength, BinaryWriter writer)
        {
            Serialize(obj, ArrayDefinition.Default, constantStringLength, writer);
        }

        private void Serialize(object obj, ArrayDefinition arrayDefinition, int constantStringLength, BinaryWriter writer)
        {
            if (obj == null)
                throw new SerializationException("Cannot serialize a null root value.");

            var objectType = obj.GetType();
            var isSimpleType = objectType.IsSimpleType();

            if (objectType.IsArray && objectType.GetArrayRank() == 1)
            {
                // a 1-dimensional array
                WriteArray((Array)obj, arrayDefinition, constantStringLength, writer);
            }
            else if (objectType.IsArray && objectType.GetArrayRank() == 2)
            {
                // a 2-dimensional ([,]) array
                WriteArray2((Array)obj, constantStringLength, writer);
            }
            else if (isSimpleType)
            {
                // a simple type (including String and Object)
                WriteSimpleObject(obj, constantStringLength, writer);
            }
            else
            {
                // a complex type
                WriteStructure(obj, writer);
            }
        }

        private void WriteSimpleObject(Object obj, int constantStringLength, BinaryWriter writer)
        {
            var oType = obj.GetType();

            if (oType == typeof(byte))
                WriteByte((byte)obj, writer);
            else if (oType == typeof(bool))
                WriteBool((bool)obj, writer);
            else if (oType == typeof(Single))
                WriteFloat((float)obj, writer);
            else if (oType == typeof(Int16))
                WriteShort((short)obj, writer);
            else if (oType == typeof(int))
                WriteInt((int)obj, writer);
            else if (oType == typeof(double))
                WriteDouble((double)obj, writer);
            else if (oType == typeof(string))
            {
                if (constantStringLength >= 0)
                    WriteCString((string)obj, constantStringLength, writer);
                else
                    WriteString((string)obj, writer);
            }
            else if (oType == typeof(DateTime))
                WriteDate((DateTime)obj, writer);
            else if (oType == typeof(object))
                WriteObject(obj, writer);
            else
                throw new SerializationException(string.Format("Type {0} is not supported.", oType.Name));
        }

        private void WriteStructure(object obj, BinaryWriter writer)
        {
            var type = obj.GetType();

            if (type.IsSystemClass())
                throw new SerializationException(string.Format("Type {0} is not supported.", type.Name));

            foreach (var property in type.GetProperties())
            {
                if (property.MustIgnore())
                    continue;

                var constantStringLength = property.GetConstantLengthString();
                var arrayDefinition = property.GetArrayDefinition();
                
                var value = property.GetValue(obj, null);

                if (value == null)
                {
                    if (property.PropertyType.IsArray && !arrayDefinition.IsFixedSize)
                    {
                        WriteNullArray(arrayDefinition, property.PropertyType, writer);
                        continue;
                    }

                    else if (property.PropertyType.IsArray && arrayDefinition.IsFixedSize)
                    {
                        value = property.CreateInstance(arrayDefinition.FixedLength);
                    }
                    else
                    {
                        value = property.CreateInstance();
                    }
                }

                Serialize(value, arrayDefinition, constantStringLength, writer);
            }
        }

        #region Arrays

        private void WriteNullArray(ArrayDefinition definition, Type arrayType, BinaryWriter writer)
        {
            WriteShort(0, writer);
        }

        private void WriteArray(Array arr, ArrayDefinition arrayDefinition, int constantStringLength, BinaryWriter writer)
        {
            if (arrayDefinition.IsFixedSize)
            {
                if (arr.Length != arrayDefinition.FixedLength)
                    throw new SerializationException(
                        string.Format(
                            "Array length mismatch: expected {0} elements but found {1}.",
                            arrayDefinition.FixedLength,
                            arr.Length));
            }
            else
            {
                // Dynamically sized arrays have an header made of rank, length and starting index.
                WriteArrayRank(1, writer);
                WriteArrayLength(arr.Length, arrayDefinition.From, writer);
            }

            WriteFixedArray(arr, constantStringLength, writer);
        }

        private void WriteFixedArray(Array arr, int constantStringLength, BinaryWriter writer)
        {
            // Fixed-size arrays have no header at all. Just all the values one after each other.
            for (var i = 0; i < arr.Length; i++)
            {
                var value = arr.GetValue(i);
                if (value == null)
                    value = arr.CreateElementInstance();

                Serialize(value, constantStringLength, writer);
            }
        }

        private void WriteArray2(Array arr, int constantStringLength, BinaryWriter writer)
        {
            // only dynamic, 0-based multidimensional arrays are supported.

            WriteArrayRank(2, writer);

            int i = arr.GetLength(0);
            int j = arr.GetLength(1);

            WriteArrayLength(j, 0, writer);
            WriteArrayLength(i, 0, writer);

            for (int jj = 0; jj < j; jj++)
            {
                for (int ii = 0; ii < i; ii++)
                {
                    var value = arr.GetValue(ii, jj);
                    if (value == null)
                        value = arr.CreateElementInstance();

                    Serialize(value, constantStringLength, writer);
                }
            }
        }

        private void WriteArrayRank(short dim, BinaryWriter writer)
        {
            // writes the array rank, for ex.: int[] -> 1, int[,] -> 2
            writer.Write(dim);
        }

        private void WriteArrayLength(int len, int startIndex, BinaryWriter writer)
        {
            // writes the length of the array
            writer.Write(len);

            // writes the array base, i.e. the first index
            writer.Write(startIndex);
        }

        #endregion

        #region Simple types

        private void WriteByte(byte val, BinaryWriter writer)
        {
            // 8 bits
            writer.Write(val);
        }

        private void WriteBool(bool value, BinaryWriter writer)
        {
            // .NET bool, VB6 Boolean: 32 / 16 bits
            // VB6: true=-1, false=0  .NET: true=1, false=0
            short v = value ? (short)-1 : (short)0;
            writer.Write(v);
        }

        private void WriteFloat(float value, BinaryWriter writer)
        {
            // .NET float, VB6 Single: 32 bits
            writer.Write(value);
        }

        private void WriteShort(short value, BinaryWriter writer)
        {
            // .NET short, VB6 integer: 16 bits
            writer.Write(value);
        }

        private void WriteInt(int value, BinaryWriter writer)
        {
            // .NET int, VB6 long: 32 bits
            writer.Write(value);
        }

        private void WriteDouble(double value, BinaryWriter writer)
        {
            // 64 bits
            writer.Write(value);
        }

        private void WriteString(string str, BinaryWriter writer)
        {
            // 16 bits for the string length
            // 32 bits/char in .NET, 16 bits for ASCII in VB6, 32 bits for Unicode in VB6.

            if (str == null)
            {
                WriteShort(0, writer);
                return;
            }

            writer.Write((short)(str.Length));
            foreach (char c in str.ToCharArray())
            {
                writer.Write(c);
            }
        }

        internal void WriteCString(string str, int stringLength, BinaryWriter writer)
        {
            // constant-length string:
            // 32 bits/char in .NET, 16 bits for ASCII in VB6, 32 bits for Unicode in VB6.

            if (str == null)
                str = string.Empty;
            
            var toWrite = str;
            if (str.Length < stringLength)
            {
                toWrite = str.PadRight(stringLength, ' ');
            }
            else if (str.Length > stringLength)
            {
                toWrite = str.Substring(0, stringLength);
            }

            foreach (char c in toWrite.ToCharArray())
            {
                writer.Write(c);
            }
        }

        private void WriteDate(DateTime date, BinaryWriter writer)
        {
            // 64 bits, as a double.
            // Integral part represents days from Jan 1, 100.
            // Decimal part represents the fraction of the day.
            var ts = date.Subtract(new DateTime(1899, 12, 30));
            writer.Write(ts.TotalDays);
        }

        private void WriteObject(object obj, BinaryWriter writer)
        {
            // .NET Object, VB6 Variant.
            // 16 bytes in memory, here seems to be just 2 bytes of datatype, followed by the content.

            if (obj == null)
            {
                writer.Write((short)Variants.VbEmpty);
            }
            else if (obj is short)
            {
                writer.Write((short)Variants.VbInteger);
                WriteShort((short)obj, writer);
            }
            else if (obj is int)
            {
                writer.Write((short)Variants.VbLong);
                WriteInt((int)obj, writer);
            }
            else if (obj is float)
            {
                writer.Write((short)Variants.VbSingle);
                WriteFloat((float)obj, writer);
            }
            else if (obj is double)
            {
                writer.Write((short)Variants.VbDouble);
                WriteDouble((double)obj, writer);
            }
            else if (obj is string)
            {
                writer.Write((short)Variants.VbString);
                WriteString((string)obj, writer);
            }
            else if (obj is bool)
            {
                writer.Write((short)Variants.VbBoolean);
                WriteBool((bool)obj, writer);
            }
            else if (obj is byte)
            {
                writer.Write((short)Variants.VbByte);
                WriteByte((byte)obj, writer);
            }
            else if (obj is Array)
            {
                writer.Write((short)Variants.VbArray);
                Serialize(obj, writer);
            }
            else if (obj.GetType().IsClass)
            {
                writer.Write((short)Variants.VbComplex);
                Serialize(obj, writer);
            }
            else
            {
                // not implemented:
                // vbCurrency
                // vbDate
                // vbObject
                // vbError
                // vbVariant
                throw new SerializationException(string.Format("Variant type {0} is not supported.", obj.GetType()));
            }
        }

        #endregion
    }
}
