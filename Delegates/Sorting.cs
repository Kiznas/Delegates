using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UserInfo;

namespace Delegates
{
    internal class Program
    {
        public static IEnumerable<T> Sort<T>(IEnumerable<T> source, Func<T, T, int> comparer)
        {
            var items = source.ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (comparer(items[i], items[j]) > 0)
                    {
                        Swap(ref items[i], ref items[j]);
                    }
                }
            }

            IEnumerator<T> iEnumerator = items.Take(items.Count()).GetEnumerator();

            while (iEnumerator.MoveNext())
            {
                yield return iEnumerator.Current;
            }
        }

        static private void Swap<T>(ref T x, ref T y)
        {
            var temp = x;
            x = y;
            y = temp;
        }

        static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
        {
            foreach (var item in source)
            {
                if (filter(item))
                {
                    yield return item;
                }
            }
        }

        public static void RepeateCode(Action<int> action, Func<bool> condition, int timesCount, int whatToPrint)
        {
            int repeated= 0;

            while (condition() && ++repeated <= timesCount)
            {
                action(whatToPrint++);
            }
        }

        //public static void whatToPrint(int i)
        //{
        //    Console.WriteLine(i);
        //}



        static void Main(string[] args)
        {
            

            User[] users = new User[3]
            {
            new User("Vasya", "Pupkin", DateTime.Now, "123"),
            new User("Kolia", "Pechkin", DateTime.Now, "321"),
            new User("Sergey", "Andreyev", DateTime.Now, "222"),
            };
            var sorted = Sort(users, (x, y) => x.LastName.CompareTo(y.LastName));
            foreach (var item in sorted)
            {
                Console.WriteLine(item);
            }
            var selection = Filter(users, t => t.PhoneNumber == "123").ToList();

            Console.WriteLine(" ");

            foreach (var item in selection)
            {
                Console.WriteLine(item);
            }
            Func<bool> condition = new Func<bool>(() => true);


            RepeateCode(Console.WriteLine, condition, 5, 10);
        }
    }
}