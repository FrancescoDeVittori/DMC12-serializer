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

using NUnit.Framework;
using SerializationTests.Structures;
using System.Runtime.Serialization;

namespace SerializationTests
{
    public class FixedLengthArrayTest
    {
        [Test]
        public void OkTest()
        {
            Utils.CheckSerializeDeserialize(new FixedLengthArray() { SomeInts = new[] { 0, 1, 2, 3, 4 } });
        }

        [Test]
        public void NullTest()
        {
            // fixed-length arrays are always written, even if they are null.
            var original = new FixedLengthArray() { SomeInts = null };
            var res = Utils.SerializeDeserialize(original);

            Utils.AssertAreEqual(res, new FixedLengthArray() { SomeInts = new int[5] });
        }

        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void ShorterTest()
        {
            Utils.CheckSerializeDeserialize(new FixedLengthArray() { SomeInts = new int[2] });
        }

        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void LongerTest()
        {
            Utils.CheckSerializeDeserialize(new FixedLengthArray() { SomeInts = new int[15] });
        }

        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void InvalidLengthTest()
        {
            Utils.CheckSerializeDeserialize(new InvalidLengthArray() { SomeInts = new int[4] });
        }

        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void InvalidAttributesTest()
        {
            Utils.CheckSerializeDeserialize(new FixedAndDynamicLengthArray() { SomeInts = new int[4] });
        }

        [Test]
        public void FixArr1_Test()
        {
            var c = new FixedLengthArray() { SomeInts = new[] { 0, 1, 2, 3, 4 } };
            Utils.TestRoundTrip(c, "FixArr1");
        }

        [Test]
        public void FixArr2_Test()
        {
            var c = new FixedLengthArray() { SomeInts = new int[5] };
            var ser = Utils.SerializeDeserialize(c);
            Utils.TestRoundTrip(ser, "FixArr2");
        }
    }
}
