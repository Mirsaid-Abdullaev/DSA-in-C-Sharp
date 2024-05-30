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
        private readonly List<T> _Stack;
        private int _Count;
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
        public Stack()
        {
            _Count = 0;
            _Stack = new List<T>();
        }

        public void Push(T Item)
        {
            _Count++;
            _Stack.Add(Item);
        }

        public T Pop()
        {
            if (_Count == 0) //empty stack
            {
                throw new Exception("Error: empty stack, nothing to pop.");
            }
            T Item = _Stack[_Count - 1];
            _Stack.RemoveAt(_Count - 1);
            _Count--;
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
            return "Stack: {" + string.Join(',', _Stack.Reverse<T>()) + "}";
        }
    }
}
