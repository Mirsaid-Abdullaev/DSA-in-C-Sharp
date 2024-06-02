using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Sorts
{
    /// <summary>
    /// Static class that contains the .Sort() method to sort the given array. There is a sort function and a sort procedure,
    /// depending on whether the original array or list needs to be kept or overwritten.
    /// </summary>
    /// <typeparam name="T">The data type of the parameters to pass in to use for sorting</typeparam>
    internal static class MergeSorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Performs a merge sort on the input array in-place and overwrites the original array with the sorted values
        /// </summary>
        /// <param name="Source">The array of values to be sorted</param>
        static public void Sort(T[] Source)
        {
            int n = Source.Length; //stores array length

            for (int CurrentSize = 1; CurrentSize < n; CurrentSize *= 2) //iterator that specifies what the current sublist size is (powers of 2 ascending)
            {
                for (int Left = 0; Left < n - 1; Left += 2 * CurrentSize) //iterator to go through every "Left" index based on the current size, to get the range of 2 arrays to merge
                {
                    int Mid = Math.Min(Left + CurrentSize - 1, n - 1);
                    //middle is either Left + CurrentSize - 1, or (n - 1), whichever is smaller - to prevent indexing out of bounds errors
                    int Right = Math.Min(Left + 2 * CurrentSize - 1, n - 1);
                    //right is either Left + CurrentSize * 2 - 1 or (n - 1), whichever is smaller, as input arrays will not always be lengths of powers of 2, the array at the end will be of an odd length
                    MergeArrays(Source, Left, Mid, Right); //merging the two subarrays inplace back into the Destination array into the correct index range
                }
            }
            return;
        }
        /// <summary>
        /// Performs a merge sort on the input list in-place and overwrites the original list with the sorted values
        /// </summary>
        /// <param name="Source">The list of values to be sorted</param>
        static public void Sort(List<T> Source)
        {
            int n = Source.Count;
            T[] SourceArray = Source.ToArray();
            for (int CurrentSize = 1; CurrentSize < n; CurrentSize *= 2) //iterator that specifies what the current sublist size is (powers of 2 ascending)
            {
                for (int Left = 0; Left < n - 1; Left += 2 * CurrentSize) //iterator to go through every "Left" index based on the current size, to get the range of 2 arrays to merge
                {
                    int Mid = Math.Min(Left + CurrentSize - 1, n - 1);
                    //middle is either Left + CurrentSize - 1, or (n - 1), whichever is smaller - to prevent indexing out of bounds errors
                    int Right = Math.Min(Left + 2 * CurrentSize - 1, n - 1);
                    //right is either Left + CurrentSize * 2 - 1 or (n - 1), whichever is smaller, as input arrays will not always be lengths of powers of 2, the array at the end will be of an odd length
                    MergeArrays(SourceArray, Left, Mid, Right); //merging the two subarrays inplace back into the Destination array into the correct index range
                }
            }
            for (int i = 0; i < n; i++)
            {
                Source[i] = SourceArray[i];
            }
            return;
        }
        /// <summary>
        /// Performs a merge sort on the input array and returns a sorted copy of the input array without modifying the input array
        /// </summary>
        /// <param name="Source">The array of values to be sorted</param>
        /// <returns>Sorted copy of the input array</returns>
        static public T[] SortAsCopy(T[] Source)
        {
            int n = Source.Length; //stores the array length
            T[] Destination = new T[Source.Length]; //initialises a destination array for the sorted data
            Array.Copy(Source, Destination, n); //creates a copy of the source array to use for sorting


            for (int CurrentSize = 1; CurrentSize < n; CurrentSize *= 2) //iterator that specifies what the current sublist size is (powers of 2 ascending)
            {
                for (int Left = 0; Left < n - 1; Left += 2 * CurrentSize) //iterator to go through every "Left" index based on the current size, to get the range of 2 arrays to merge
                {
                    int Mid = Math.Min(Left + CurrentSize - 1, n - 1);
                    //middle is either Left + CurrentSize - 1, or (n - 1), whichever is smaller - to prevent indexing out of bounds errors
                    int Right = Math.Min(Left + 2 * CurrentSize - 1, n - 1);
                    //right is either Left + CurrentSize * 2 - 1 or (n - 1), whichever is smaller, as input arrays will not always be lengths of powers of 2, the array at the end will be of an odd length
                    MergeArrays(Destination, Left, Mid, Right); //merging the two subarrays inplace back into the Destination array into the correct index range
                }
            }
            return Destination; //the sorted copy of the unsorted Source array is returned
        }
        /// <summary>
        /// Performs a merge sort on the input list and returns a sorted copy of the input list without modifying the input list
        /// </summary>
        /// <param name="Source">The list of values to be sorted</param>
        /// <returns>Sorted list of the input list</returns>
        static public List<T> SortAsCopy(List<T> Source)
        {
            int n = Source.Count; //stores the array length
            T[] Destination = Source.ToArray(); //initialises a destination array for the sorted data

            for (int CurrentSize = 1; CurrentSize < n; CurrentSize *= 2) //iterator that specifies what the current sublist size is (powers of 2 ascending)
            {
                for (int Left = 0; Left < n - 1; Left += 2 * CurrentSize) //iterator to go through every "Left" index based on the current size, to get the range of 2 arrays to merge
                {
                    int Mid = Math.Min(Left + CurrentSize - 1, n - 1);
                    //middle is either Left + CurrentSize - 1, or (n - 1), whichever is smaller - to prevent indexing out of bounds errors
                    int Right = Math.Min(Left + 2 * CurrentSize - 1, n - 1);
                    //right is either Left + CurrentSize * 2 - 1 or (n - 1), whichever is smaller, as input arrays will not always be lengths of powers of 2, the array at the end will be of an odd length
                    MergeArrays(Destination, Left, Mid, Right); //merging the two subarrays inplace back into the Destination array into the correct index range
                }
            }
            return Destination.ToList(); //the sorted copy of the unsorted Source list is returned
        }
        /// <summary>
        /// Private method to merge two subarrays in-place given the left/middle/right indices of the merging area
        /// </summary>
        /// <param name="Source">The array in which the sort is taking place</param>
        /// <param name="LeftIndex">The first index of the left subarray</param>
        /// <param name="Middle">The last index of the left subarray (the "middle" of the merging area)</param>
        /// <param name="RightIndex">The last index of the right subarray</param>
        static private void MergeArrays(T[] Source, int LeftIndex, int Middle, int RightIndex)
        {
            //Left array is the range of index Left to Mid
            //Right array is the range of index (Mid + 1) to Right
            int LeftSize = Middle - LeftIndex + 1;
            int RightSize = RightIndex - Middle;
            T[] Left = new T[LeftSize];
            T[] Right = new T[RightSize];

            Array.Copy(Source, LeftIndex, Left, 0, LeftSize); //filling the left array from the correct source array range
            Array.Copy(Source, Middle + 1, Right, 0, RightSize); //filling the right array from the correct source array range

            int Pointer1 = 0; //pointer to keep track of the left array's current index during the merging
            int Pointer2 = 0; //pointer to keep track of the right array's current index during the merging

            for (int i = LeftIndex; i < (LeftIndex + LeftSize + RightSize); i++) //iterating through the total data items to merge, starting from the LeftIndex
            {
                if (Pointer1 == LeftSize) //this means all elements used from left array
                {
                    Source[i] = Right[Pointer2]; 
                    Pointer2++; //keep filling the passed array with items from the right array until all items used up
                    continue; //make sure to skip the iteration after this data item is processed
                }

                if (Pointer2 == RightSize) //this means all elements used from right array
                {
                    Source[i] = Left[Pointer1];
                    Pointer1++; //keep filling the passed array with items from the left array until all items used up
                    continue; //make sure to skip the iteration after this data item is processed
                }

                if (Left[Pointer1].CompareTo(Right[Pointer2]) < 0) //left item is less than right item
                {
                    Source[i] = Left[Pointer1];
                    Pointer1++; //increment the left array's pointer
                }
                else //right item is less than left item
                {
                    Source[i] = Right[Pointer2];
                    Pointer2++; //increment the right array's pointer
                }
            }
            return;
        }
    }
}
