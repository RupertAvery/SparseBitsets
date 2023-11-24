# SparseBitsets

A pure C# implementation of sparse bitsets.

# Introduction

A bitset, or bit array, is a data structure for storing a bit "string" or a series of bits of arbitrary length.  

While we are familiar with bits representing numbers, such as an 8-bit number being able to store the values 0-255, bit arrays are not so much used to store a value, rather to store a list of values.

For example, a bitset containing 8 bits with the bit pattern `10101010` has a 1 in the bit positions `1,3,5,7`, so this bit pattern contains the values `1,3,5,7`.

A byte can hold 8 bits, a long can store 64 bits. To store more than 64 bits, we need to use an array of bytes, or longs.


A bitset can contain hundreds or thousands, or tens of thousands of bits. To do this, we can store arrays of bit patterns

A sparse bitset is a special implementation of bitset where 

See [Introduction](docs/introduction.md)

# Applications

* [Survey Analysis](docs/survey-analysis.md) 
* [Faceted Search](docs/faceted-search.md)
* [Dynamic Filtering](docs/dynamic-filtering.md)

# Nuget

```ps1
> Install-Package SparseBitsets
```


# Usage

Values in a `SparseBitset` are initially stored in a temporary dictionary to simplify insertion of values.  After adding the values, call `Pack()` on the bitset to optimize it.

```cs
var bitset = new SparseBitset();

for(int i = 1000; i < 2000; i++){
    bitset.Add(i);
}

for(int i = 1000000; i < 1002000; i++){
    bitset.Add(i);
}

for(int i = 6000000; i < 6002000; i++){
    bitset.Add(i);
}

bitset.Pack();
```

# Set Operations

Once the bitsets are packed, you can use the set operation extension methods.

## Intersection

To get the intersection of two bitsets, call the `And` method on one bitset, passing the other bitset as the argument. An intersection will return a new bitset containing the values that overlap or only exist in both bitsets.

Since intersection is commuative, the order does not matter.

```cs
bitset1.And(bitset2); // filters bitset1 by bitset 2
bitset2.And(bitset1); // same result
```

## Union

To get the union of two bitsets, call the `Or` method on one bitset, passing the other bitset as the argument.  A union will return a new bitset containing the values of both bitsets combined.

Since union is commuative, the order does not matter.

```cs
bitset1.Or(bitset2); // merges bitset1 with bitset 2
bitset2.Or(bitset1); // same result
```

## Complement

A complement is the inverse of a bitset, where all the valid values are inverted, e.g. 0 is set to 1 or 1 is set to 0.

In order to perform a complement, you must have a full bitset, or the complete set of values. This bitset must have bits set to 1 for each valid value in the set. This is because a real-world bitset may start and end at arbitrary values, and may have gaps in between values e.g. unused values. These unused values must be set to zero, as the position they occupy does not represent a member of the set.

For example, the full bitset of a set with 8 values starting from 0 is `11111111`.

The full bitset of a set with 7 values starting from 0 is `01111111` (position 7 is not a valid value or part of the set).

The full bitset of a set with 10 values starting from 7 is `00000001 11111111 10000000`.

It is recommended to build this full bitset once and cache it.

To get the complement of two bitsets, call the `Not` method on one bitset, passing the other bitset and the full bitset as arguments. Complement is not commutative.

```cs
bitset1.Not(bitset2, fullBitset);
```

It is up to your implementation to ensure that for your set, the values you use fall are valid.  To validate whether a value exists in a set, you can do this:

```cs
// assuming fullBitset already contains the full set
var testSet = new SparseBitset();
testSet.Add(testValue);  // Add one or more values to test

var result = fullBitset.And(testSet);

// Result should have the same values as testSet
```

# Count or Population

It is very useful to know the population of a bitset - the number of bits that ar set to 1 - or the number of members in a group.

To get the population count of two bitsets, call the `GetPopCount()` method on the bitset.

```cs
bitset3.GetPopCount();
```

## Values

To get the values in a bitset, call the `GetValues` method on the bitset. This will return an `IEnumerable<ulong>`.

```cs
var values = bitset3.GetValues();

foreach(var value in values)
{
    ///...     
}
```
