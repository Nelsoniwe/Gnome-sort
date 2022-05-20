using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gnome_sort
{
    public static class Gnome
    {
        public static T[] Sort<T>(T[] array) where T : IComparable<T>
        {
            var i = 1;
            var j = 2;
            var size = array.Length;
            while (i < size)
            {
                if (array[i-1].CompareTo(array[i]) < 0)
                {
                    i = j;
                    j = i + 1;
                }
                else
                {
                    Swap(i-1,i,ref array);
                    i--;
                    if (i==0)
                    {
                        i = j;
                        j++;
                    }
                }
            }
            return array;
        }

        public static T[] ParallelSortCheckTwoArrays<T>(T[] array) where T : IComparable, IComparable<T>
        {
            int len = array.Length;
            T[] arr1 = array.Take(len / 2).ToArray();
            T[] arr2 = array.Skip(len / 2).ToArray();

            Task a1 = Task.Run<T[]>(() => Gnome.Sort(arr1));
            Task a2 = Task.Run<T[]>(() => Gnome.Sort(arr2));

            a1.Wait();
            a2.Wait();

            T[] result = merge(arr1, arr2);
            return result;
        }

        public static T[] ParallelSortCheckNArrays<T>(T[] array,int count) where T:IComparable, IComparable<T>
        {
            List<T[]> arrays = CutArray(array, count);
            Task[] tasks = new Task[count];

            for (int i = 0; i < count; i++)
            {
                int j = i;
                tasks[j] = Task.Run<T[]>(() => Gnome.Sort(arrays[j]));
            }

            Task.WaitAll(tasks);


            T[] result = mergeArraysFromList(arrays,array.Length);
            return result;
        }

        public static List<T[]> CutArray<T>(T[] array, int count)
        {
            List<T[]> arrays = new List<T[]>();
            int partCount = array.Length / count;
            int counter = 0;

            for (int i = 0; i < count; i++)
            {
                if (i < count-1)
                {
                    arrays.Add(new T[partCount]);
                }
                else
                {
                    arrays.Add(new T[array.Length - counter]);
                }
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    arrays[i][j] = array[counter];
                    counter++;
                }
            }
            return arrays;
        }

       

        private static void Swap<T>(int i, int l, ref T[] array)
        {
            (array[l], array[i]) = (array[i], array[l]);
        }

        private static T[] mergeArraysFromList<T>(List<T[]> arrays,int length) where T:IComparable
        {
            T[] array = arrays[0];
            for (int i = 1; i < arrays.Count; i++)
            {
                array = merge(array, arrays[i]);
            }

            return array;
        }

        private static T[] merge<T>(T[] left, T[] right)where T : IComparable
        {
            T[] result = new T[left.Length + right.Length];

            int firstCount = 0;
            int secondCount = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (left[firstCount].CompareTo(right[secondCount]) < 0)
                {
                    result[i] = left[firstCount];
                    firstCount++;
                }
                else
                {
                    result[i] = right[secondCount];
                    secondCount++;
                }

                if (firstCount == left.Length)
                {
                    i++;
                    return finishMerge(result, right, i, secondCount);
                }
                if (secondCount == right.Length)
                {
                    i++;
                    return finishMerge(result, left, i, firstCount);
                }
            }

            return result;
        }

        private static T[] finishMerge<T>(T[] main, T[] adit, int ind, int aditInd)
        {
            while (ind<main.Length)
            {
                main[ind] = adit[aditInd];
                ind++;
                aditInd++;
            }

            return main;
        }
    }

}

