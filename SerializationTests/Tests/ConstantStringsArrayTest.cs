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

namespace SerializationTests
{
    public class ConstantStringsArrayTest
    {
        [Test]
        public void DefaultTest()
        {
            var c = new ConstantStringArray();
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringsWith5Chars, c.StringsWith5Chars);
        }

        [Test]
        public void SameLengthTest()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "01234" } };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringsWith5Chars, c.StringsWith5Chars);
        }

        [Test]
        public void SameLengthsTest()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "01234", "01234", "01234" } };
            var res = Utils.SerializeDeserialize(c);

            Assert.AreEqual(res.StringsWith5Chars, c.StringsWith5Chars);
        }

        [Test]
        public void EmptyTest()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "", "", "" } };
            var res = Utils.SerializeDeserialize(c);

            Utils.AssertAreEqual(res.StringsWith5Chars, new[] { new string(' ', 5), new string(' ', 5), new string(' ', 5) });
        }

        [Test]
        public void EmptiesTest()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "", "01234", "" } };
            var res = Utils.SerializeDeserialize(c);

            Utils.AssertAreEqual(res.StringsWith5Chars, new[] { new string(' ', 5), "01234", new string(' ', 5) });
        }

        [Test]
        public void MixedTest()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "", "01234", null, "012345678", "012" } };
            var res = Utils.SerializeDeserialize(c);

            Utils.AssertAreEqual(res.StringsWith5Chars, new[] { new string(' ', 5), "01234", new string(' ', 5), "01234", "012  " });
        }

        [Test]
        public void CStringArr1_Test()
        {
            var c = new ConstantStringArray();
            Utils.TestRoundTrip(c, "CStringArr1");
        }

        [Test]
        public void CStringArr2_Test()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "01234", "abcde" } };
            Utils.TestRoundTrip(c, "CStringArr2");
        }

        [Test]
        public void CStringArr3_Test()
        {
            var c = new ConstantStringArray() { StringsWith5Chars = new[] { "abcde", "ABCDE", "01234", "01   ", "01234" } };
            Utils.TestRoundTrip(c, "CStringArr3");
        }
    }
}
