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
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace SerializationTests
{
    public static class Utils
    {
        public static DateTime RoundToMillisecond(this DateTime dt)
        {
            return dt.AddTicks(-(dt.Ticks % TimeSpan.TicksPerSecond));
        }

        private static byte[] ReadDump(string fileName)
        {
            try
            {
                var assembly = typeof(Utils).Assembly;
                using (var stream = assembly.GetManifestResourceStream("SerializationTests.VB6Dumps." + fileName + ".bin"))
                {
                    if (stream == null)
                    {
                        throw new Exception(string.Format("Could not find test case file '{0}'", fileName));
                    }

                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    return buffer;
                }
            }
            catch (IOException ex)
            {
                throw new Exception(string.Format("Could not read test case '{0}': {1}", fileName, ex.Message));
            }
        }

        public static void TestRoundTrip<T>(T originalObject, string dumpFileName)
        {
            var serializer = new DMC12Serializer.DMC12Serializer();

            var vb6Dump = ReadDump(dumpFileName);
            var vb6Object = (T)serializer.Deserialize(typeof(T), vb6Dump);

            Utils.AssertAreEqual(originalObject, vb6Object);

            var csDump = serializer.Serialize(originalObject);
            AssertAreEqual(csDump, vb6Dump);
        }

        public static T SerializeDeserialize<T>(T value)
        {
            var serializer = new DMC12Serializer.DMC12Serializer();
            var dump = serializer.Serialize(value);
            return (T)serializer.Deserialize(typeof(T), dump);
        }

        public static void CheckSerializeDeserialize<T>(T value)
        {
            var res = SerializeDeserialize(value);

            if (value == null)
                Assert.That(res == null);
            else
                AssertAreEqual(res, value);
        }

        public static void CheckSerializeDeserialize<T>(T[] value)
        {
            var res = SerializeDeserialize(value);
            AssertAreEqual(res, value);
        }

        public static void CheckSerializeDeserialize<T>(T[,] value)
        {
            var res = SerializeDeserialize(value);
            AssertAreEqual(res, value);
        }

        public static void AssertAreEqual<T>(T[] one, T[] another)
        {
            Assert.That(AreEqual(one, another));
        }

        public static void AssertAreEqual<T>(T one, T another)
        {
            Assert.That(AreEqual(one, another));
        }

        public static void AssertAreEqual<T>(T[,] one, T[,] another)
        {
            Assert.That(AreEqual(one, another));
        }

        public static bool AreEqual<T>(T[,] one, T[,] another)
        {
            if (one == null || another == null)
                return one == another;

            if (one.GetLength(0) != another.GetLength(0))
                return false;

            if (one.GetLength(1) != another.GetLength(1))
                return false;

            for (int i = 0; i < one.GetLength(0); i++)
            {
                for (int j = 0; j < one.GetLength(1); j++)
                {
                    var a = (T)one.GetValue(i, j);
                    var b = (T)another.GetValue(i, j);
                    if (!AreEqual(a, b))
                        return false;
                }
            }

            return true;
        }

        public static bool AreEqual<T>(T[] one, T[] another)
        {
            if (one == null || another == null)
                return one == another;

            if (one.Length != another.Length)
                return false;

            for (int i = 0; i < one.Length; i++)
            {
                if (!AreEqual(one[i], another[i]))
                    return false;
            }

            return true;
        }

        public static bool AreEqual<T>(T one, T another)
        {
            if (typeof(T) == typeof(DateTime) || (one != null && one is DateTime) || (another != null && another is DateTime))
            {
                return AreDateTimesEqual(one, another);
            }
            else if ((one != null && one is string) || (another != null && another is string))
            {
                return AreStringsEqual(one, another);
            }
            else
            {
                return EqualityComparer<T>.Default.Equals(one, another);
            }
        }

        public static bool AreDateTimesEqual(object one, object another)
        {
            return ((DateTime)one).RoundToMillisecond() == ((DateTime)another).RoundToMillisecond();
        }

        public static bool AreStringsEqual(object one, object another)
        {
            return ((string)one) == ((string)another) || (string.IsNullOrEmpty((string)one) && string.IsNullOrEmpty((string)another));
        }
    }
}
