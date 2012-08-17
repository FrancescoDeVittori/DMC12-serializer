Attribute VB_Name = "ConstantStringArrays"
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

Public Type ConstantStringArray
    StringsWith5Chars() As String * 5
End Type

Public Sub MakeConstantStringArrays()
    
    Dim c1 As ConstantStringArray
    WriteToFile "CStringArr1", c1
    
    Dim c2 As ConstantStringArray
    ReDim c2.StringsWith5Chars(1)
    c2.StringsWith5Chars(0) = "01234"
    c2.StringsWith5Chars(1) = "abcde"
    WriteToFile "CStringArr2", c2
    
    Dim c3 As ConstantStringArray
    ReDim c3.StringsWith5Chars(4)
    c3.StringsWith5Chars(0) = "abcde"
    c3.StringsWith5Chars(1) = "ABCDE"
    c3.StringsWith5Chars(2) = "01234"
    c3.StringsWith5Chars(3) = "01"
    c3.StringsWith5Chars(4) = "0123456"
    WriteToFile "CStringArr3", c3
    
End Sub

