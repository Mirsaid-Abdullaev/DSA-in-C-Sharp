using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Structures
{
    /// <summary>
    /// Stack class made of a template type. Contains all the stack operations, Push, Peek, Pop, as well as a custom ToString() overriden method, and the ability to get the underlying stacklist
    /// </summary>
    internal class Stack<T>
    {
        private T[] _Stack;
        private int _Count;
        private readonly int _BatchSize;
        public int Count
        {
            get
            {
                return _Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _Count == 0;
            }
        }

        public List<T> StackList
        {
            get
            {
                return _Stack.ToList();
            }
        }
        public Stack(int BatchSize = 1000)
        {
            _Count = 0;
            _Stack = new T[BatchSize];
            _BatchSize = BatchSize;
        }

        public void Push(T Item)
        {
            _Count++;

            if (_Count % _BatchSize == 1 && _Count > _BatchSize) //there are now more elements than the batch siz, redim the array
            {
                //count holds the number of elements in stack + 1
                T[] temp = new T[_BatchSize + _Stack.Length];
                Array.Copy(_Stack, temp, _Stack.Length);
                _Stack = temp;
                _Stack[_Count - 1] = Item;
            }
            else //not reached batch size
            {
                _Stack[_Count - 1] = Item;
            }
        }

        public T Pop()
        {
            if (_Count == 0) //empty stack
            {
                throw new Exception("Error: empty stack, nothing to pop.");
            }
            T Item = _Stack[_Count - 1];
            _Count--;
            T[] temp = new T[_Count];
            Array.Copy(_Stack, temp, _Count);
            _Stack = temp;
            return Item;
        }

        public T Peek()
        {
            if (_Count == 0)
            {
                throw new Exception("Error: empty stack, nothing to peek at.");
            }
            return _Stack[Count - 1];
        }

        public override string ToString()
        {
            return "Stack: {" + string.Join(',', _Stack.ToList().GetRange(0, _Count).Reverse<T>()) + "}";
        }
    }
}
