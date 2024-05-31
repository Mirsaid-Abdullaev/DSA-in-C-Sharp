using System;
using System.Collections.Generic;

namespace DSA.Structures
{
    /// <summary>
    /// Binary tree class for any template type where the type implements the IComparable interface
    /// </summary>
    internal class BinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// Class that stores the data of a binary tree node for a binary tree class instance
        /// </summary>
        internal class BTreeNode
        {
            /// <summary>
            /// The left child node - linking the current (parent) instance to the left child node instance
            /// </summary>
            private BTreeNode _Left;
            /// <summary>
            /// Public getter and setter for the left child node of the current node instance
            /// </summary>
            public BTreeNode Left
            {
                get
                {
                    return _Left;
                }
                set
                {
                    _Left = value;
                }
            }
            /// <summary>
            /// The right child node - linking the current (parent) instance to the right child node instance
            /// </summary>
            private BTreeNode _Right;
            /// <summary>
            /// Public getter and setter for the right child node of the current node instance
            /// </summary>
            public BTreeNode Right
            {
                get
                {
                    return _Right;
                }
                set
                {
                    _Right = value;
                }
            }
            /// <summary>
            /// The actual data held in the current node instance
            /// </summary>
            private T _Data;
            /// <summary>
            /// Public getter and setter for the private _Data property
            /// </summary>
            public T Data
            {
                get
                {
                    return _Data;
                }
                set
                {
                    _Data = value;
                }
            }
            /// <summary>
            /// The number of the same data instances of the same value as _Data held in this node - to bypass the requirement of binary trees needing to have unique elements at the nodes
            /// </summary>
            private int _Count;
            /// <summary>
            /// Public getter and setter for the private _Count property
            /// </summary>
            public int Count
            {
                get
                {
                    return _Count;
                }
                set
                {
                    _Count = value;
                }
            }
            /// <summary>
            /// Constructor for a binary tree node, with Data as a mandatory parameter and the left and right child as optional parameters
            /// </summary>
            /// <param name="Data">The actual data to be held by the node</param>
            /// <param name="LeftChild">Optional parameter to create the link to a left child node instance during initialisation</param>
            /// <param name="RightChild">Optional parameter to create the link to a right child node instance during initialisation</param>
            public BTreeNode(T Data, BTreeNode LeftChild = null, BTreeNode RightChild = null)
            {
                _Left = LeftChild;
                _Right = RightChild;
                _Data = Data;
                _Count = 1;
            }
        }

