using System;
using System.Collections.Generic;

namespace DSA.Structures
{
    /// <summary>
    /// Queue class using a template type. Contains all queue operations: Enqueue, Dequeue, Peek, as well as DeleteLastAddedElement and Contains, and a custom ToString() override
    /// </summary>
    internal class Queue<T>
    {
        private int RearPointer = -1;
        private readonly List<T> Items;
        private const int FrontPointer = 0;

        public bool IsEmpty
        {
            get
            {
                return RearPointer == -1;
            }
        }

        public List<T> QueueList
        {
            get
            {
                return Items;
            }
        }

        public int Size
        {
            get
            {
                return RearPointer + 1;
            }
        }
        public Queue()
        {
            Items = new List<T>();
        }

        public void DeleteLastAddedElement()
        {
            if (Items.Count > 0)
            {
                Items.RemoveAt(RearPointer);
                RearPointer--;
            }
        }

        public void Enqueue(T Data)
        {
            RearPointer++;
            Items.Add(Data);
        }
        public T Dequeue()
        {
            if (Items.Count > 0)
            {
                T Data = Items[FrontPointer];
                Items.RemoveAt(FrontPointer);
                RearPointer--;
                return Data;
            }
            else
            {
                throw new Exception("Error: Queue empty, nothing to dequeue.");
            }
        }

        public T Peek()
        {
            if (Items.Count > 0)
            {
                return Items[FrontPointer];
            }
            else
            {
                throw new Exception("Error: Queue empty, nothing to dequeue.");
            }
        }
        public bool Contains(T Element)
        {
            return Items.Contains(Element);
        }

        public override string ToString()
        {
            return "Queue: {" + string.Join(',', Items) + "}";
        }
    }
}
