using System;
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
        public static int[] Sort(int[] array)
        {
            var i = 1;
            var j = 2;
            var size = array.Length;
            while (i < size)
            {
                if (array[i-1] < array[i])
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

        public static int[] ParallelSortCheckTwoArrays(int[] array)
        {
            int len = array.Length;
            int[] arr1 = array.Take(len / 2).ToArray();
            int[] arr2 = array.Skip(len / 2).ToArray();

            Task a1 = Task.Run(() => Gnome.Sort(arr1));
            Task a2 = Task.Run(() => Gnome.Sort(arr2));

            a1.Wait();
            a2.Wait();

            int[] result = merge(arr1, arr2);
            return result;
        }

        public static int[] ParallelSortCheckNArrays(int[] array,int count)
        {
            List<int[]> arrays = CutArray(array, count);
            Task[] tasks = new Task[count];

            for (int i = 0; i < count; i++)
            {
                int j = i;
                tasks[j] = Task.Run(() => Gnome.Sort(arrays[j]));
            }

            Task.WaitAll(tasks);


            int[] result = mergeArraysFromList(arrays,array.Length);
            return result;
        }

        public static List<int[]> CutArray(int[] array, int count)
        {
            List<int[]> arrays = new List<int[]>();
            int partCount = array.Length / count;
            int counter = 0;

            for (int i = 0; i < count; i++)
            {
                if (i < count-1)
                {
                    arrays.Add(new int[partCount]);
                }
                else
                {
                    arrays.Add(new int[array.Length - counter]);
                }
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    arrays[i][j] = array[counter];
                    counter++;
                }
            }
            return arrays;
        }

        public static int[] SortCheck(int[] array)
        {
            var i = 1;
            var j = 2;
            var size = array.Length;
            while (i < size)
            {
                if (array[i - 1] < array[i])
                {
                    i = j;
                    j = i + 1;
                }
                else
                {
                    Swap(i - 1, i, ref array);
                    i--;
                    if (i == 0)
                    {
                        i = j;
                        j++;
                    }
                }
            }
            return array;
        }

        private static void Swap(int i, int l, ref int[] array)
        {
            array[i] = array[l] - array[i];
            array[l] -= array[i];
            array[i] += array[l];
        }

        private static int[] mergeArraysFromList(List<int[]> arrays,int length)
        {
            int[] array = arrays[0];
            for (int i = 1; i < arrays.Count; i++)
            {
                array = merge(array, arrays[i]);
            }

            return array;
        }

        private static int[] merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];

            int firstCount = 0;
            int secondCount = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (left[firstCount] < right[secondCount])
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

        private static int[] finishMerge(int[] main, int[] adit, int ind, int aditInd)
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

