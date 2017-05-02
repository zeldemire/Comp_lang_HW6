﻿using System;
using System.Collections.Generic;

namespace HW6 {
    class Generics {
        public struct Point : IComparable {
            public int x, y;
            public Point(int X, int Y) {
                x = X;
                y = Y;
            }
            int IComparable.CompareTo(Object other) {
                Point pt = (Point)other;
                if (x != pt.x) {
                    return x - pt.x;
                } else {
                    return y - pt.y;
                }
            }
            public override int GetHashCode() {
                return x << 8 | y;
            }
            public override bool Equals(object obj) {
                Point other = (Point)obj;
                return x == other.x && y == other.y;
            }
            public override string ToString() {
                return String.Format("(%d, %d)", x, y);
            }
        }
        public static void Display<T>(IEnumerable<T> items) {
            foreach (T item in items) {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        public static bool NonGenericContains(IEnumerable<int> items, int value) {
            bool found = false;
            foreach (int v in items) {
                if (v == value) {
                    found = true;
                    break;
                }
            }
            return found;
        }
        public static bool NonGenericContains(IEnumerable<string> items, string value) {
            bool found = false;
            foreach (string v in items) {
                if (v == value) {
                    found = true;
                    break;
                }
            }
            return found;
        }
        public static bool NonGenericContains(IEnumerable<Point> items, Point value) {
            bool found = false;
            foreach (Point v in items) {
                if (v.Equals(value)) {
                    found = true;
                    break;
                }
            }
            return found;
        }

	    public static bool Contains<T>(IEnumerable<T> items, T value)
	    {
		    bool found = false;
		    foreach (T v in items)
		    {
			    if (v.Equals(value))
			    {
				    found = true;
				    break;
			    }
		    }
		    return found;
	    }

	    public static int CountIf<T>(IEnumerable<T> items, Predicate<T> predicate)
	    {
		    int count = 0;

		    foreach (T item in items)
		    {
			    if (predicate(item))
			    {
				    count++;
			    }
		    }

		    return count;
	    }


	    public static List<T> Filter<T>(IEnumerable<T> items, Predicate<T> predicate)
	    {
		    List<T> result = new List<T>();

		    foreach (T item in items)
		    {
			    if (predicate(item))
			    {
				    result.Add(item);
			    }
		    }

		    return result;
	    }

	    public static void TransformIf<T>(ref T[] items, Func<T, T> func, Predicate<T> predicate)
	    {
		    List<T> result = new List<T>();


		    foreach (T item in items)
		    {
			    if (predicate(item))
			    {
				    result.Add(func(item));
			    }
			    else
			    {
				    result.Add(item);
			    }
		    }

		    items = result.ToArray();
	    }
        static void Main(string[] args) {
            String[] strings1 = new String[] { "a", "b", "ac" };
            String[] strings2 = new String[] { "abc", "34b", "ABDac" };
            HashSet<string> strings3 = new HashSet<string>();
            strings3.Add("ABC");
            strings3.Add("abc");
            strings3.Add("wxyz");
            strings3.Add("wxyz");

            int[] ints1 = new int[] { 1, 2, 3, 4, -5 };
            int[] ints2 = new int[] { 1, -2, 2, -1, 10 };

            Point[] pts1 = new Point[] { new Point(2, 3), new Point(4, 3), new Point(4, 9) };

            Console.WriteLine("------------NonGenericContains------------");
            Console.WriteLine(NonGenericContains(strings1, "ac"));
            Console.WriteLine(NonGenericContains(ints1, 4));
            Console.WriteLine(NonGenericContains(ints1, 42));
            Console.WriteLine(NonGenericContains(pts1, new Point(2, 3)));
            Console.WriteLine(NonGenericContains(pts1, new Point(0, 0)));

	        Console.WriteLine("------------GenericContains------------");
	        Console.WriteLine(Contains(strings1, "ac"));
	        Console.WriteLine(Contains(ints1, 4));
	        Console.WriteLine(Contains(ints1, 42));
	        Console.WriteLine(Contains(pts1, new Point(2, 3)));
	        Console.WriteLine(Contains(pts1, new Point(0, 0)));

	        Console.WriteLine("------------CountIf------------");
	        Console.WriteLine(CountIf(strings1, x => x[0] == 'a'));
	        Console.WriteLine(CountIf(ints1, x => x % 2 == 0));
	        Console.WriteLine(CountIf(pts1, pt => pt.x == 0 && pt.y == 0));

	        Console.WriteLine("------------Filter------------");
	        Display(Filter(ints1, x => x % 2 == 0));		// get even values
	        Display(Filter(ints2, x => x % 2 == 0));		// get even values
	        Display(Filter(strings2, x => x.Length > 2));	// get strings with more than 2 chars
	        Display(Filter(strings1, x => x.Length > 2));

	        Console.WriteLine("------------TransformIf------------");
	        Display(ints2);
	        TransformIf(ref ints2, x => 0, x => x < 0);			// replace negative elements with 0
	        Display(ints2);
	        Display(strings2);
	        TransformIf(ref strings2, x => x.Substring(0, 2), x => x.Length > 2);	// Truncate long strings
	        Display(strings2);

/***************************
			Your code additions above should allow the following code to run properly

			// Contains should be a generic function that works for enumerable collections of int,
			// double, float, short, objects, etc. The second parameter should be a value whose
			// type is the same as the objects in the collection. The function should return
			// true iff the value is in the collection.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------Contains------------");
			Console.WriteLine(Contains(strings1, "ac"));
			Console.WriteLine(Contains(ints1, 4));
			Console.WriteLine(Contains(ints1, 42));
			Console.WriteLine(Contains(pts1, new Point(2, 3)));
			Console.WriteLine(Contains(pts1, new Point(0, 0)));

			// IsSorted should be a generic function that works for arrays of int, double, float, short,
			// objects, etc. It should return true if each array element is >= to its predecessor.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------IsSorted------------");
			Console.WriteLine(IsSorted(strings1));
			Console.WriteLine(IsSorted(ints1));
			Console.WriteLine(IsSorted(pts1));

			// CountIf should be a generic function that works for enumerable collections of int,
			// double, float, short, objects, etc. The second parameter should be a predicate (i.e.,
			// a function that return true/false) that accepts a single parameter whose type is
			// the same as the items in the collection. This function returns the number of items
			// in the collection that satisfy the predicate.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------CountIf------------");
			Console.WriteLine(CountIf(strings1, x => x[0] == 'a'));
			Console.WriteLine(CountIf(ints1, x => x % 2 == 0));
			Console.WriteLine(CountIf(pts1, pt => pt.x == 0 && pt.y == 0));

			// Filter should be a generic function that works for enumerable collections of int,
			// double, float, short, objects, etc. The second parameter should be a predicate (i.e.,
			// a function that return true/false) that accepts a single parameter whose type is
			// the same as the items in the collection. This function returns a List containing
			// the items that satsify the predicate.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------Filter------------");
			Display(Filter(ints1, x => x % 2 == 0));		// get even values
			Display(Filter(ints2, x => x % 2 == 0));		// get even values
			Display(Filter(strings2, x => x.Length > 2));	// get strings with more than 2 chars
			Display(Filter(strings1, x => x.Length > 2));

			// A common operation is to combine two vectors having the same length. For example,
			// the "dot product" between two vectors is defined be: 1. multiply the corresponding
			// pairs of array elements 2. add up all the products produced in step 1.
			// For example: The dot product of [1,2,3] and [3,2,5] is 1*3 + 2*2 + 3*5 = 22
			//
			// This type of operation can be generalized by viewing it as having two replaceable functions.
			// One function is used to combine the corresponding array elements (i.e., *).
			// The other function is then used to combine those individual results.
			//
			// SumProduct should be a generic function that works for arrays of int, double, float, short,
			// objects, etc. It should return a value having the same type as the array elements.
			// The first two arguments of the function are the two vectors. The third parameter is the
			// operation used to combine the individual results. The fourth parameters is applied to the
			// corresponding array elements.
			//
			// This function should throw an exception if the arrays are not suitable for this operation.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------SumProduct------------");
			Console.WriteLine(SumProduct(ints1, ints2, (x, y) => x + y, (x, y) => x * y));		// Typical dot product
			Console.WriteLine(SumProduct(ints1, ints2, (x, y) => x + y, Math.Max));
			Console.WriteLine(SumProduct(strings1, strings2, (x, y) => x + y, (x, y) => x.CompareTo(y) <= 0 ? x : y));

			// TransformIf should be a generic function that works for an array of int,
			// double, float, short, objects, etc. The second parameter should be function
			// to be applied to certain, individual, elements of the array. For those array
			// elements that satisfy the predicate, that array position should be replaced
			// with the result of the function applied to that element.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------TransformIf------------");
			Display(ints2);
			TransformIf(ints2, x => 0, x => x < 0);			// replace negative elements with 0
			Display(ints2);
			Display(strings2);
			TransformIf(strings2, x => x.Substring(0, 2), x => x.Length > 2);	// Truncate long strings
			Display(strings2);
***************************/
        }
    }
}