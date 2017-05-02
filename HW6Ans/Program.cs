using System;
using System.Collections.Generic;

namespace HW6Ans
{
    internal class Program
    {
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

        public static void TransformIf<T>(ref T[] items, Func<T, T> func
            , Predicate<T> predicate)
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

        public static void Display<T>(IEnumerable<T> items) {
            foreach (T item in items) {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            int[] ints1 = new[] { 1, 2, 3, 4, -5,6 };
            String[] strings1 = new String[] { "abc", "34b", "ABDac" };
//            Console.WriteLine(Contains(strings1, "ac"));
//            Console.WriteLine(Contains(ints1, 16));
//
//            Console.WriteLine(CountIf(ints1, x => x % 2 == 0));
//            Console.WriteLine(CountIf(strings1, x => x[0] == 'a'));

//            Display(Filter(ints1, x => x % 2 == 0));
//            Display(Filter(strings1, x => x.Length > 2));

            TransformIf(ref ints1, x => 0, x => x < 0);
            TransformIf(ref strings1, x => x.Substring(0, 2), x => x.Length > 2);
            Display(strings1);
            Display(ints1);
        }
    }
}

