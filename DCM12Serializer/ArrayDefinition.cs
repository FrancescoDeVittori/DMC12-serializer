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

namespace DMC12Serializer
{
    internal class ArrayDefinition
    {
        private readonly bool _isFixedSize;
        private readonly int _fixedLength;
        private readonly int _from;

        private static ArrayDefinition _default = new ArrayDefinition(false, 0, 0);

        internal bool IsFixedSize { get { return _isFixedSize; } }
        internal int FixedLength { get { return _fixedLength; } }
        internal int From { get { return _from; } }

        private ArrayDefinition(bool isFixedSize, int from, int fixedLength)
        {
            _isFixedSize = isFixedSize;
            _from = from;
            _fixedLength = fixedLength;
        }

        internal static ArrayDefinition Default { get { return _default; } }

        internal static ArrayDefinition Fixed(int length)
        {
            return new ArrayDefinition(true, 0, length);
        }

        internal static ArrayDefinition Dynamic(int from)
        {
            return new ArrayDefinition(false, from, 0);
        }
    }
}
