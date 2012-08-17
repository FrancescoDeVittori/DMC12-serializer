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
using DMC12Serializer;

namespace SerializationTests.Structures
{
    public class FixedLengthArray : IEquatable<FixedLengthArray>
    {
        [FixedLength(5)]
        public int[] SomeInts { get; set; }


        public bool Equals(FixedLengthArray other)
        {
            if (other == null)
                return false;

            return Utils.AreEqual(SomeInts, other.SomeInts);
        }
    }

    public class InvalidLengthArray
    {
        [FixedLength(-1)]
        public int[] SomeInts { get; set; }
    }

    public class FixedAndDynamicLengthArray
    {
        [FixedLength(10)]
        [DynamicLength(10)]
        public int[] SomeInts { get; set; }
    }
}
