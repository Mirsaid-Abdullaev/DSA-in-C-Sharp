using System;
using System.Collections.Generic;
using System.Text;

namespace DSA.Structures
{
    /// <summary>
    /// Linked List class using a template type T. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class LinkedList<T>
    {
        private LinkedListNode<T> Head;
        private LinkedListNode<T> Tail;

        public LinkedList(T Element)
        {
            Head = new LinkedListNode<T>(Element, null);
        }

        public void AddItem(T Element)
        {
            if (Tail == null)
            {
                Tail = new LinkedListNode<T>(Element, Head);
                Head.LinkNextNode(Tail);
                return;
            }
            if (Head != null)
            {
                LinkedListNode<T> NewTail = new LinkedListNode<T>(Element, Tail);
                Tail.LinkNextNode(NewTail);
                Tail = NewTail;
            }
            else //empty linked list, adding the head
            {
                Head = new LinkedListNode<T>(Element, null);
                Tail = null;
            }
        }
        public T PopItem()
        {
            if (Tail == null) //Head is the last item left
            {
                T Data = Head.Data;
                Head = null;
                return Data;
            }
            else
            {
                T Data = Tail.Data;
                LinkedListNode<T> NewTail = new LinkedListNode<T>(Tail.Data, null);
                //NewTail.LinkPrevNode
            }
        }
        public override string ToString()
        {
            return Head.ToString();
        }
    }
    internal class LinkedListNode<T>
    {
        private LinkedListNode<T> _PrevNode;
        private T _Data;
        private LinkedListNode<T> _NextNode;
        


        public LinkedListNode(T Data, LinkedListNode<T> PrevNode)
        {
            this._Data = Data;
            _NextNode = null;
            if (PrevNode != null)
            {
                this._PrevNode = PrevNode;
            }
        }

        public void LinkNextNode(LinkedListNode<T> NextNode)
        {
            this._NextNode = NextNode;
        }
        public void LinkPrevNode(LinkedListNode<T> PrevNode)
        {
            this._PrevNode = PrevNode;
        }
        
        public void UnlinkNextNode()
        {
            if (this._NextNode == null)
            {
                throw new Exception("Error: no item to unlink, node not connected to any next item.");
            }
            this._NextNode = null;
        }
        public void UnlinkPrevNode()
        {
            if (this._PrevNode == null)
            {
                throw new Exception("Error: no item to unlink, node not connected to any previous item.");
            }
            this._PrevNode = null;
        }
        public T Data
        {
            get
            {
                return _Data;
            }
            set
            {
                this._Data = value;
            }
        }

        public T PrevItem
        {
            get
            {
                return _PrevNode.Data;
            }
        }

        public LinkedListNode<T> PrevNode
        {
            get
            {
                return this._PrevNode;
            }
        }

        public T NextItem
        {
            get
            {
                return _NextNode.Data;
            }
        }

        public LinkedListNode<T> NextNode
        {
            get
            {
                return this._NextNode;
            }
        }
        public string Traverse()
        {
            if (_NextNode == null)
            {
                return _Data.ToString();
            }
            else
            {
                return _Data.ToString() + " <=> " + _NextNode.Traverse().ToString();
            }
        }

        public override string ToString()
        {
            return "Linked list (head to tail): {" + Traverse().ToString() + "}";
        }
    }
}
