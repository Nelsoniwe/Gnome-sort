using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gnome_sort
{
    class Program
    {
        static void Main(string[] args)
        {


            int[] c = new int[100];
            int[] d = new int[500];
            int[] e = new int[1000];

            var aa = new Random();

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = aa.Next(-c.Length, c.Length); //fill array with random numbers
            }
            for (int i = 0; i < d.Length; i++)
            {
                d[i] = aa.Next(-d.Length, d.Length); //fill array with random numbers
            }
            for (int i = 0; i < e.Length; i++)
            {
                e[i] = aa.Next(-e.Length, e.Length); //fill array with random numbers
            }

            Console.WriteLine();
            Console.WriteLine("Array 1:");
            for (int i = 0; i < c.Length; i++)
            {
                Console.Write($"{c[i]} ");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Array 2:");
            for (int i = 0; i < d.Length; i++)
            {
                Console.Write($"{d[i]} ");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Array 3:");
            for (int i = 0; i < e.Length; i++)
            {
                Console.Write($"{e[i]} ");
            }
            Console.WriteLine("\n");

            

            c =Gnome.ParallelSortCheckNArrays(c, 10);
            d=Gnome.ParallelSortCheckNArrays(d, 50);
            e=Gnome.ParallelSortCheckNArrays(e, 100);

            Console.WriteLine();
            Console.WriteLine("Array 1 after sort with 10 tasks:");
            for (int i = 0; i < c.Length; i++)
            {
                Console.Write($"{c[i]} ");
            }
            Console.WriteLine();
            Console.WriteLine("Array 2 after sort with 50 tasks:");
            for (int i = 0; i < d.Length; i++)
            {
                Console.Write($"{d[i]} ");
            }
            Console.WriteLine();
            Console.WriteLine("Array 3 after sort with 100 tasks:");
            for (int i = 0; i < e.Length; i++)
            {
                Console.Write($"{e[i]} ");
            }
            Console.WriteLine("\n");


            Console.Write("Array 1:");
            if (Gnome.IsArraySorted(c))
                Console.WriteLine("Sorted");
            else
                Console.WriteLine("Not Sorted");
            Console.WriteLine("\n");
            Console.Write("Array 2:");
            if (Gnome.IsArraySorted(d))
                Console.WriteLine("Sorted");
            else
                Console.WriteLine("Not Sorted");

            Console.WriteLine("\n");
            Console.Write("Array 3:");
            if (Gnome.IsArraySorted(e))
                Console.WriteLine("Sorted");
            else
                Console.WriteLine("Not Sorted");

            Console.WriteLine();

            int[] bigArray = new int[5000000];


            for (int i = 0; i < c.Length; i++)
            {
                bigArray[i] = aa.Next(-bigArray.Length, bigArray.Length); //fill array with random numbers
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Gnome.ParallelSortCheckNArrays(bigArray, 200);
            watch.Stop();
            Console.WriteLine($"Time in Milliseconds: {watch.ElapsedMilliseconds}");
            Console.Write("Array is:");
            if (Gnome.IsArraySorted(e))
                Console.WriteLine("Sorted");
            else
                Console.WriteLine("Not Sorted");

            //int count = 5; //count of iterations
            //int taskCount = 12; // count of array slices
            //int size = 10; // size of array

            //int[] array = new int[size]; //size of array
            //List<long> allTimes = new List<long>();
            //List<long> allTimesParallel = new List<long>();

            //for (int l = 0; l < count; l++)
            //{
            //    var a = new Random();
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        array[i] = a.Next(-array.Length, array.Length); //fill array with random numbers
            //    }

            //    Stopwatch stopWatch = new Stopwatch(); //stopwatch
            //    stopWatch.Start();
            //    array = Gnome.Sort(array);
            //    stopWatch.Stop();
            //    allTimes.Add(stopWatch.ElapsedMilliseconds);
            //}

            //long medianCheck = allTimes[0];
            //for (int i = 1; i < allTimes.Count; i++)
            //{
            //    medianCheck += allTimes[i];
            //    medianCheck /= 2;
            //}


            //Console.WriteLine($"Час виконання послідовного алгоритму: {medianCheck}");

            //for (int l = 0; l < count; l++)
            //{
            //    var a = new Random();
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        array[i] = a.Next(-array.Length, array.Length); //fill array with random numbers
            //    }

            //    Stopwatch stopWatch = new Stopwatch(); //stopwatch
            //    stopWatch.Start();
            //    array = Gnome.ParallelSortCheckNArrays(array, taskCount);
            //    stopWatch.Stop();
            //    allTimesParallel.Add(stopWatch.ElapsedMilliseconds);
            //}

            //medianCheck = allTimesParallel[0];
            //for (int i = 1; i < allTimesParallel.Count; i++)
            //{
            //    medianCheck += allTimesParallel[i];
            //    medianCheck /= 2;
            //}

            //Console.WriteLine($"Час виконання паралельного алгоритму використовуючи {taskCount} потоків: {medianCheck}");
        }
    }
}
