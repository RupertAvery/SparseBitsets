
#  Introduction

A bit is a binary value, represented by 0 or 1.

We are familiar with bits as a collection of 8, 16, 32, 64, 128 bits and so on.

We know that a 8-bit number can represent one of 256 values, 0 to 255, or `00000000` to `11111111`. The number 170 for example is `10101010` in binary, i.e. the *bit pattern* `10101010` represents, or is equal to 170.

A larger number of bits can represent a larger range of values.

# A Primer on Binary Numbers

You are probably familiar with base 10. For example, the number 170. But what *is* 170? 

We're taught in school that 170 consists of 3 digits: 1, 7, 0. They are put together by placing them in a place value, ones, tens, hundreds. Taking into account their place value give you the numerical value, "170". But how does this work exactly?  Let's consider the digits in their place value:

| Hundreds | Tens | Ones |
|:--------:|:----:|:----:|
|     1    |  7   |   0  |

Let's replace the place values with their numeric values:

|    100   |  10  |   1  |
|:--------:|:----:|:----:|
|     1    |   7  |   0  |

Now, to get the total value, we sum the products of the place value and the digit.

```
  100 * 1 =  100 
+  10 * 7 =   70 
+   1 * 0 =    0
          =  170  
```

Let's replace the place values with their equivalent powers of 10. We use powers of ten because we have 10 base digits: 0-9. Every power of 10 means 10 of the previous place value. 10 is ten 1's, 100 is ten 10's, and so on.

```
  (10 ^ 2) * 1 =  100 
+ (10 ^ 1) * 7 =   70
+ (10 ^ 0) * 0 =    0
                = 170  
```

In a binary number, instead of 10 base digits, there are only 2: 0 and 1.

We can still use base 2 to represent any integer. Instead of the place values being powers of 10, we use powers of 2:

```
  (2 ^ 7) * 1 =  128 
+ (2 ^ 6) * 0 =    0
+ (2 ^ 5) * 1 =   32
+ (2 ^ 4) * 0 =    0
+ (2 ^ 3) * 1 =    8
+ (2 ^ 2) * 0 =    0
+ (2 ^ 1) * 1 =    2
+ (2 ^ 0) * 0 =    0
              =  170
```

# Bitsets

A bitset or bit array is a collection of bits of arbitary length.

In our previous example, the bit pattern `10101010` is used to represent a single number: 170.

However, we don't use bitsets to represent a single number. Instead, we use bitset to represent a list of numbers.  How does that work? Let's use the bit pattern `10101010` again:

```
 (2 ^ 7) * 1  
 (2 ^ 6) * 0 
 (2 ^ 5) * 1 
 (2 ^ 4) * 0 
 (2 ^ 3) * 1 
 (2 ^ 2) * 0 
 (2 ^ 1) * 1 
 (2 ^ 0) * 0 
```

Let's rewrite this as a table:

| 2 ^ 7 | 2 ^ 6 | 2 ^ 5 | 2 ^ 4 | 2 ^ 3 | 2 ^ 2 | 2 ^ 1 | 2 ^ 0 |
|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|
|   1   |   0   |   1   |   0   |   1   |   0   |   1   |   0   |   

And now without the base of 2, leaving only the powers:

|   7   |   6   |   5   |   4   |   3   |   2   |   1   |   0   |
|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|
|   1   |   0   |   1   |   0   |   1   |   0   |   1   |   0   |   

Instead of powers, let's call them bit positions where bit `n` is the `n + 1`th bit in the bit pattern. Therefore bit 7 is the 8th bit, and bit 0 is the 1st bit.

If we consider only the bit positions where there is a bit value of 1, we can then say that the bit pattern `10101010` contains the bit positions `7,5,3,1`.

Likewise the bit pattern `11111111` contains the bit positions `7,6,5,4,3,2,1,0`. Or if you want, `0,1,2,3,4,5,6,7`

An 8-bit number can store 8 bit positions, and a 64-bit number can store 64 bit positions.  So, instead of a series of bits representing one number, we can use it to store a list of (unique) numbers.

So somehow the value `170` can represent the numbers `7,5,2,1`. Don't think about this too hard, it works better in binary. Just know that an n-bit value can store n numbers.

How do we do this? How do we "store" a list of numbers into an single value? And how do we know what a bitset contains?

# Bitsets in code

Before we continue, lets define how we are going to represent bitsets in code.  We obviosuly need to store these bits in a variable.

Let's look at the data type we have in C#:

| Data Type  | Min Value                    | Max Value                    | Number of Bits |
|------------|------------------------------|------------------------------|-----------------|
| sbyte      | -128                         | 127                          | 8               |
| byte       | 0                            | 255                          | 8               |
| short      | -32,768                      | 32,767                       | 16              |
| ushort     | 0                            | 65,535                       | 16              |
| int        | -2,147,483,648               | 2,147,483,647                | 32              |
| uint       | 0                            | 4,294,967,295                | 32              |
| long       | -9,223,372,036,854,775,808   | 9,223,372,036,854,775,807    | 64              |
| ulong      | 0                            | 18,446,744,073,709,551,615   | 64              |

For our purposes, we are only interested in positive integers. The signed data types use the last bit as the sign (0 = positive, 1 = negative), so we don't want to use them, since we can use the last bit as a bit position rather than the sign. So, we'll focus on the unsigned data types.


