DMC12-Serializer
================

A library to help those poor souls still stuck in the dark ages of Visual Basic 6. DMC12-Serializer loads VB6 UDTs (Universal Data Types, i.e. structs) into .NET objects, and produces VB6-compatible dumps.

The project is named after the famous De Lorean from Back to the Future. For a brief overview of this project please refer to: -todo-

#### DMC12-Serializer handles (besides the obvious stuff):

- Nested structures
- One-dimensional dynamic-length arrays
- One-dimensional dynamic-length n-based arrays
- One-dimensional fixed-length arrays
- Two-dimensional dynamic-length arrays
- Fixed-length strings
- Variants (with some limitations)

#### Known limitations:

- VB6 classes are not supported (they are not serializable with Put/Get anyways)
- Circular/recursive structures are not supported (not supported by VB6)
- DateTimes may be represented with slight differences because of floating-point rounding errors (but the resulting objects are equals to the millisecond)
- Fixed-length arrays with more than one dimension are not supported
- Dynamic arrays with more than two dimensions are not supported
- Structures and arrays in Variant fields are not deserialized

#### License

Distributed under the MIT license. Copyright (c) 2006-2012 Francesco De Vittori, Board International SA

#### Basic Usage

Assuming you have this UDT in VB6:

    Type Monster
        Name as String
        Legs as Integer
        Victims() as String
        BirthDate as Date
    End Type

You first define a .NET class with the same shape. Please note that I said 'class', not 'struct'.
Fields must be exposed as public properties (in the same order) with public get and set. The class (and any referenced class) must have a public parameterless constructor.
The types must match in size: int becomes short, long becomes int, etc.

    public class Monster
    {
        public string Name { get; set; }
        public short Legs { get; set; }
        public string[] Victims { get; set; }
        public DateTime BirthDate { get; set; }
    }

You serialize the VB6 UDT with the Put method:

-todo-

In .NET, you deserialize the structure using DMC12 Serializer:

    var fs = File.OpenRead("myfile.bin");
    var serializer = new DMC12Serializer.DMC12Serializer();
    var monster = (Monster)(serializer.Deserialize(fs, typeof(Monster)));

You can pass any kind of Stream, or a byte array of you prefer.
To go in the opposite direction, you serialize from C# using DCM12Serializer:


    var fs = File.OpenWrite("myfile.bin");
    var serializer = new DMC12Serializer.DMC12Serializer();
    serializer.Serialize(fs, myMonster);

You can now load the structure in VB6 with the Get method:

-todo-


#### Attributes

DMC12Serializer defines a few attributes to handle special cases:

##### DoNotSerialize -todo-
##### DynamicLength: -todo-
##### FixedLength: -todo-
