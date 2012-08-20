DMC12-Serializer
================

A library to help those poor souls still stuck in the dark ages of Visual Basic 6. DMC12-Serializer loads VB6 UDTs (Universal Data Types, i.e. structs) into .NET objects, and produces VB6-compatible dumps.

The project is named after the famous De Lorean from Back to the Future. [Here](http://www.frenk.com/todo) is how the project was born.

#### DMC12-Serializer handles (besides the obvious stuff):

- Nested structures
- One-dimensional dynamic-length arrays
- One-dimensional dynamic-length n-based arrays
- One-dimensional fixed-length arrays
- Two-dimensional dynamic-length arrays
- Fixed-length strings
- Variants (with some limitations)

#### Known limitations:

- VB6 classes are not supported (not supported by VB6 Put/Get anyways)
- Circular/recursive structures are not supported (again, not supported by VB6)
- DateTime values may be represented with slight differences at byte-level because of floating-point rounding errors, but the resulting DateTimes are equals to the millisecond
- Fixed-length arrays with more than one dimension are not supported
- Dynamic arrays with more than two dimensions are not supported
- Structures and arrays in Variant fields are not deserialized

#### License

Distributed under the MIT license. Copyright (c) 2006-2012 Francesco De Vittori, Board International SA

#### Basic Usage

First, add a reference to DMC12Serializer.dll with [NuGet](http://nuget.org/packages/DMC12Serializer) (or build it from source):

    PM> Install-Package DMC12Serializer

Assuming you start with this UDT in VB6:

    Type Monster
        Name as String
        Legs as Integer
        Victims() as String
        BirthDate as Date
    End Type

You first define a .NET class with the same shape. Please note that I said 'class', not 'struct'.
Fields must be exposed as public properties (in the same order) with public get and set. Properties without a setter are ignored. Properties are taken in order, so it is not important that names match the VB6 fields, but *it is essential that they are in the same order*.
The types must match in size: int becomes short, long becomes int, etc.
The class (and any referenced classes) must also have a public parameterless constructor. 

    public class Monster
    {
        public string Name { get; set; }
        public short Legs { get; set; }
        public string[] Victims { get; set; }
        public DateTime BirthDate { get; set; }
    }

At this point you can serialize the VB6 UDT with the Put method:

    Dim FileNum As Integer
    FileNum = FreeFile
    Open "c:\myDump.bin" For Binary Access Write Shared As FileNum
      Put #FileNum, 1, FileContents
    Close #FileNum

In .NET, you deserialize the structure using DMC12-Serializer:

    using (var fs = File.OpenRead(@"c:\myDump.bin"))
    {
        var buffer = new byte[fs.Length];
        fs.Read(buffer, 0, buffer.Length);
        new DMC12Serializer.DMC12Serializer().Deserialize(typeof(Monster), buffer);
    }

Please notice that the current version of the deserializer needs the full dump in memory (which is stupid), but it will be an easy fix for future releases.

Now you can go in the opposite direction: you serialize using DMC12-Serializer:

    using (var fs = File.Create(@"c:\myDump.bin"))
    {
        new DMC12Serializer.DMC12Serializer().Serialize(myMonster, fs);
    }

and load the structure in VB6 with Get:

    Dim FileNum As Integer
    Dim myMonster As Monster
    Open "c:\myDump.bin" For Binary Access Read As #FileNum
    Get #FileNum, 0, myMonster


#### Attributes

DMC12Serializer defines a few attributes to handle special cases:

##### DoNotSerialize

Use the DoNotSerialize attribute no properties that have no match in the VB6 UDT and must be ignored during serialization/deserialization:

    public class Monster
    {
        public string Name { get; set; }
        
        [DoNotSerialize]
        public double Height { get; set; }
        
        public short Legs { get; set; }
        public string[] Victims { get; set; }
        public DateTime BirthDate { get; set; }
    }


##### DynamicLength

Use the DynamicLength attribute when the VB6 field is an array with a base different than 0:

    Public Type MyUDT
        SomeArray() As Long
    End Type
    
    Dim myObj as MyUDT
    ReDim myObj.SomeArray(2 to 10)

and in C#:

    public class MyUDT
    {
        [DynamicLength(2)
        public int[] SomeArray { get; set; }
    }

##### FixedLength

Use the FixedLength attribute when the VB6 field is an array with a fixed length. Please notice that in VB6 you specify the upper bound (i.e. the last index), while in the FixedLengthAttribute you specify the array size.

    Public Type MyUDT
        SomeArray(24) As Long
    End Type

and in C#:

    public class MyUDT
    {
        [FixedLength(23)
        public int[] SomeArray { get; set; }
    }
    
##### ConstantLengthString

Use the ConstantLengthString attribute when a VB6 field is a constant-length string, or an array of constant-length strings:

    Public Type MyUDT
        SomeStr As String * 8
        SomeStrings() As String * 255
    End Type
    
and in C#:

    public class MyUDT
    {
        [ConstantLengthString(8)]
        public string SomeStr { get; set; }
        
        [ConstantLengthString(255)]
        public string[] SomeStrings { get; set; }
    }