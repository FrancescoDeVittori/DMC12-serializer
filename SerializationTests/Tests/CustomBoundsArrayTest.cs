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
    public class CustomBoundsArrayTest
    {
        [Test]
        public void NullTest()
        {
            var c = new CustomBaseArrays();
            var res = Utils.SerializeDeserialize(c);

            Utils.AreEqual(c, res);
        }

        [Test]
        public void NormalTest()
        {
            var c = new CustomBaseArrays()
            {
                Base0 = new[] { 0, 1, 2, 3, 4 },
                Base2 = new[] { 0, 1, 2, 3, 4, 5, 6 }
            };
            var res = Utils.SerializeDeserialize(c);

            Utils.AreEqual(c, res);
        }

        [Test]
        public void LongTest()
        {
            var c = new CustomBaseArrays()
            {
                Base0 = new int[1024],
                Base2 = new int[1024]
            };
            var res = Utils.SerializeDeserialize(c);

            Utils.AreEqual(c, res);
        }

        [Test]
        public void ShortTest()
        {
            var c = new CustomBaseArrays()
            {
                Base0 = new[] { 0, 1 },
                Base2 = new[] { 2, 3 }
            };
            var res = Utils.SerializeDeserialize(c);

            Utils.AreEqual(c, res);
        }

        [Test]
        public void EmptyTest()
        {
            var c = new CustomBaseArrays()
            {
                Base0 = new int[0],
                Base2 = new int[0]
            };
            var res = Utils.SerializeDeserialize(c);

            Utils.AreEqual(c, res);
        }

        [Test]
        public void CBounds1_Test()
        {
            var c = new CustomBaseArrays();
            Utils.TestRoundTrip(c, "CBounds1");
        }

        [Test]
        public void CBounds2_Test()
        {
            var c = new CustomBaseArrays()
            {
                Base0 = new[] { 0, 1, 2, 3, 4 },
                Base2 = new[] { 0, 1, 2, 3, 4, 5, 6 }
            };
            Utils.TestRoundTrip(c, "CBounds2");
        }

        [Test]
        public void CBounds3_Test()
        {
            var c = new CustomBaseArrays()
            {
                Base0 = new int[1024],
                Base2 = new int[1024]
            };
            Utils.TestRoundTrip(c, "CBounds3");
        }

        // cannot declare a 0-elements array in VB6.
        //
        //[Test]
        //public void CBounds4_Test()
        //{
        //    var c = new CustomBaseArrays()
        //    {
        //        Base0 = new int[0],
        //        Base2 = new int[0]
        //    };
        //    Utils.TestRoundTrip(c, "CBounds4");
        //}
    }
}
