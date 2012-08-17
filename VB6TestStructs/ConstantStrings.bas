Attribute VB_Name = "ConstantStrings"
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

Public Type ConstantStringLengthProperty
    StringWith5Chars As String * 5
End Type

Sub MakeConstantStrings()

    Dim c1 As ConstantStringLengthProperty
    WriteToFile2 "CString1", c1
    
    Dim c2 As ConstantStringLengthProperty
    c2.StringWith5Chars = ""
    WriteToFile2 "CString2", c2
    
    Dim c3 As ConstantStringLengthProperty
    c3.StringWith5Chars = "01234"
    WriteToFile2 "CString3", c3
    
    Dim c4 As ConstantStringLengthProperty
    c4.StringWith5Chars = "0123456"
    WriteToFile2 "CString4", c4

End Sub

