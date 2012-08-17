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
using System.Runtime.Serialization;
using NUnit.Framework;

namespace SerializationTests
{
    public class SimpleTypesTest
    {
        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void NullTest()
        {
            Utils.CheckSerializeDeserialize((string)null);
        }

        [TestCase((byte)0)]
        [TestCase((byte)20)]
        [TestCase((byte)255)]
        [TestCase(byte.MinValue)]
        [TestCase(byte.MaxValue)]
        public void ByteTest(byte value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BoolTest(bool value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(0F)]
        [TestCase(100.23F)]
        [TestCase(-10.5F)]
        [TestCase(Single.MinValue)]
        [TestCase(Single.MaxValue)]
        public void FloatTest(Single value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase((Int16)0)]
        [TestCase((Int16)100)]
        [TestCase((Int16)(-10))]
        [TestCase(Int16.MinValue)]
        [TestCase(Int16.MaxValue)]
        public void Int16Test(Int16 value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(-10)]
        [TestCase(Int32.MinValue)]
        [TestCase(Int32.MaxValue)]
        public void IntTest(int value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase((double)0)]
        [TestCase((double)100)]
        [TestCase((double)-10)]
        [TestCase((double)-10.333632)]
        [TestCase(Math.PI)]
        [TestCase(Double.MinValue)]
        [TestCase(Double.MaxValue)]
        [TestCase(Double.Epsilon)]
        [TestCase(Double.NaN)]
        [TestCase(Double.NegativeInfinity)]
        [TestCase(Double.PositiveInfinity)]
        public void DoubleTest(double value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCase("")]
        [TestCase("test")]
        [TestCase("zynu bifawebfyaw weiofnweofwe")]
        public void StringTest(string value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        public DateTime[] GetDateTimes()
        {
            return new DateTime[]
            {
                new DateTime(),
                DateTime.Now,
                DateTime.MaxValue,
                DateTime.MinValue,
                new DateTime(2010, 1, 1),
                new DateTime(2010, 3, 4, 8, 3, 2, 5)
            };
        }

        [TestCaseSource("GetDateTimes")]
        public void DateTimeTest(DateTime value)
        {
            // VB6 has a 1 ms resolution
            Utils.CheckSerializeDeserialize(value.RoundToMillisecond());
        }
    }
}
