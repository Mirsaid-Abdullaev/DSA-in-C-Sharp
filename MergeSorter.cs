using System;
namespace DSA
{
    static public class MergeSorter<T> where T : IComparable<T>
    {
        static public void Sort(T[] source)
        {
            int n = source.Length;

            for (int CurrentSize = 1; CurrentSize < n; CurrentSize *= 2)
            {
                for (int Left = 0; Left < n - 1; Left += 2 * CurrentSize)
                {
                    int Mid = Math.Min(Left + CurrentSize - 1, n - 1);
                    int Right = Math.Min(Left + 2 * CurrentSize - 1, n - 1);
                    MergeArrays(source, Left, Mid, Right);
                }
            }
            return;
        }

        static private void MergeArrays(T[] source, int left, int mid, int right)
        {
            int LeftSize = mid - left + 1;
            int RightSize = right - mid;
            T[] Left = new T[LeftSize];
            T[] Right = new T[RightSize];

            for (int i = 0; i < LeftSize; i++)
            {
                Left[i] = source[left + i];
            }
            for (int i = 0; i < RightSize; i++)
            {
                Right[i] = source[mid + 1 + i];
            }

            int pointer1 = 0;
            int pointer2 = 0;

            for (int i = left; i < (left + LeftSize + RightSize); i++)
            {
                if (pointer1 == LeftSize) //this means all elements used from left array
                {
                    source[i] = Right[pointer2];
                    pointer2++;
                    continue;
                }

                if (pointer2 == RightSize) //this means all elements used from right array
                {
                    source[i] = Left[pointer1];
                    pointer1++;
                    continue;
                }

                if (Left[pointer1].CompareTo(Right[pointer2]) < 0) //left item less than right item
                {
                    source[i] = Left[pointer1];
                    pointer1++;
                }
                else //right less than left
                {
                    source[i] = Right[pointer2];
                    pointer2++;
                }
            }
            return;
        }
    }
}
