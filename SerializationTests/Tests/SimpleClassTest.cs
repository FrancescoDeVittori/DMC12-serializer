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
using System.Runtime.Serialization;
using NUnit.Framework;
using SerializationTests.Structures;

namespace SerializationTests
{
    public class StructuresTest
    {
        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void MissingConstructorTest()
        {
            Utils.CheckSerializeDeserialize(new MissingConstructor(2));
        }

        [Test]
        public void DefaultTest()
        {
            Utils.CheckSerializeDeserialize(new SimpleClass());
        }

        [Test]
        public void SimpleClassTest()
        {
            Utils.CheckSerializeDeserialize(new SimpleClass()
                {
                    BoolProp = true,
                    ByteProp = 15,
                    DateTimeProp = DateTime.Now.RoundToMillisecond(),
                    DoubleProp = 633.23,
                    FloatProp = 22.6F,
                    IntProp = 1256,
                    ShortProp = 14,
                    StringProp = "foo"
                });
        }

        [Test]
        public void ArrayDefaultTest()
        {
            Utils.CheckSerializeDeserialize(new SimpleArrays());
        }

        [Test]
        public void ArrayClassTest()
        {
            Utils.CheckSerializeDeserialize(new SimpleArrays()
                {
                    BoolProp = new[] { true, false, true },
                    ByteProp = new byte[] { 15, 0, 255 },
                    DateTimeProp = new[] { DateTime.Now.RoundToMillisecond(), DateTime.MinValue, new DateTime(1999, 10, 2) },
                    DoubleProp = new[] { Math.PI, Math.E },
                    FloatProp = new[] { 15232.24F, 32.26F, -25.1F },
                    IntProp = new[] { 0, 1, 2, int.MaxValue, int.MinValue },
                    ShortProp = new short[] { 15, 0, -15, short.MaxValue, short.MinValue },
                    StringProp = new string[] { "test", string.Empty, "", " ", "    " }
                });
        }

        [Test]
        public void ArrayClassWithNullsTest()
        {
            Utils.CheckSerializeDeserialize(new SimpleArrays()
            {
                StringProp = new string[] { "test", string.Empty, null, " ", "    " }
            });
        }

        [Test]
        public void Simple1_Test()
        {
            var c = new SimpleClass()
            {
                BoolProp = true,
                ByteProp = 15,
                DateTimeProp = new DateTime(1899, 12, 30, 0, 0, 0),
                //DateTimeProp = new DateTime(1972, 12, 2, 14, 27, 35).RoundToMillisecond(), // Fails because of rounding errors
                DoubleProp = 13.42532,
                FloatProp = 3.1415F,
                IntProp = 235,
                ShortProp = -2562,
                StringProp = "foobar"
            };
            Utils.TestRoundTrip(c, "simple1");
        }

        [Test]
        public void Simple2_Test()
        {
            var c = new SimpleArrays();
            Utils.TestRoundTrip(c, "simple2");
        }

        [Test]
        public void Simple3_Test()
        {
            var c = new SimpleArrays()
            {
                BoolProp = new[] { true, false },
                ByteProp = new byte[] { 15, 8 },
                DateTimeProp = new[] { new DateTime(1899, 12, 30, 0, 0, 0), new DateTime(1899, 12, 30, 0, 0, 0) },
                DoubleProp = new[] { 13.42532, -2113.42532 },
                FloatProp = new[] { 3.1415F, -3.1415F },
                IntProp = new[] { 235352, -12422 },
                ShortProp = new short[] { 2562, -2562 },
                StringProp = new[] { "foo", "bar" }
            };
            Utils.TestRoundTrip(c, "simple3");
        }
    }
}
