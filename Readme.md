# Sparse Bitsets

A pure C# implementation of sparse bitsets.

Sparse bitsets are a way of storing unique integer values in very little space. Using the SparseBitset you can store integer values of up to 64-bit unsigned.

You can then use boolean operator extension methods on two SparseBitsets to filter or merge the bitsets. You can also get the count of bits in the bitset.  This makes 
SparseBitsets a useful tool for filtering and analyzing data that can be assigned into unique ids.

For example, you have a large dataset of people who like ice cream and another large set of people who play video games.  If each person has an ID, you can create two SparseBitsets, each containing the IDs of the groups of people and using the boolean operators, find out very efficiently those people who both like ice cream and also play video games.

In many cases this can be faster and more performant than SQL.  It's also possible to serialize the Bitsets so that they don't have to be created everytime. 

# Applications

* Data Analysis - filtering, counting of large number of IDs, 64-bit ids
* Facet Search - filtering a set of data by multiple properties

# Nuget

```
> Install-Package SparseBitsets
```

# Usage

SparseBitsets are initially stored in a temporary dictionary to speed up insertion.  After adding the bits / values, call `Pack()` on the bitset to make the bitsets ready for processing.

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

Once the bitsets are packed, you can use boolean operator extension methods for filtering, merging the bitsets.

```cs
    var bitset3 = bitset1.And(bitset2); // filters bitset1 by bitset 2
    var bitset4 = bitset1.Or(bitset2); // merges bitset1 with bitset 2
```

In order to perform a `Not()`, you must have the full bitset, or the complete set of unique ids.

```cs
    bitset1.Not(bitset2, fullBitset);
```

Use `GetPopCount()` to return the number of values stored in the bitset. For example, to find out the number of people who both liked ice clream and played video games:

```cs
    var bitset3 = bitset1.And(bitset2);
    bitset3.GetPopCount();
```

Use `GetPopCount()` to return the number of values stored in the bitset. For example, to find out the number of people who both liked ice clream and played video games:

```cs
    var bitset3 = bitset1.And(bitset2);
    bitset3.GetPopCount();
```

Use `GetValues()` to return the actual IDs or values stored in the bitset.

```cs
    var values = bitset3.GetValues();
    foreach(var value in values)
    {
        ///...     
    }
```