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
using System.Text;

namespace DMC12Serializer
{
    public class DMC12Serializer
    {
        /// <summary>
        /// Serializes the given object to a VB6 dump.
        /// </summary>
        public byte[] Serialize(object root)
        {
            using (var stream = new MemoryStream())
            {
                Serialize(root, stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Serializes the given object to a VB6 dump.
        /// </summary>
        public void Serialize(object root, Stream outputStream)
        {
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            using (var writer = new BinaryWriter(outputStream, Encoding.ASCII))
            {
                new Serializer().Serialize(root, writer);
            }
        }

        /// <summary>
        /// Serializes the given string to a VB6 binary dump, in fixed-length format.
        /// </summary>
        public byte[] SerializeFixedString(string str)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.ASCII))
                {
                    new Serializer().WriteCString(str, str.Length, writer);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Hydrates a VB6 dump into a new instance of the provided type.
        /// </summary>
        public object Deserialize(Type targetType, byte[] serializedContent)
        {
            using (var ms = new MemoryStream(serializedContent))
            {
                return Deserialize(targetType, ms);
            }
        }

        public object Deserialize(Type targetType, MemoryStream stream)
        {
            return Deserialize(targetType, stream, -1);
        }

        public object Deserialize(Type targetType, MemoryStream stream, int offset)
        {
            return Deserialize(targetType, stream, offset, false);
        }

        public object Deserialize(Type targetType, MemoryStream stream, int offset, bool checkFullStream)
        {
            object result = null;

            using (var reader = new BinaryReader(stream, Encoding.ASCII))
            {
                if (offset >= 0)
                    stream.Position = offset;

                result = new Deserializer().Deserialize(targetType, reader);

                if (checkFullStream && reader.BaseStream.Position != reader.BaseStream.Length)
                    throw new SerializationException(string.Format("{0} bytes are still in the buffer.", (reader.BaseStream.Length - reader.BaseStream.Position)));
            }

            return result;
        }
    }
}
