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
    public class ConstantStringTest
    {
        [Test]
        public void SameLengthTest()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "01234" };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringWith5Chars, c.StringWith5Chars);
        }

        [Test]
        public void EmptyTest()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "" };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringWith5Chars, new string(' ', 5));
        }

        [Test]
        public void ShorterTest()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "012" };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringWith5Chars, "012  ");
        }

        [Test]
        public void NullTest()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = null };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringWith5Chars, new string(' ', 5));
        }

        [Test]
        public void LongerTest()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "0123456789123456789" };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringWith5Chars, "01234");
        }

        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void InvalidLengthTest()
        {
            var c = new InvalidStringLengthProperty() { StringWith5Chars = "0123456789123456789" };
            var res = Utils.SerializeDeserialize(c);
        }

        [Test]
        public void CString1_Test()
        {
            var c = new ConstantStringLengthProperty();
            Utils.TestRoundTrip(c, "CString1");
        }

        [Test]
        public void CString2_Test()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "     " };
            Utils.TestRoundTrip(c, "CString2");
        }

        [Test]
        public void CString3_Test()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "01234" };
            Utils.TestRoundTrip(c, "CString3");
        }

        [Test]
        public void CString4_Test()
        {
            var c = new ConstantStringLengthProperty() { StringWith5Chars = "01234" };
            Utils.TestRoundTrip(c, "CString4");
        }
    }
}
