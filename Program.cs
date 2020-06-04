using System;
using System.Diagnostics;

namespace practica12
{
  
    class Program
    {
        static int bucketCompares = 0,heapCompares=0;
        static int bucketChanges = 0, heapChanges = 0;
        static int[] Insert(int[] mas, int insertNum)
        {
            int[] masNew = new int[mas.Length + 1];
            Array.Copy(mas, masNew,mas.Length);
            masNew[mas.Length] = insertNum;

            return masNew;
        }
        static void ShellSort(int[] array)            
        {
            int step, i, j, tmp,size=array.Length;

            for (step = size / 2; step > 0; step /= 2)
                for (i = step; i < size; i++)
                    for (j = i - step; j >= 0 && array[j] > array[j + step]; j -= step)
                    {
                        bucketCompares++;
                        bucketChanges++;
                        tmp = array[j];
                        array[j] = array[j + step];
                        array[j + step] = tmp;
                    }
        }
        static void BucketSort(int[] a)
        {
            int changes, compares;
            int[][] aux = new int[a.Length][];

            int minValue = a[0];
            int maxValue = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                if (a[i] < minValue)
                    minValue = a[i];
                else if (a[i] > maxValue)
                    maxValue = a[i];
            }
            double numRange = maxValue - minValue;

            for (int i = 0; i < aux.Length; ++i)
                aux[i] = new int[0];

            for (int i = 0; i < a.Length; ++i)
            {
                int bcktIdx = (int)Math.Floor((a[i] - minValue) / numRange * (aux.Length - 1));
                aux[bcktIdx]= Insert(aux[bcktIdx], a[i]);
                bucketChanges++;

            }

            for (int i = 0; i < aux.Length; ++i)
                ShellSort(aux[i]);

            int idx = 0;

            for (int i = 0; i < aux.Length; ++i)
            {
                for (int j = 0; j < aux[i].Length; ++j)
                {
                    bucketChanges++;
                    a[idx++] = aux[i][j];
                }
            }
        }
        static void WriteArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine();
        }
        static void HeapSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            for (int i = n - 1; i >= 0; i--)
            {

                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                heapChanges++;

                Heapify(arr, i, 0);
            }
        }


        static void Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            heapCompares++;
            if (l < n && arr[l] > arr[largest])
                largest = l;

            heapCompares++;
            if (r < n && arr[r] > arr[largest])
                largest = r;

            heapCompares++;
            if (largest != i)
            {
                heapChanges++;
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                Heapify(arr, n, largest);
            }
        }

        static void Main()
        {
           
            int size = 15;
            int num;
            int[] arr = new int[size];
            int[] arr2 = new int[size];
            Random rnd = new Random();

            for (int i = 0; i < arr.Length; ++i)
            {
                num= rnd.Next(-999, 1000);
                arr[i] = num;
                arr2[i] = num;
            }

            Console.WriteLine("Неупорядоченный:");
            Sort(arr, arr2);
            bucketCompares = 0;
            heapCompares = 0;
            bucketChanges = 0; 
            heapChanges = 0;

            //WriteArr(arr);
            //WriteArr(arr2);
            //WriteArr(arr3);

            Console.WriteLine("Упорядоченный по возрастанию:");
            Sort(arr, arr2);
            bucketCompares = 0;
            heapCompares = 0;
            bucketChanges = 0;
            heapChanges = 0;
            Array.Reverse(arr);
            Array.Reverse(arr2);
            Console.WriteLine("Упорядоченный по убыванию:");
            Sort(arr, arr2);

        }
        static void Sort(int[] arr, int[] arr2)
        {
            Stopwatch SW = new Stopwatch();
            Console.WriteLine("Bucket Sort:");
            SW.Start();
            BucketSort(arr);
            SW.Stop();
            Console.WriteLine(SW.ElapsedTicks);
            Console.WriteLine($"Сравнения:{bucketCompares} ; Перессылки:{bucketChanges}");
            Console.WriteLine();
            Console.WriteLine("Heap Sort:");
            SW.Reset();
            SW.Start();
            HeapSort(arr2);
            SW.Stop();
            Console.WriteLine(SW.ElapsedTicks);
            Console.WriteLine($"Сравнения:{heapCompares} ; Перессылки:{heapChanges}");
            Console.WriteLine();
        }
    }
}
