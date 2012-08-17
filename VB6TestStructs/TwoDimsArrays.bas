Attribute VB_Name = "TwoDimsArrays"
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

Type TwoDimsArrays
    ByteProp() As Byte
    BoolProp() As Boolean
    FloatProp() As Single
    ShortProp() As Integer
    IntProp() As Long
    DoubleProp() As Double
    StringProp() As String
    DateTimeProp() As Date
End Type


Sub MakeTwoDimsArrays()

    Dim c As TwoDimsArrays
    WriteToFile9 "TwoDims1", c
    
    Dim c1 As TwoDimsArrays
    
    ReDim c1.BoolProp(1, 2)
    c1.BoolProp(0, 0) = False
    c1.BoolProp(0, 1) = False
    c1.BoolProp(0, 2) = True
    c1.BoolProp(1, 0) = True
    c1.BoolProp(1, 1) = True
    c1.BoolProp(1, 2) = False
    
    ReDim c1.ByteProp(1, 2)
    c1.ByteProp(0, 0) = 14
    c1.ByteProp(0, 1) = 251
    c1.ByteProp(0, 2) = 22
    c1.ByteProp(1, 0) = 15
    c1.ByteProp(1, 1) = 252
    c1.ByteProp(1, 2) = 23
    
    ReDim c1.DateTimeProp(1, 0)
    c1.DateTimeProp(0, 0) = #1/1/2010#
    c1.DateTimeProp(1, 0) = #8/8/2010#
    
    ReDim c1.DoubleProp(1, 2)
    c1.DoubleProp(0, 0) = 14.444
    c1.DoubleProp(0, 1) = 251
    c1.DoubleProp(0, 2) = 22
    c1.DoubleProp(1, 0) = 15.32
    c1.DoubleProp(1, 1) = 252
    c1.DoubleProp(1, 2) = 23
    
    ReDim c1.FloatProp(1, 2)
    c1.FloatProp(0, 0) = 14.444
    c1.FloatProp(0, 1) = 251
    c1.FloatProp(0, 2) = 22
    c1.FloatProp(1, 0) = 15.32
    c1.FloatProp(1, 1) = 252
    c1.FloatProp(1, 2) = 23
    
    ReDim c1.IntProp(1, 2)
    c1.IntProp(0, 0) = 14
    c1.IntProp(0, 1) = 251
    c1.IntProp(0, 2) = 22
    c1.IntProp(1, 0) = 15
    c1.IntProp(1, 1) = 252
    c1.IntProp(1, 2) = 23
    
    ReDim c1.ShortProp(1, 2)
    c1.ShortProp(0, 0) = 14
    c1.ShortProp(0, 1) = 251
    c1.ShortProp(0, 2) = -22
    c1.ShortProp(1, 0) = 15
    c1.ShortProp(1, 1) = -252
    c1.ShortProp(1, 2) = 23
    
    ReDim c1.StringProp(1, 2)
    c1.StringProp(0, 0) = "foo"
    c1.StringProp(0, 1) = "bar"
    c1.StringProp(0, 2) = "foobar"
    c1.StringProp(1, 0) = "foo2"
    c1.StringProp(1, 1) = "bar2"
    c1.StringProp(1, 2) = "foobar2"
    
    WriteToFile9 "TwoDims2", c1

End Sub

