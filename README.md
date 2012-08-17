DMC12-serializer
================

A serializer that translates VB6 Universal Data Types (structs) to .NET classes and viceversa.
For a brief overview of the project please refer to: -todo-

Handles (besides the obvious stuff):
====================================

- Nested structures
- One-dimensional dynamic-length arrays
- One-dimensional dynamic-length n-based arrays
- One-dimensional fixed-length arrays
- Two-dimensional dynamic-length arrays
- Fixed-length strings
- Variants (with some limitations)

Known limitations:
==================

- Does not support VB6 classes (not serializable with Put/Get anyways)
- Circular/recursive structures are not supported (not supported by VB6)
- DateTime objects may be represented with slight differences because of floating-point rounding errors (but the resulting objects are equals to the millisecond)
- Fixed-length arrays with more than one dimension are not supported
- Dynamic arrays with more than two dimensions are not supported
- Structures and arrays in Variant fields are not deserialized

License
=======

Distributed under the MIT license. Copyright (c) 2006-2012 Francesco De Vittori, Board International SA


Usage
=====

You define a UDT in VB6:

-todo-

In .NET you define a class with the same shape as the VB6 UDT. Please note that I said 'class', not 'struct'.
Fields must be exposed as public properties, with get and set method. The class (and any referenced class) must have a public parameterless constructor.

-todo-

You serialize the VB6 UDT (Universal Data Type) with the Put method:

-todo-

You deserialize from C# using DMC12 Serializer:

-todo-

You serialize from C# using DCM12Serializer:

-todo-

You deserialize in VB6 with the Get method:

-todo-


Attributes
==========

DMC12Serializer defines a few attributes to handle special cases:

DoNotSerialize: -todo-
DynamicLength: -todo-
FixedLength: -todo-