        /// <summary>
        /// The root node of the binary tree
        /// </summary>
        private BTreeNode _Root;
        /// <summary>
        /// The number of nodes in the binary tree
        /// </summary>
        private int _NoOfNodes;
        /// <summary>
        /// Public getter for the private Root property
        /// </summary>
        public BTreeNode Root
        {
            get
            {
                return _Root;
            }
        }
        /// <summary>
        /// Public getter for the number of nodes property
        /// </summary>
        public int NoOfNodes
        {
            get
            {
                return _NoOfNodes;
            }
        }
        /// <summary>
        /// Constructor to initialise an empty binary tree
        /// </summary>
        public BinaryTree()
        {
            _Root = null;
            _NoOfNodes = 0;
        }
        /// <summary>
        /// Constructor to initialise and add a specified element to a binary tree
        /// </summary>
        /// <param name="Element">The data item to add to the list</param>
        public BinaryTree(T Element)
        {
            _Root = new BTreeNode(Element);
            _NoOfNodes++;
        }
        /// <summary>
        /// Constructor to initialise and add a specified collection of items to a binary tree
        /// </summary>
        /// <param name="Elements">The collection of data items to add sequentially to the tree structure</param>
        public BinaryTree(ICollection<T> Elements)
        {
            foreach(T Element in Elements)
            {
                AddElement(Element);       
            }
        }
        /// <summary>
        /// Subprocedure to add an element into the binary tree
        /// </summary>
        /// <param name="Element">The data item to add to the binary tree</param>
        public void AddElement(T Element)
        {
            if (_NoOfNodes == 0) //empty binary tree, add the root element
            {
                _Root = new BTreeNode(Element);
            }
            else //nodes exist, need to add node in correct place in tree
            {
                BTreeNode CurrentNode = _Root; //start traversal with root node
                while (true)
                {
                    if (CurrentNode.Data.CompareTo(Element) > 0) //new element less than current data, go to left subtree
                    {
                        if (CurrentNode.Left is null) //no element at current.left, so just insert it there and break
                        {
                            BTreeNode NewElement = new BTreeNode(Element); //add the data item
                            CurrentNode.Left = NewElement; //add the parent-child link
                            break; //exit
                        }
                        else //current has a left child, need to continue traversing
                        {
                            CurrentNode = CurrentNode.Left;
                            continue;
                        }
                    }
                    if (CurrentNode.Data.CompareTo(Element) < 0) //new element larger than current data, go to right subtree
                    {
                        if (CurrentNode.Right is null) //no element at current.right, so just insert it there and break
                        {
                            BTreeNode NewElement = new BTreeNode(Element); //add the data item
                            CurrentNode.Right = NewElement; //add the parent-child link
                            break; //exit loop and sub
                        }
                        else //current has a right child, need to continue traversing
                        {
                            CurrentNode = CurrentNode.Right; //set the new current node to the right child of the current node
                            continue; //continue the traversal to insert new data
                        }
                    }
                    if (CurrentNode.Data.CompareTo(Element) == 0) //element is the same as the current data, increment the Count property of the current node and break
                    {
                        CurrentNode.Count++; //increment the number of data items for that node - counting duplicates
                        break;
                    }
                }
            }
            _NoOfNodes++; //increment number of elements
        }
        /// <summary>
        /// Helper method to carry out the recursive preorder traversal and add the elements in the correct order into a target list
        /// </summary>
        /// <param name="CurrentNode">The current node to traverse</param>
        /// <param name="Target">The target list where to add the elements in the desired order of output (in this case preorder traversal)</param>
        private void PreOrderTraversalInPlace(BTreeNode CurrentNode, List<T> Target)
        {
            if (!(CurrentNode is null))
            {
                for (int i = 0; i < CurrentNode.Count; i++)
                {
                    Target.Add(CurrentNode.Data);
                }
                PreOrderTraversalInPlace(CurrentNode.Left, Target);
                PreOrderTraversalInPlace(CurrentNode.Right, Target);
            }
        }
        /// <summary>
        /// Helper method to carry out the recursive postorder traversal and add the elements in the correct order into a target list
        /// </summary>
        /// <param name="CurrentNode">The current node to traverse</param>
        /// <param name="Target">The target list where to add the elements in the desired order of output (in this case postorder traversal)</param>
        private void PostOrderTraversalInPlace(BTreeNode CurrentNode, List<T> Target)
        {
            if (!(CurrentNode is null))
            {
                PostOrderTraversalInPlace(CurrentNode.Left, Target);
                PostOrderTraversalInPlace(CurrentNode.Right, Target);
                for (int i = 0; i < CurrentNode.Count; i++)
                {
                    Target.Add(CurrentNode.Data);
                }
            }
        }
        /// <summary>
        /// Helper method to carry out the recursive inorder traversal and add the elements in the correct order into a target list
        /// </summary>
        /// <param name="CurrentNode">The current node to traverse</param>
        /// <param name="Target">The target list where to add the elements in the desired order of output (in this case inorder traversal)</param>
        private void InOrderTraversalInPlace(BTreeNode CurrentNode, List<T> Target)
        {
            if (!(CurrentNode is null))
            {
                InOrderTraversalInPlace(CurrentNode.Left, Target);
                for (int i = 0; i < CurrentNode.Count; i++)
                {
                    Target.Add(CurrentNode.Data);
                }
                InOrderTraversalInPlace(CurrentNode.Right, Target);
            }
        }
        /// <summary>
        /// The function that returns the list of elements in the correct in-order traversal order of the current binary tree instance
        /// </summary>
        /// <returns>The list of elements that is the result of carrying out the in-order traversal on the current binary tree</returns>
        public List<T> InOrderTraversal()
        {
            List<T> Result = new List<T>();
            InOrderTraversalInPlace(this.Root, Result);
            return Result;
        }
        /// <summary>
        /// The function that returns the list of elements in the correct pre-order traversal order of the current binary tree instance
        /// </summary>
        /// <returns>The list of elements that is the result of carrying out the pre-order traversal on the current binary tree</returns>
        public List<T> PreOrderTraversal()
        {
            List<T> Result = new List<T>();
            PreOrderTraversalInPlace(this.Root, Result);
            return Result;
        }
        /// <summary>
        /// The function that returns the list of elements in the correct post-order traversal order of the current binary tree instance
        /// </summary>
        /// <returns>The list of elements that is the result of carrying out the post-order traversal on the current binary tree</returns>
        public List<T> PostOrderTraversal()
        {
            List<T> Result = new List<T>();
            PostOrderTraversalInPlace(this.Root, Result);
            return Result;
        }
        /// <summary>
        /// Function that returns whether a specified element exists in the current binary tree instance (true) or not (false) by using the binary search tree algorithm
        /// </summary>
        /// <param name="Element">The element to check for within the current tree instance</param>
        /// <returns>True if element exists in the tree, false if the element does not</returns>
        public bool IsInTree(T Element)
        {
            BTreeNode CurrentNode = _Root;
            while (true) //while still at least one child left
            {
                if (CurrentNode.Data.CompareTo(Element) == 0) //found item
                {
                    return true;
                }
                if (CurrentNode.Data.CompareTo(Element) > 0) //item we are looking for is less than current element => go left subtree
                {
                    if (CurrentNode.Left is null) //no more elements to the left, item doesn't exist in tree
                    {
                        return false;
                    }
                    else //left node exists, traverse down left subtree
                    {
                        CurrentNode = CurrentNode.Left;
                        continue;
                    }
                }
                if (CurrentNode.Data.CompareTo(Element) < 0) //item we are looking for is larger than current element => go right subtree
                {
                    if (CurrentNode.Right is null) //no more elements to the right, item doesn't exist in tree
                    {
                        return false;
                    }
                    else //right node exists, traverse down right subtree
                    {
                        CurrentNode = CurrentNode.Right;
                        continue;
                    }
                }
            }
        }
        /// <summary>
        /// Subroutine for deleting a specific element from the binary tree without throwing an exception if the element specified is not in the current tree instance
        /// </summary>
        /// <param name="Element">The data item to try and delete out of the tree</param>
        /// <returns>True if item found and deleted from the tree, false if the item not in the tree (tree not modified)</returns>
        public bool TryDeleteElement(T Element)
        {
            if (!this.IsInTree(Element)) //search for the element before attempting to delete
            {
                return false;
            }
            BTreeNode CurrentNode = _Root; //beginning traversal with root node
            //Need to check 4 cases: 1) Node is a LEAF, 2) Node has a SINGLE child, 3) Node has BOTH children, 4) Node is the LAST one in the tree (a subset of case 1)

            while (CurrentNode.Data.CompareTo(Element) != 0) //at this point, the root does NOT have the node to delete, so we keep traversing the tree until the currentnode is the element to delete
            {
                if (CurrentNode.Data.CompareTo(Element) > 0) //data in the current node is larger than the element we are looking for => go to left child and continue traversal
                {
                    CurrentNode = CurrentNode.Left;
                    continue;
                }
                if (CurrentNode.Data.CompareTo(Element) < 0) //data in the current node is less than the element we are looking for => go to right child and continue traversal
                {
                    CurrentNode = CurrentNode.Right;
                    continue;
                }
            }
            //at this point, currentnode holds the node to delete
            if (CurrentNode.Left is null || CurrentNode.Right is null) // LEAF node to delete or the last ROOT element
            {
                BTreeNode LeafParent = GetNodeParentByRef(CurrentNode.Data);
                if (LeafParent is null) //deleting the last element in the binary tree, the root
                {
                    _Root = null;
                    _NoOfNodes = 0;
                    return true;
                }
                if (LeafParent.Left.Data != null && LeafParent.Left.Data.CompareTo(CurrentNode.Data) == 0) //deleting the left child
                {
                    LeafParent.Left = null;
                }
                else
                {
                    LeafParent.Right = null;
                }
                return true;
            }
            if (!(CurrentNode.Left is null || CurrentNode.Right is null)) //BOTH children exist
            {
                BTreeNode Node = GetNodeByRef(CurrentNode.Data); //the node to delete
                BTreeNode Successor = GetInOrderSuccessorByRef(Node); //its in-order successor
                BTreeNode SuccessorParent = GetInOrderSuccessorParentByRef(Node); //the parent of the in-order successor (or the successor node if the right node of the node to be deleted is a leaf - this case is handled when conditionally setting the successorparent's left node below)

                Successor.Left = Node.Left;
                Successor.Right = Node.Right; //setting the references of the new node to the left and right child nodes of the node to be deleted to preserve references

                SuccessorParent.Left = Successor.Left == null ? Node.Left : null; //setting the successor parent's left reference to be null if the successor parent is not the same as the successor node
                Node = Successor; //overwriting the node to be the new successor node
                return true;
            }
            if (CurrentNode.Right is null ^ CurrentNode.Left is null) //ONE child exists
            {
                BTreeNode Node = GetNodeByRef(CurrentNode.Data); //get the node to delete
                BTreeNode Successor = CurrentNode.Left is null ? Node.Right : Node.Left; //set the successor to be single child node (depending on which one wasn't null)
                Successor.Left = null;
                Successor.Right = null; //unlink both left and right for the new successor as it now has no children

                Node = Successor; //overwrite the node to be deleted
                return true;
            }
            throw new Exception("Algorithm error: deletion case encountered that has not been accounted for"); //this line should never be hit as all cases have been exhausted but I have left here just in case a bug ever shows up
        }
        /// <summary>
        /// Given a certain element, returns its in-order successor node as a ByReference object, i.e. the leftmost node of the right subtree of a given node.
        /// </summary>
        /// <param name="Node">The node for which to get the in-order successor</param>
        /// <returns>The ByReference in-order successor node for the specified Node</returns>
        private BTreeNode GetInOrderSuccessorByRef(BTreeNode Node)
        {
            BTreeNode Successor = Node.Right;
            while (!(Successor.Left is null)) //get the leftmost leaf node of the right subtree of the specified node
            {
                Successor = Successor.Left;
            }
            return Successor;
        }
        /// <summary>
        /// Gets the parent of the in-order successor node of a given node as a ByReference object, i.e. the parent node of the leftmost node of the right subtree of a given node
        /// </summary>
        /// <param name="Node">The node for which to find the parent of the in-order successor node</param>
        /// <returns>The ByReference in-order successor node's parent node for the specified Node parameter</returns>
        private BTreeNode GetInOrderSuccessorParentByRef(BTreeNode Node)
        {
            BTreeNode SuccessorParent = Node.Right;
            while (!(SuccessorParent.Left.Left is null)) //almost the same as the getting inorder successor, but returning the node before the successor
            {
                SuccessorParent = SuccessorParent.Left;
            }
            return SuccessorParent;
        }
        /// <summary>
        /// Gets a node object ByReference from the current binary tree instance based on the passed data value
        /// </summary>
        /// <param name="Element">The data of the node to return</param>
        /// <returns>A ByReference node object with the given data, or throws an exception if a node with such data doesn't exist</returns>
        private BTreeNode GetNodeByRef(T Element)
        {
            BTreeNode CurrentNode = _Root;
            while (true) //while still at least one child left
            {
                if (CurrentNode.Data.CompareTo(Element) == 0) //found item
                {
                    return CurrentNode;
                }
                if (CurrentNode.Data.CompareTo(Element) > 0) //item we are looking for is less than current element => go left subtree
                {
                    if (CurrentNode.Left is null) //no more elements to the left, item doesn't exist in tree
                    {
                        throw new Exception("Error: a node with this data does not exist in the current binary tree instance.");
                    }
                    else //left node exists, traverse down left subtree
                    {
                        CurrentNode = CurrentNode.Left;
                        continue;
                    }
                }
                if (CurrentNode.Data.CompareTo(Element) < 0) //item we are looking for is larger than current element => go right subtree
                {
                    if (CurrentNode.Right is null) //no more elements to the right, item doesn't exist in tree
                    {
                        throw new Exception("Error: a node with this data does not exist in the current binary tree instance.");
                    }
                    else //right node exists, traverse down right subtree
                    {
                        CurrentNode = CurrentNode.Right;
                        continue;
                    }
                }
            }
        }
        /// <summary>
        /// Gets the parent node object ByReference of a node from the current binary tree instance with the passed data value
        /// </summary>
        /// <param name="Element">The data value of the node for which to return the parent node of</param>
        /// <returns>The parent node of the node with the data value passed in as a ByReference object</returns>
        /// <exception cref="Exception"></exception>
        private BTreeNode GetNodeParentByRef(T Element)
        {
            BTreeNode ParentNode = null;
            BTreeNode CurrentNode = _Root;
            while (true) //while still at least one child left
            {
                if (CurrentNode.Data.CompareTo(Element) == 0) //found item, so return parent
                {
                    return ParentNode;
                }

                if (CurrentNode.Data.CompareTo(Element) > 0) //item we are looking for is less than current element => go left subtree
                {
                    if (CurrentNode.Left is null) //no more elements to the left, item doesn't exist in tree
                    {
                        throw new Exception("Error: a node with this data does not exist in the current binary tree instance.");
                    }
                    else //left node exists, traverse down left subtree
                    {
                        ParentNode = CurrentNode;
                        CurrentNode = CurrentNode.Left;
                        continue;
                    }
                }
                if (CurrentNode.Data.CompareTo(Element) < 0) //item we are looking for is larger than current element => go right subtree
                {
                    if (CurrentNode.Right is null) //no more elements to the right, item doesn't exist in tree
                    {
                        throw new Exception("Error: a node with this data does not exist in the current binary tree instance.");
                    }
                    else //right node exists, traverse down right subtree
                    {
                        ParentNode = CurrentNode;
                        CurrentNode = CurrentNode.Right;
                        continue;
                    }
                }
            }
        }
    }
}