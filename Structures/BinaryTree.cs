using System;
using System.Collections.Generic;
using System.Text;

namespace DSA.Structures
{
    internal class BinaryTree<T> where T : IComparable
    {
        private BTreeNode<T> _Root;
        private int _NoOfNodes;

        public BTreeNode<T> Root
        {
            get
            {
                return _Root;
            }
        }
        public int NoOfNodes
        {
            get
            {
                return _NoOfNodes;
            }
        }
        public BinaryTree()
        {
            _Root = null;
            _NoOfNodes = 0;
        }
        public BinaryTree(T Element)
        {

        }
        public BinaryTree(ICollection<T> Elements)
        {

        }
        public void AddElement(T Element)
        {

        }
        public string Str_PreOrderTraversal()
        {

        }
        public string Str_PostOrderTraversal()
        {

        }
        public string Str_InOrderTraversal()
        {

        }
        public List<T> PreOrderTraversal()
        {

        }
        public List<T> PostOrderTraversal()
        {

        }
        public List<T> InOrderTraversal()
        {

        }
    }
    internal class BTreeNode<T>
    {
        private BTreeNode<T> _Left;
        public BTreeNode<T> Left
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
        private BTreeNode<T> _Right;
        public BTreeNode<T> Right
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
        private T _Data;
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
        public BTreeNode(T Data)
        {
            _Left = null;
            _Right = null;
            _Data = Data;
        }
    }
}