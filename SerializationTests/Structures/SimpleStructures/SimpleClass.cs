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

namespace SerializationTests.Structures
{
    public class SimpleClass : IEquatable<SimpleClass>
    {
        public byte ByteProp { get; set; }
        public bool BoolProp { get; set; }
        public float FloatProp { get; set; }
        public Int16 ShortProp { get; set; }
        public int IntProp { get; set; }
        public double DoubleProp { get; set; }
        public string StringProp { get; set; }
        public DateTime DateTimeProp { get; set; }

        public bool Equals(SimpleClass that)
        {
            if (that == null)
                return false;

            return
                Utils.AreEqual(that.ByteProp, this.ByteProp) &&
                Utils.AreEqual(that.BoolProp, this.BoolProp) &&
                Utils.AreEqual(that.FloatProp, this.FloatProp) &&
                Utils.AreEqual(that.ShortProp, this.ShortProp) &&
                Utils.AreEqual(that.IntProp, this.IntProp) &&
                Utils.AreEqual(that.DoubleProp, this.DoubleProp) &&
                Utils.AreEqual(that.StringProp, this.StringProp) &&
                Utils.AreEqual(that.DateTimeProp, this.DateTimeProp);
        }
    }
}
