Attribute VB_Name = "CustomBaseArrays"
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

Public Type CustomBaseArrays
    Base0() As Long
    Base2() As Long
End Type

Sub MakeCustomBoundsArrays()

    Dim c1 As CustomBaseArrays.CustomBaseArrays
    WriteToFile3 "CBounds1", c1
    
    Dim c2 As CustomBaseArrays.CustomBaseArrays
    ReDim c2.Base0(0 To 4)
    c2.Base0(0) = 0
    c2.Base0(1) = 1
    c2.Base0(2) = 2
    c2.Base0(3) = 3
    c2.Base0(4) = 4
    ReDim c2.Base2(2 To 8)
    c2.Base2(2) = 0
    c2.Base2(3) = 1
    c2.Base2(4) = 2
    c2.Base2(5) = 3
    c2.Base2(6) = 4
    c2.Base2(7) = 5
    c2.Base2(8) = 6
    WriteToFile3 "CBounds2", c2
    
    Dim c3 As CustomBaseArrays.CustomBaseArrays
    ReDim c3.Base0(0 To 1023)
    ReDim c3.Base2(2 To 1025)
    WriteToFile3 "CBounds3", c3

End Sub

