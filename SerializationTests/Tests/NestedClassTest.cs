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
    public class NestedClassTest
    {
        // note: cyclic references are not supported (by design, because VB6 does not handle them)

        [Test]
        public void SimpleTest()
        {
            Utils.CheckSerializeDeserialize(new NestedClass()
                {
                    StringProp = "foo",
                    NestedProp = new InnerStructure()
                    {
                        StringProp = "bar",
                        Inner = new InnerStructure2()
                        {
                            SomeBytes = new byte[] { 10, 5, 255, 8 }
                        }
                    }
                });
        }

        [Test]
        public void Nested1_Test()
        {
            var c = new NestedClass() { NestedProp = new InnerStructure() { Inner = new InnerStructure2() } };
            Utils.TestRoundTrip(c, "Nested1");
        }

        [Test]
        public void Nested2_Test()
        {
            var c = new NestedClass()
            {
                StringProp = "foo",
                NestedProp = new InnerStructure() { Inner = new InnerStructure2() }
            };
            Utils.TestRoundTrip(c, "Nested2");
        }

        [Test]
        public void Nested3_Test()
        {
            var c = new NestedClass()
            {
                StringProp = "foo",
                NestedProp = new InnerStructure()
                {
                    StringProp = "bar",
                    Inner = new InnerStructure2()
                }
            };
            Utils.TestRoundTrip(c, "Nested3");
        }

        [Test]
        public void Nested4_Test()
        {
            var c = new NestedClass()
            {
                StringProp = "foo",
                NestedProp = new InnerStructure()
                {
                    StringProp = "bar",
                    Inner = new InnerStructure2()
                }
            };
            Utils.TestRoundTrip(c, "Nested4");
        }

        [Test]
        public void Nested5_Test()
        {
            var c = new NestedClass()
            {
                StringProp = "foo",
                NestedProp = new InnerStructure()
                {
                    StringProp = "bar",
                    Inner = new InnerStructure2() { SomeBytes = new byte[255] }
                }
            };
            Utils.TestRoundTrip(c, "Nested5");
        }
    }
}
