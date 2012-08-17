Attribute VB_Name = "SimpleClass"
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

Type SimpleClass
    ByteProp As Byte
    BoolProp As Boolean
    FloatProp As Single
    ShortProp As Integer
    IntProp As Long
    DoubleProp As Double
    StringProp As String
    DateTimeProp As Date
End Type


Sub MakeSimpleClasses()

    Dim c1 As SimpleClass
    c1.BoolProp = True
    c1.ByteProp = 15
    'c1.DateTimeProp = #12/2/1972 2:27:35 PM# 'Fails because of rounding errors
    c1.DoubleProp = 13.42532
    c1.FloatProp = 3.1415
    c1.IntProp = 235
    c1.ShortProp = -2562
    c1.StringProp = "foobar"
    WriteToFile7 "simple1", c1
    
    
    Dim c2 As SimpleArrays.SimpleArrays
    WriteToFile8 "simple2", c2
    
    
    Dim c3 As SimpleArrays.SimpleArrays
    ReDim c3.BoolProp(1)
    c3.BoolProp(0) = True
    c3.BoolProp(1) = False
    
    ReDim c3.ByteProp(1)
    c3.ByteProp(0) = 15
    c3.ByteProp(1) = 8
    
    ReDim c3.DateTimeProp(1)
    'c3.DateTimeProp(0) = #12/2/1972 2:27:35 PM# 'Fails because of rounding errors
    'c3.DateTimeProp(1) = #2/1/2025 3:14:15 AM# 'Fails because of rounding errors
    
    ReDim c3.DoubleProp(1)
    c3.DoubleProp(0) = 13.42532
    c3.DoubleProp(1) = -2113.42532
    
    ReDim c3.FloatProp(1)
    c3.FloatProp(0) = 3.1415
    c3.FloatProp(1) = -3.1415
    
    ReDim c3.IntProp(1)
    c3.IntProp(0) = 235352
    c3.IntProp(1) = -12422
    
    ReDim c3.ShortProp(1)
    c3.ShortProp(0) = 2562
    c3.ShortProp(1) = -2562
    
    ReDim c3.StringProp(1)
    c3.StringProp(0) = "foo"
    c3.StringProp(1) = "bar"
    
    WriteToFile8 "simple3", c3
    
End Sub
