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
    public class SimpleArrays : IEquatable<SimpleArrays>
    {
        public byte[] ByteProp { get; set; }
        public bool[] BoolProp { get; set; }
        public float[] FloatProp { get; set; }
        public Int16[] ShortProp { get; set; }
        public int[] IntProp { get; set; }
        public double[] DoubleProp { get; set; }
        public string[] StringProp { get; set; }
        public DateTime[] DateTimeProp { get; set; }

        public bool Equals(SimpleArrays that)
        {
            if (that == null)
                return false;

            return
                Utils.AreEqual(this.ByteProp, that.ByteProp) &&
                Utils.AreEqual(this.BoolProp, that.BoolProp) &&
                Utils.AreEqual(this.FloatProp, that.FloatProp) &&
                Utils.AreEqual(this.ShortProp, that.ShortProp) &&
                Utils.AreEqual(this.IntProp, that.IntProp) &&
                Utils.AreEqual(this.DoubleProp, that.DoubleProp) &&
                Utils.AreEqual(this.StringProp, that.StringProp) &&
                Utils.AreEqual(this.DateTimeProp, that.DateTimeProp);
        }
    }
}
