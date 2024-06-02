using DSA.Sorts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Searches
{
    /// <summary>
    /// Static class that contains the methods .Contains(), .IndexOf(), and GetDuplicates() for verifying existence of data in a given collection and/or returning the specific index of the data item(s). Can be used on unsorted data (sort occurs statically within, no need to manually sort beforehand)
    /// </summary>
    /// <typeparam name="T">The data type of the collection of data to pass in for searching</typeparam>
    internal static class BinarySearcher<T> where T: IComparable<T>
    {
        /// <summary>
        /// The search method that returns whether a given item is in the collection or not
        /// </summary>
        /// <param name="Source">The collection to search through</param>
        /// <param name="Target">The target item to search for in the Source collection</param>
        /// <returns>True if the element exists in the collection, false if not found</returns>
        static public bool Contains(ICollection<T> Source, T Target)
        {
            List<T> TempSource;
            if (IsDescending(Source)) //list is sorted but in reverse, we need to reverse it to enable binary searching
            {
                TempSource = (List<T>)Source.Reverse<T>(); //now we can perform the search on this list
            }
            else if (IsAscending(Source)) //sorted list, its ok
            {
                TempSource = new List<T>(Source);
            }
            else //sort the list as a copy using merge sort
            {
                TempSource = MergeSorter<T>.SortAsCopy(Source.ToList());
            }
            int Left, Right, Mid;
            Left = 0;
            Right = TempSource.Count - 1;
            Mid = (int)Math.Floor((double)Left / 2 + (double)Right / 2);

            while (Mid != Left && Mid != Right) //checking until we are at the last element
            {
                Mid = (int)Math.Floor((double)Left / 2 + (double)Right / 2);
                T Element = TempSource[Mid];
                if (Element.CompareTo(Target) == 0) //found target element
                {
                    return true;
                }
                if (Element.CompareTo(Target) > 0) //current element being checked is larger than target element, take the left sublist
                {
                    Right = Mid - 1; //updating the Right pointer, left remains unchanged
                }
                if (Element.CompareTo(Target) < 0) //current element being checked is larger than target element, take the left sublist
                {
                    Left = Mid + 1; //updating the Left pointer, right remains unchanged
                }
            }
            return false; //item not found
        }
        /// <summary>
        /// Method to return the index of the first occurrence of a given item in the list. If item not in the list, simply returns -1.
        /// </summary>
        /// <param name="Source">The collection to search through for the given item</param>
        /// <param name="Target">The item to search for within the Source collection</param>
        /// <returns>The index of the first occurrence of the Target item within the Source collection, or -1 upon Target not being in the collection</returns>
        static public int IndexOf(ICollection<T> Source, T Target)
        {
            List<T> TempSource;
            if (IsDescending(Source)) //list is sorted but in reverse, we need to reverse it to enable binary searching
            {
                TempSource = (List<T>)Source.Reverse<T>(); //now we can perform the search on this list
            }
            else if (IsAscending(Source)) //sorted list, its ok
            {
                TempSource = new List<T>(Source);
            }
            else //sort the list as a copy using merge sort
            {
                TempSource = MergeSorter<T>.SortAsCopy(Source.ToList());
            }
            int Left, Right, Mid;
            Left = 0;
            Right = TempSource.Count - 1;
            Mid = (int)Math.Floor((double)Left / 2 + (double)Right / 2);

            while (Mid != Left && Mid != Right) //checking until we are at the last element, then we can confirm item not in list
            {
                Mid = (int)Math.Floor((double)Left / 2 + (double)Right / 2);
                T Element = TempSource[Mid];
                if (Element.CompareTo(Target) == 0)
                {
                    while (Mid > 1 && TempSource[Mid - 1].CompareTo(Target) == 0) //checking for duplicate entries before a found index
                    {
                        Mid--; //return the first occurring index of the item to look for
                    }
                    return Mid; //Mid now holds the first index of the item being searched for
                }
                if (Element.CompareTo(Target) > 0) //current element being checked is larger than target element, take the left sublist
                {
                    Right = Mid - 1; //updating the Right pointer, left remains unchanged
                }
                if (Element.CompareTo(Target) < 0) //current element being checked is larger than target element, take the left sublist
                {
                    Left = Mid + 1; //updating the Left pointer, right remains unchanged
                }
            }
            return -1; //not in the list
        }
        /// <summary>
        /// Method to return how many times a given element occurs in a given collection.
        /// </summary>
        /// <param name="Source">The collection to search through</param>
        /// <param name="Target">The element to search for and count occurrences of</param>
        /// <returns>0 if element does not exist in the collection, otherwise returns the integer of the number of times Target appears in the collection</returns>
        static public int GetDuplicates(ICollection<T> Source, T Target)
        {
            List<T> TempSource;
            if (IsDescending(Source)) //list is sorted but in reverse, we need to reverse it to enable binary searching
            {
                TempSource = (List<T>)Source.Reverse<T>(); //now we can perform the search on this list
            }
            else if (IsAscending(Source)) //sorted list, its ok
            {
                TempSource = new List<T>(Source);
            }
            else //sort the list as a copy using merge sort
            {
                TempSource = MergeSorter<T>.SortAsCopy(Source.ToList());
            }
            int Left, Right, Mid, Occurrences;
            Left = 0;
            Right = TempSource.Count - 1;
            Mid = (int)Math.Floor((double)Left / 2 + (double)Right / 2);
            Occurrences = 0;

            while (Mid != Left && Mid != Right) //checking until we are at the last element, then we can confirm item not in list
            {
                Mid = (int)Math.Floor((double)Left / 2 + (double)Right / 2);
                T Element = TempSource[Mid];
                if (Element.CompareTo(Target) == 0)
                {
                    int temp = Mid;
                    Occurrences = 1; //found at least one item
                    while (Mid > 1 && TempSource[Mid - 1].CompareTo(Target) == 0) //checking for duplicate entries BEFORE the found index
                    {
                        Mid--;
                        Occurrences++;
                    }
                    while (temp < TempSource.Count - 1 && TempSource[temp + 1].CompareTo(Target) == 0) //checking for duplicate entries AFTER the found index
                    {
                        temp++;
                        Occurrences++;
                    }
                    return Occurrences; //Mid now holds the total number of occurrences of the given Target element in the Source collection
                }
                if (Element.CompareTo(Target) > 0) //current element being checked is larger than target element, take the left sublist
                {
                    Right = Mid - 1; //updating the Right pointer, left remains unchanged
                }
                if (Element.CompareTo(Target) < 0) //current element being checked is larger than target element, take the left sublist
                {
                    Left = Mid + 1; //updating the Left pointer, right remains unchanged
                }
            }
            return Occurrences; //not in the list, so no occurrences
        }
        /// <summary>
        /// Private method to return whether a collection is ascending (i.e. is it sorted) or not. 
        /// </summary>
        /// <param name="Source">The collection to check</param>
        /// <returns>True if the collection is ascending, even if not strictly (i.e. duplicates are allowed), and false if not all elements are ascending (i.e. at least one pair of elements exists where they are strictly descending)</returns>
        static private bool IsAscending(ICollection<T> Source)
        {
            for (int i = 0; i < Source.Count - 1; i++)
            {
                if (Source.ElementAt(i).CompareTo(Source.ElementAt(i + 1)) > 0) //checking whether every element is less than or equal to the next one
                {
                    return false; //as soon as one pair of elements found where the next element is less than the previous, break loop and return false (not ascending)
                }
            }
            return true;
        }
        /// <summary>
        /// Private method to return whether a collection is descending (i.e. is it sorted in reverse) or not. 
        /// </summary>
        /// <param name="Source">The collection to check</param>
        /// <returns>True if the collection is descending, even if not strictly (i.e. duplicates are allowed), and false if not all elements are descending (i.e. at least one pair of elements exists where they are strictly ascending)</returns>
        static private bool IsDescending(ICollection<T> Source)
        {
            for (int i = 0; i < Source.Count - 1; i++)
            {
                if (Source.ElementAt(i).CompareTo(Source.ElementAt(i + 1)) < 0) //checking whether every element is larger than or equal to the next one
                {
                    return false; //as soon as one pair of elements found where the next element is larger than the previous, break loop and return false (not descending)
                }
            }
            return true;
        }

    }
}
