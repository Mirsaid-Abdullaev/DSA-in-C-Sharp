using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Searches
{
    /// <summary>
    /// Static class that contains the methods .Contains(), .IndexOf(), and GetDuplicates() for verifying existence of data in a given collection and/or returning the specific index of the data item(s).
    /// </summary>
    /// <typeparam name="T">The data type of the collection of data to pass in for searching</typeparam>
    internal static class LinearSearcher<T> where T:IComparable<T>
    {
        /// <summary>
        /// The search method that returns whether a given item is in the collection or not
        /// </summary>
        /// <param name="Source">The collection to search through</param>
        /// <param name="Target">The target item to search for in the Source collection</param>
        /// <returns>True if the element exists in the collection, false if not found</returns>
        static public bool Contains(ICollection<T> Source, T Target)
        {
            for (int i = 0; i < Source.Count; i++)
            {
                if (Source.ElementAt(i).CompareTo(Target) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Method to return the index of the first occurrence of a given item in the list. If item not in the list, simply returns -1.
        /// </summary>
        /// <param name="Source">The collection to search through for the given item</param>
        /// <param name="Target">The item to search for within the Source collection</param>
        /// <returns>The index of the first occurrence of the Target item within the Source collection, or -1 upon Target not being in the collection</returns>
        static public int IndexOf(ICollection<T> Source, T Target)
        {
            for (int i = 0; i < Source.Count; i++)
            {
                if (Source.ElementAt(i).CompareTo(Target) == 0)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Method to return how many times a given element occurs in a given collection.
        /// </summary>
        /// <param name="Source">The collection to search through</param>
        /// <param name="Target">The element to search for and count occurrences of</param>
        /// <returns>0 if element does not exist in the collection, otherwise returns the integer of the number of times Target appears in the collection</returns>
        static public int GetDuplicates(ICollection<T> Source, T Target)
        {
            int Occurrences = 0;
            for (int i = 0; i < Source.Count; i++)
            {
                if (Source.ElementAt(i).CompareTo(Target) == 0)
                {
                    Occurrences++;
                }
            }
            return Occurrences;
        }
    }
}
