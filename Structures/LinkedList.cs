using System;
using System.Collections.Generic;

namespace DSA.Structures
{
    /// <summary>
    /// Linked List class using a template type T. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class LinkedList<T>
    {
        /// <summary>
        /// node that holds the head of the linked list
        /// </summary>
        private LinkedListNode<T> Head;
        /// <summary>
        /// node holding the tail of the linked list
        /// </summary>
        private LinkedListNode<T> Tail;
        /// <summary>
        /// public getter for the head node
        /// </summary>
        public LinkedListNode<T> ListHead 
        {
            get
            {
                return Head;
            }
        }
        /// <summary>
        /// public getter for the tail node
        /// </summary>
        public LinkedListNode<T> ListTail
        {
            get
            {
                return Tail;
            }
        }
        /// <summary>
        /// constructor for the linked list with an initial element
        /// </summary>
        /// <param name="Element">Data item to add to the list initially</param>
        public LinkedList(T Element)
        {
            Tail = null;
            Head = new LinkedListNode<T>(Element, null, Tail);
        }
        /// <summary>
        /// constructor for the linked list from an initial collection of items
        /// </summary>
        /// <param name="Collection">The collection of items to add to the linked list - will be added one by one using a foreach loop</param>
        public LinkedList(ICollection<T> Collection)
        {
            Tail = null;
            Head = null;
            foreach(T item in Collection)
            {
                AddItem(item);
            }
        }
        /// <summary>
        /// constructor for an empty linked list
        /// </summary>
        public LinkedList()
        {
            Head = null;
            Tail = null;
        }
        /// <summary>
        /// Adds an element to the linked list 
        /// </summary>
        /// <param name="Element">The element to add to the list</param>
        public void AddItem(T Element) 
        {
            if (Tail == null && Head != null) //means that only one element in the list
            {
                Tail = new LinkedListNode<T>(Element, Head, null);
                Head.LinkNextNode(Tail);
                return;
            }
            if (Tail != null && Head != null) //populated linked list with an existing head and tail
            {
                LinkedListNode<T> NewTail = new LinkedListNode<T>(Element, Tail, null); //create a new tail node, and link it to the current tail node
                Tail.LinkNextNode(NewTail); //link the existing tail node to the new tail node
                Tail = NewTail; //set tail to be the new tail node
                return;
            }
            if (Head == null && Tail == null) //empty linked list, adding the head item
            {
                Tail = null;
                Head = new LinkedListNode<T>(Element, null, Tail); //set the element to be the new head and link it to the null tail node
            }
        }
        /// <summary>
        /// Pops the item at the tail of the linked list
        /// </summary>
        /// <returns>The data item held at the tail end of the list</returns>
        public T PopTailItem()
        {
            if (Head == null) //empty linked list
            {
                throw new Exception("Error: empty linked list. Nothing to pop.");
            }
            if (Tail == null) //Head is the last item left
            {
                T Data = Head.Data; //set the data variable to hold the data item to return
                Head = null; //set head node to null
                return Data; //return this item
            }
            else
            {
                T Data = Tail.Data; //set the data variable to hold data item to return
                if (Head.NextNode == Tail) //two items in the linked list, head and tail
                {
                    Head.UnlinkNextNode(); //unlink the head from the tail
                    Tail = null; //set tail to null
                }
                else //more than two items
                {
                    LinkedListNode<T> NewTail = new LinkedListNode<T>(Tail.PrevData, Tail.PrevNode.PrevNode, null); //set new tail node to hold the data of the current tail's previous node, and set the prevnode to be the prevnode of the current tail's prevnode
                    Tail = NewTail; //overwrite the tail with newtail
                }
                return Data;
            }
        }
        /// <summary>
        /// Pops the item at the head of the linked list
        /// </summary>
        /// <returns>The data held at the head element of the linked list</returns>
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
                    if (Head.NextNode == Tail) //two elements in list
                    {
                        LinkedListNode<T> NewHead = new LinkedListNode<T>(Tail.Data, null, null); //the new head will be the tail element
                        Head = NewHead; //set the new head to be the tail
                        Tail = null; //set tail to null
                    }
                    else //more than two items
                    {
                        LinkedListNode<T> NewHead = new LinkedListNode<T>(Head.NextData, null, Head.NextNode.NextNode); //setting the new head node to hold the data of the next node of the current head, and link to the head's next node's next node (not the tail, as >2 nodes)
                        Head.NextNode.NextNode.LinkPrevNode(NewHead); //creating the link between the new head and its next node
                        Head = NewHead; //overwriting the head with the new head
                    }
                }
                return Data;
            }
        }
        /// <summary>
        /// Returns the nodes from the current node to the last linked node in order, formatted in string format
        /// </summary>
        /// <returns>String of the linked list stemming from this current node until the tail is reached</returns>
        public override string ToString()
        {
            if (Head == null) //empty linked list
            {
                return "Linked list (head to tail): {}";
            }
            return Head.ToString(); //call the traverse back method of the head node - returns the chain of data items from the current node backwards until last node reached, recursively
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

        public T PrevData
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

        public T NextData
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
        public string TraverseBack()
        {
            if (_NextNode == null)
            {
                return _Data.ToString();
            }
            else
            {
                return _Data.ToString() + " <=> " + _NextNode.TraverseBack().ToString();
            }
        }
        public string TraverseFull()
        {
            if (_PrevNode == null)
            {
                return _Data.ToString();
            }
            else
            {
                return _PrevNode.TraverseFull() + " <=> " + _Data.ToString();
            }
        }
        public override string ToString()
        {
            return "Linked list (head to tail): {" + TraverseBack().ToString() + "}";
        }

        public static bool operator ==(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            if ((a is null && !(b is null)) || (b is null && !(a is null)))
            {
                return false;
            }
            return a.TraverseBack() == b.TraverseBack();
        }
        public static bool operator !=(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            if (a is null && b is null)
            {
                return false;
            }
            if ((a is null && !(b is null)) || (b is null && !(a is null)))
            {
                return true;
            }
            return a.TraverseBack() != b.TraverseBack();
        }
    }
}
