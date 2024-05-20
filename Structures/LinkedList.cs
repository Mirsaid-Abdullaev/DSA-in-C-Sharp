using System;
using System.Collections.Generic;
using System.Text;

namespace DSA.Structures
{
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
                Head.LinkNode(Tail);
            }
            else
            {
                LinkedListNode<T> NewTail = new LinkedListNode<T>(Element, Tail);
                Tail.LinkNode(NewTail);
                Tail = NewTail;
            }
        }
        public override string ToString()
        {
            return Head.ToString();
        }
    }
    internal class LinkedListNode<T>
    {
        private LinkedListNode<T> PrevNode;
        private T Data;
        private LinkedListNode<T> NextNode;
        
        public LinkedListNode(T Data, LinkedListNode<T> PrevNode)
        {
            this.Data = Data;
            NextNode = null;
            if (PrevNode != null)
            {
                this.PrevNode = PrevNode;
            }
        }

        public void LinkNode(LinkedListNode<T> NextNode)
        {
            this.NextNode = NextNode;
        }
        
        public void UnlinkNextNode()
        {
            if (this.NextNode == null)
            {
                throw new Exception("Error: no item to unlink, node not connected to any next item.");
            }
            this.NextNode = null;
        }
        public void UnlinkPrevNode()
        {
            if (this.PrevNode == null)
            {
                throw new Exception("Error: no item to unlink, node not connected to any previous item.");
            }
            this.PrevNode = null;
        }
        public T Item
        {
            get
            {
                return Data;
            }
            set
            {
                this.Data = value;
            }
        }

        public T PrevItem
        {
            get
            {
                return PrevNode.Item;
            }
        }

        public T NextItem
        {
            get
            {
                return NextNode.Item;
            }
        }
        public string Traverse()
        {
            if (NextNode == null)
            {
                return Data.ToString();
            }
            else
            {
                return Data.ToString() + " <=> " + NextNode.Traverse().ToString();
            }
        }

        public override string ToString()
        {
            return "Linked list (head to tail): {" + Traverse().ToString() + "}";
        }
    }
}
