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
using System.Linq;
using NUnit.Framework;

namespace SerializationTests
{
    public class SimpleArraysTest
    {
        [TestCase(new byte[] { })]
        [TestCase(new byte[] { 0 })]
        [TestCase(new byte[] { 14, 251, 22 })]
        [TestCase(new byte[] { 14, 251, 22, 2, 8, 24, 11, 90, 11, 83, 19, 63, 9, byte.MaxValue, byte.MinValue })]
        public void ByteTest(byte[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(new bool[] { })]
        [TestCase(new bool[] { true })]
        [TestCase(new bool[] { false })]
        [TestCase(new bool[] { true, false, true, false, false, true, false, true, true, true })]
        public void BoolTest(bool[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(new Single[] { })]
        [TestCase(new Single[] { 0F })]
        [TestCase(new Single[] { 0F, 55.3F, 12.3F, -2.5F, 8F, Single.MaxValue, Single.MinValue })]
        public void FloatTest(Single[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(new Int16[] { })]
        [TestCase(new Int16[] { 0 })]
        [TestCase(new Int16[] { 0, 55, 12, 2, 8, -6999, Int16.MinValue, Int16.MaxValue })]
        public void Int16Test(Int16[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 0 })]
        [TestCase(new int[] { 0, 55, 12, 2, 8, -6999 })]
        [TestCase(new int[] { 0, 55, 12, 2, 8, -6999, int.MaxValue, int.MinValue })]
        public void IntTest(int[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(new double[] { })]
        [TestCase(new double[] { 0 })]
        [TestCase(new double[] { 0, 55.3, 12.3, -2.5, 8, double.MaxValue, double.MinValue, Math.PI })]
        public void DoubleTest(double[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        public string[][] GetStrings()
        {
            return new string[][]
            {
                new string[] { },
                new string[] { "" },
                new string[] { "test" },
                new string[] { "test", "", "test2", "" },
                new string[] { "test", "another", "test", "astring" }
            };
        }

        [TestCaseSource("GetStrings")]
        public void StringTest(string[] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [Test]
        public void StringWithNullsTest()
        {
            var provided = new string[] { null, null, "test", null };
            var expected = new string[] { "", "", "test", "" };

            var serializer = new DMC12Serializer.DMC12Serializer();
            var actual = (string[])serializer.Deserialize(typeof(string[]), serializer.Serialize(provided));

            Utils.AssertAreEqual(actual, expected);
        }

        public DateTime[][] GetDateTimes()
        {
            return new DateTime[][]
            {
                new DateTime[] { },
                new DateTime[] { new DateTime(2010, 1, 1) },
                new DateTime[] { new DateTime(2010, 3, 4, 8, 3, 2, 5), DateTime.MinValue, DateTime.MaxValue }
            };
        }

        [TestCaseSource("GetDateTimes")]
        public void DateTimeTest(DateTime[] value)
        {
            // VB6 has a 1 ms resolution
            if (value != null)
                value = value.Select(dt => dt.RoundToMillisecond()).ToArray();

            Utils.CheckSerializeDeserialize(value);
        }
    }
}
