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
    public class DoNotSerializeTest
    {
        [Test]
        public void DoNotSerializeAttributeTest()
        {
            var s = GetSample();
            var res = Utils.SerializeDeserialize(s);
            Utils.AssertAreEqual(res.BytesSerialized, s.BytesSerialized);
        }

        [Test]
        public void DoNotSerializeAttribute2Test()
        {
            var s = GetSample();
            var res = Utils.SerializeDeserialize(s);
            Utils.AssertAreEqual(res.IntSerialized, s.IntSerialized);
        }

        [Test]
        public void DoNotSerializeAttribute3Test()
        {
            var s = GetSample();
            var res = Utils.SerializeDeserialize(s);
            Utils.AssertAreEqual(res.StringSerialized, s.StringSerialized);
        }

        [Test]
        public void DoNotSerializeAttribute4Test()
        {
            var s = GetSample();
            var res = Utils.SerializeDeserialize(s);
            Assert.IsNull(res.BytesIgnored);
        }

        [Test]
        public void DoNotSerializeAttribute5Test()
        {
            var s = GetSample();
            var res = Utils.SerializeDeserialize(s);
            Assert.That(res.IntIgnored == 0);
        }

        [Test]
        public void DoNotSerializeAttribute6Test()
        {
            var s = GetSample();
            var res = Utils.SerializeDeserialize(s);
            Assert.IsNullOrEmpty(res.StringIgnored);
        }

        private DoNotSerializeAttr GetSample()
        {
            return new DoNotSerializeAttr()
            {
                BytesIgnored = new byte[] { 0, 145, 2, 10 },
                BytesSerialized = new byte[] { 3, 255 },
                IntIgnored = 10,
                IntSerialized = 22,
                StringIgnored = "hello",
                StringSerialized = "test"
            };
        }

        [Test]
        public void DoNot1_Test()
        {
            var s = GetSample();
            var ser = Utils.SerializeDeserialize(s);

            Utils.TestRoundTrip(ser, "DoNot1");
        }
    }
}