| Data Type  | Min Value                    | Max Value                    | Number of Bits |
|------------|------------------------------|------------------------------|-----------------|
| byte       | 0                            | 255                          | 8               |
| ushort     | 0                            | 65,535                       | 16              |
| uint       | 0                            | 4,294,967,295                | 32              |
| ulong      | 0                            | 18,446,744,073,709,551,615   | 64              |

For the purpose of our discussion (and for brevity) we will use 8 bits as the storage for our bitsets, in the examples, we will be using var for simplicity, and the number literals are of course `int` by default, but consider them as holding 8-bit values.

# Storing Values in a Bitset

To add a value to a bitset, we need to set the bit position in the bitset to 1.

To do this, we get the *value* of the bit position (2 ^ n) by itself, then perform a bitwise OR with the bitset, and store the result back into the bitset. 

For example, to set bit 7 to 1, we first need to get the value of bit 7, which is 2 ^ 7 or 128. 

```cs
var value = 1;           //        00000001
value = value | 128;     // 128 =  10000000
                         // 129 =  10000001  result of bitwise OR
```

But, we don't want to use hardcoded values like 128 when we want to set bit positions that are numbers, like 7.  We want to use the bit position itself. So how can we replace 128 with the bit position 7? It would be nice to use it as a power of 2 so we could write:

```cs
var value = 1;           //        00000001
var bitPosition = 7;
var place = 2 ^ bitPosition;  // This an XOR!
value = value | place; 
```

Unfortunately, C# uses the operator ^ for bitwise XOR. It would be tempting to use `Math.Power`, but that returns a `double`, and it's not very performant for our purposes.

Fortunately for us, we can use **bit shifting** to get powers of 2. In C#, the shift left operator `a << n` shifts the bit pattern `a` `n` times to the left. Each shift is equal to multiplying by a power of 2.

For example, if we have the value 1, and shift it 3 times to the left, it will be the same as 1 * 2 ^ 3 or 8.

```
00000001   = 1
(shift left 3 times)
00000010   = 2   
00000100   = 4
00001000   = 8
```

The great news is that CPUs have this function built in, as in there are circuits that can shift bits.

```cs
var value = 1;                   // 00000001
var bitPosition = 7;
var place = 1 << bitPosition;    // 10000000
value = value | place;           // 10000001
```

Writing this into a function we have:

```cs
public int SetValues(int initialValue, IEnumerable<int> values) 
{
    var value = initialValue;

    foreach(var bitPosition in values) 
    {
        var place = 1 << bitPosition;    
        value = value | place;           
    }

    return value;
}

var result = SetValues(0, new []{ 0, 7 });    //129
```

So the value `129` stores the numbers `0` and `7`.  How do we get the numbers back out of the value?

# Retrieving Values from a Bitset

To retrieve the values, we need to check if each bit position is set to 1. To do this, we need to bitwise AND the bit position place value with the value we're checking.

If the result is equal to the place value, then we know the bit is set to 1.

```
129 = 10000001
128 = 10000000   (2 ^ 7)
      10000000   // 129 & 128 = 128 
```

therefore `129` has bit 7 set

```
129 = 10000001
 16 = 00010000   (2 ^ 4)
      00000000   // 129 & 16 = 0 
```
therefore `129` does NOT have bit 4 set

In code, this would be

```cs
var value = 129;
var bitPosition = 7;
var place = 1 << bitPosition;    
var result = value & place;
if (result == place) { 
    // bitPosition is set
}
```

And as a function:

```cs
public IEnumerable<int> GetValues(int value) 
{
    for(var bitPosition = 0; bitPosition <= 7; bitPosition++) 
    {
        var place = 1 << bitPosition;    
        if((value & place) == place) 
        {
            yield return bitPosition;       
        }
    }
}

GetValues(129);  // { 0, 7 }
```

# Bitsets as Sets

It's obvious from the name, but what do we mean when we say "set"?

A set is a collection of items, or elements. For example, the numbers `1,3,5,7` is a set.

The numbers `5,6,7` is also a set.  

We define an intersection of two sets as a set containing the elements that are found in both sets.  The intersection of `1,3,5,7` and `5,6,7` is `5,7`.

Bitsets contain a collection of unique numbers, so we can think of them as  sets. 

To get the intersection of two bitsets, we can apply the bitwise AND (&) operator to them.

```
  10101010    // 1,3,5,7
& 11100000    // 5,6,7
  10100000    // 1,3,5,7 AND 5,6,7 = 5,7  
```

Likewise, the union of two sets is a set that contails all the unique elements of both sets. The union  of `1,3,5,7` and `5,6,7` is `1,3,5,6,7`.

To get the union of two bitsets, we can apply the bitwise OR (|) operator to them:

```
  10101010    // 1,3,5,7
| 11100000    // 5,6,7
  11101010    // 1,3,5,7 OR 5,6,7 = 1,3,5,6,7 
```

As you can see, we can do set operations on bitsets.

# Counting Bits

One final thing, before we delve deeper. It's important to be able to count how many elements there are in a set.

To do so, we would usually loop over each bit and check if it is set like in `GetValues`, but there are bit manipulation tricks that allow us to count the bits that are set to 1.

```cs
public static int CountSetBits(ulong x)
{
      unchecked
      {
            x -= (x >> 1) & 0x5555555555555555UL;
            x = (x & 0x3333333333333333UL) + ((x >> 2) & 0x3333333333333333UL); 
            x = (x + (x >> 4)) & 0x0F0F0F0F0F0F0F0FUL; 
            return (int)((x * 0x0101010101010101UL) >> 56);
      }
}
```