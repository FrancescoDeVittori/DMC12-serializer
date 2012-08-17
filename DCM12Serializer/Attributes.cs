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

namespace DMC12Serializer
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotSerializeAttribute : Attribute
    {

        /// <summary>
        /// Decorates a property that has no correspondence in the VB6 structure.
        /// </summary>
        public DoNotSerializeAttribute()
        { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FixedLengthAttribute : Attribute
    {
        public int Length { get; set; }

        /// <summary>
        /// Decorates a property that corresponds to a fixed-length (one-dimensional) VB6 array.
        /// </summary>
        /// <param name="length">The array length.</param>
        public FixedLengthAttribute(int length)
        {
            Length = length;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicLengthAttribute : Attribute
    {
        public int From { get; set; }

        /// <summary>
        /// Decorates a property that corresponds to a dynamic-length (one-dimensional) VB6 array.
        /// </summary>
        /// <param name="from">the base of the array (i.e. the index of the first element)</param>
        public DynamicLengthAttribute(int from)
        {
            From = from;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConstantLengthString : Attribute
    {
        public int Length { get; set; }

        /// <summary>
        /// Decorates a property that corresponds to a VB6 constant-length string.
        /// </summary>
        /// <param name="length">The string length</param>
        public ConstantLengthString(int length)
        {
            Length = length;
        }
    }
}
