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
using System.Linq;
using System.Text;

namespace SerializationTests.Structures
{
    public class NestedClass : IEquatable<NestedClass>
    {
        public string StringProp { get; set; }
        public InnerStructure NestedProp { get; set; }

        public bool Equals(NestedClass other)
        {
            return
                Utils.AreEqual(StringProp, other.StringProp) &&
                Utils.AreEqual(NestedProp, other.NestedProp);
        }
    }

    public class InnerStructure : IEquatable<InnerStructure>
    {
        public string StringProp { get; set; }
        public InnerStructure2 Inner { get; set; }

        public bool Equals(InnerStructure other)
        {
            return
                Utils.AreEqual(StringProp, other.StringProp) &&
                Utils.AreEqual(Inner, other.Inner);
        }
    }

    public class InnerStructure2 : IEquatable<InnerStructure2>
    {
        public byte[] SomeBytes { get; set; }

        public bool Equals(InnerStructure2 other)
        {
            return
                Utils.AreEqual(SomeBytes, other.SomeBytes);
        }
    }
}
