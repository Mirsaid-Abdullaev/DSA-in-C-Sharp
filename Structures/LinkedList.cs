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
            Tail = null;
            Head = new LinkedListNode<T>(Element, null, Tail);
        }

        public LinkedList()
        {
            Head = null;
            Tail = null;
        }
        public void AddItem(T Element)
        {
            if (Tail == null && Head != null) //one element in the list
            {
                Tail = new LinkedListNode<T>(Element, Head, null);
                Head.LinkNextNode(Tail);
                return;
            }
            if (Tail != null && Head != null) //populated linked list
            {
                LinkedListNode<T> NewTail = new LinkedListNode<T>(Element, Tail, null);
                Tail.LinkNextNode(NewTail);
                Tail = NewTail;
                return;
            }
            if (Head == null && Tail == null) //empty linked list, adding the head
            {
                Tail = null;
                Head = new LinkedListNode<T>(Element, null, Tail);
            }
        }
        public T PopTailItem()
        {
            if (Head == null) //empty linked list
            {
                throw new Exception("Error: empty linked list. Nothing to pop.");
            }
            if (Tail == null) //Head is the last item left
            {
                T Data = Head.Data;
                Head = null;
                return Data;
            }
            else
            {
                T Data = Tail.Data;
                if (Head.NextNode == Tail) //last item is the tail
                {
                    Head.UnlinkNextNode();
                    Tail = null;
                }
                else
                {
                    LinkedListNode<T> NewTail = new LinkedListNode<T>(Tail.PrevItem, Tail.PrevNode, null);
                    Tail.PrevNode.PrevNode.LinkNextNode(NewTail);
                    Tail = NewTail;
                }
                return Data;
            }
        }
        public T PopHeadItem()
        {
            if (Head == null) //empty linked list
            {
                throw new Exception("Error: empty linked list. Nothing to pop.");
            }
            else //there is at least the head element
            {
                T Data = Head.Data;
                if (Tail == null) //head is the last element in the list
                {
                    Head = null;
                }
                else //there is a tail element
                {
                    LinkedListNode<T> NewHead = new LinkedListNode<T>(Head.NextItem, null, Head.NextNode);
                    Head.NextNode.NextNode.LinkPrevNode(NewHead);
                    Head = NewHead;
                }
                return Data;
            }
        }

        public override string ToString()
        {
            if (Head == null)
            {
                return "Linked list (head to tail): {}";
            }
            return Head.ToString();
        }

        
    }
    internal class LinkedListNode<T>
    {
        private LinkedListNode<T> _PrevNode;
        private T _Data;
        private LinkedListNode<T> _NextNode;
        


        public LinkedListNode(T Data, LinkedListNode<T> PrevNode, LinkedListNode<T> NextNode)
        {
            this._Data = Data;
            _NextNode = null;
            if (PrevNode != null)
            {
                this._PrevNode = PrevNode;
            }
            if (NextNode != null)
            {
                this._NextNode = NextNode;
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

        public static bool operator ==(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            if (a.Equals(null) && b.Equals(null))
            {
                return true;
            }
            if ((a.Equals(null) && !b.Equals(null)) || (!a.Equals(null) && b.Equals(null)))
            {
                return false;
            }
            return a.Traverse() == b.Traverse();
        }
        public static bool operator !=(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            return a.Traverse() != b.Traverse();
        }
    }
}
