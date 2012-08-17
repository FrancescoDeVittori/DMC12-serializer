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
using System.Linq;
using NUnit.Framework;
using SerializationTests.Structures;

namespace SerializationTests
{
    public class TwoDimsArraysTest
    {
        [TestCaseSource("GetBytes")]
        public void ByteTest(byte[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("GetBools")]
        public void BoolTest(bool[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("GetSingles")]
        public void FloatTest(Single[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("GetInt16s")]
        public void Int16Test(Int16[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("Getints")]
        public void IntTest(int[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("GetDoubles")]
        public void DoubleTest(double[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("GetStrings")]
        public void StringTest(string[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [TestCaseSource("GetDateTimes")]
        public void DateTimeTest(DateTime[,] value)
        {
            Utils.CheckSerializeDeserialize(value);
        }

        [Test]
        public void TwoDims1_Test()
        {
            var c = new TwoDimsArrays();
            Utils.TestRoundTrip(c, "TwoDims1");
        }

        [Test]
        public void TwoDims2_Test()
        {
            var c = new TwoDimsArrays()
            {
                BoolProp = new bool[,] { { false, false, true }, { true, true, false } },
                ByteProp = new byte[,] { { 14, 251, 22 }, { 15, 252, 23 } },
                DateTimeProp = new DateTime[,] { { new DateTime(2010, 1, 1).RoundToMillisecond() }, { new DateTime(2010, 8, 8).RoundToMillisecond() } },
                DoubleProp = new double[,] { { 14.444, 251, 22 }, { 15.32, 252, 23 } },
                FloatProp = new Single[,] { { 14.444F, 251F, 22F }, { 15.32F, 252F, 23F } },
                IntProp = new int[,] { { 14, 251, 22 }, { 15, 252, 23 } },
                ShortProp = new Int16[,] { { 14, 251, -22 }, { 15, -252, 23 } },
                StringProp = new string[,] { { "foo", "bar", "foobar" }, { "foo2", "bar2", "foobar2" } }
            };
            Utils.TestRoundTrip(c, "TwoDims2");
        }


        private byte[][,] GetBytes()
        {
            return new byte[][,]
            {
                new byte[,] { },
                new byte[,] { { } },
                new byte[,] { { }, { } },
                new byte[,] { { }, { }, { } },
                new byte[,] { { 2 } },
                new byte[,] { { 2 }, { 3 } },
                new byte[,] { { 0 }, { 0 }, { 4 } },
                new byte[,] { { 14, 251, 22 }, { 15, 252, 23 } },
                new byte[,] { { 14, byte.MaxValue, byte.MinValue }, { 14, byte.MaxValue, byte.MinValue }, { 14, byte.MaxValue, byte.MinValue }, { 14, byte.MaxValue, byte.MinValue } }
            };
        }


        private bool[][,] GetBools()
        {
            return new bool[][,]
            {
                new bool[,] { },
                new bool[,] { { } },
                new bool[,] { { }, { } },
                new bool[,] { { }, { }, { } },
                new bool[,] { { true } },
                new bool[,] { { true }, { false } },
                new bool[,] { { true }, { false }, { true } },
                new bool[,] { { false, false, true }, { true, true, false } },
                new bool[,] { { false, false, false }, { true, true, true }, { false, false, false }, { true, true, true } }
            };
        }

        private Single[][,] GetSingles()
        {
            return new Single[][,]
            {
                new Single[,] { },
                new Single[,] { { } },
                new Single[,] { { }, { } },
                new Single[,] { { }, { }, { } },
                new Single[,] { { 2F } },
                new Single[,] { { 2F }, { 3F } },
                new Single[,] { { 0F }, { 0F }, { 4F } },
                new Single[,] { { 14.444F, 251F, 22F }, { 15.32F, 252F, 23F } },
                new Single[,] { { 14F, Single.MaxValue, Single.MinValue }, { 14.025F, Single.MaxValue, Single.MinValue }, { -14F, Single.MaxValue, Single.MinValue }, { 14F, Single.MaxValue, Single.MinValue } }
            };
        }

        private Int16[][,] GetInt16s()
        {
            return new Int16[][,]
            {
                new Int16[,] { },
                new Int16[,] { { } },
                new Int16[,] { { }, { } },
                new Int16[,] { { }, { }, { } },
                new Int16[,] { { 2 } },
                new Int16[,] { { 2 }, { 3 } },
                new Int16[,] { { 0 }, { 0 }, { 4 } },
                new Int16[,] { { 14, 251, -22 }, { 15, -252, 23 } },
                new Int16[,] { { 14, Int16.MaxValue, Int16.MinValue }, { 14, Int16.MaxValue, Int16.MinValue }, { 14, Int16.MaxValue, Int16.MinValue }, { -14, Int16.MaxValue, Int16.MinValue } }
            };
        }

        private int[][,] Getints()
        {
            return new int[][,]
            {
                new int[,] { },
                new int[,] { { } },
                new int[,] { { }, { } },
                new int[,] { { }, { }, { } },
                new int[,] { { 2 } },
                new int[,] { { 2 }, { 3 } },
                new int[,] { { 0 }, { 0 }, { 4 } },
                new int[,] { { 14, 251, 22 }, { 15, 252, 23 } },
                new int[,] { { 14, int.MaxValue, int.MinValue }, { 145, int.MaxValue, int.MinValue }, { -14, int.MaxValue, int.MinValue }, { 14, int.MaxValue, int.MinValue } }
            };
        }

        private double[][,] GetDoubles()
        {
            return new double[][,]
            {
                new double[,] { },
                new double[,] { { } },
                new double[,] { { }, { } },
                new double[,] { { }, { }, { } },
                new double[,] { { 2 } },
                new double[,] { { 2 }, { 3 } },
                new double[,] { { 0 }, { 0 }, { 4 } },
                new double[,] { { 14.444, 251, 22 }, { 15.32, 252, 23 } },
                new double[,] { { 14, double.MaxValue, Math.PI }, { 14.025, double.MaxValue, Math.E }, { -14, double.MaxValue, double.MinValue }, { 14, double.MaxValue, double.MinValue } }
            };
        }

        public string[][,] GetStrings()
        {
            return new string[][,]
            {
                new string[,] { },
                new string[,] { { } },
                new string[,] { { }, { } },
                new string[,] { { }, { }, { } },
                new string[,] { { "" } },
                new string[,] { { null } },
                new string[,] { { "test" }, { "foo" } },
                new string[,] { { "foo" }, { "bar" }, { "foobar" } },
                new string[,] { { "foo", "bar", "foobar" }, { "foo2", "bar2", "foobar2" } },
                new string[,] { { "foo", "bar", "foobar" }, { "foogerger", "bargerger", "foobagergerr" },{ "foogser", "bargesgs", "foobargsegges" },{ "foo3", "bar3", "foobar3" }}
            };
        }

        public DateTime[][,] GetDateTimes()
        {
            return new DateTime[][,]
            {
                new DateTime[,] { },
                new DateTime[,] { { new DateTime(2010, 1, 1).RoundToMillisecond() }, { new DateTime(2012, 8, 9).RoundToMillisecond() } },
                new DateTime[,] { { new DateTime(2010, 3, 4, 8, 3, 2, 5).RoundToMillisecond(), DateTime.MinValue.RoundToMillisecond(), DateTime.MaxValue.RoundToMillisecond() }, { new DateTime(2003, 1, 3, 4, 1, 5, 6).RoundToMillisecond(), DateTime.MinValue.RoundToMillisecond(), DateTime.MaxValue.RoundToMillisecond() } }
            };
        }
    }
}
