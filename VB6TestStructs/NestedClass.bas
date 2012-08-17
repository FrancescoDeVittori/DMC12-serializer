Attribute VB_Name = "NestedClass"
 ' Copyright (c) 2006-2012 Francesco De Vittori, Board International SA
 '
 ' Distributed under the MIT license.
 ' Permission is hereby granted, free of charge, to any person obtaining a copy
 ' of this software and associated documentation files (the "Software"), to deal
 ' in the Software without restriction, including without limitation the rights
 ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 ' of the Software, and to permit persons to whom the Software is furnished to do
 ' so, subject to the following conditions:
 '
 ' The above copyright notice and this permission notice shall be included in all
 ' copies or substantial portions of the Software.
 '
 ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 ' INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 ' PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 ' HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 ' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 ' OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Option Explicit

Type InnerStructure2
    SomeBytes() As Byte
End Type

Type InnerStructure
    StringProp As String
    Inner As InnerStructure2
End Type

Type NestedClass
    StringProp As String
    NestedProp As InnerStructure
End Type


Sub MakeNestedClasses()

    Dim c1 As NestedClass
    WriteToFile6 "Nested1", c1
    
    Dim c2 As NestedClass
    c2.StringProp = "foo"
    WriteToFile6 "Nested2", c2
    
    Dim c3 As NestedClass
    c3.StringProp = "foo"
    c3.NestedProp.StringProp = "bar"
    WriteToFile6 "Nested3", c3
    
    Dim c4 As NestedClass
    c4.StringProp = "foo"
    c4.NestedProp.StringProp = "bar"
    WriteToFile6 "Nested4", c4
    
    Dim c5 As NestedClass
    c5.StringProp = "foo"
    c5.NestedProp.StringProp = "bar"
    ReDim c5.NestedProp.Inner.SomeBytes(254) As Byte
    WriteToFile6 "Nested5", c5

End Sub
