using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    #region ROOTED Trees
    public class SibTreeNode<T>
    {
        private T _Item;

        public T Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private SibTreeNode<T> _Parent;

        public SibTreeNode<T> Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }
        private SibTreeNode<T> _FirstChild;

        public SibTreeNode<T> FirstChild
        {
            get { return _FirstChild; }
            set { _FirstChild = value; }
        }
        private SibTreeNode<T> _NextSibling;

        public SibTreeNode<T> NextSibling
        {
            get { return _NextSibling; }
            set { _NextSibling = value; }
        }

        //Traversals
        public void Vist()
        { 
        
        }

        public void PreOrdered()
        {
            this.Vist();
            if (this._FirstChild != null)
                this._FirstChild.PreOrdered();
            if (this._NextSibling != null)
                this._NextSibling.PreOrdered();
        }

        public void PostOrdered()
        {
            if (this._FirstChild != null)
                this._FirstChild.PreOrdered();

            this.Vist();
            if (this._NextSibling != null)
                this._NextSibling.PreOrdered();            
        }

        public void InOrdered() //Only in Pair Tree
        { 
            //First -> self -> second
        }



    }


    public class SibTree<T>
    {
        private SibTreeNode<T> _Root;

        public SibTreeNode<T> Root
        {
            get { return _Root; }
            set { _Root = value; }
        }
        private int _Size;

        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public void LevelOrdered()
        {
            Queue<SibTreeNode<T>> queue = new Queue<SibTreeNode<T>>();
            queue.Enqueue(this._Root);
            Goon(queue);
        }

        private void Goon(Queue<SibTreeNode<T>> queue)
        { 
            Queue<SibTreeNode<T>> newQueue = new Queue<SibTreeNode<T>>();
            while(queue.Count() != 0)
            {
                SibTreeNode<T> QueueNode = queue.Dequeue();
                QueueNode.Vist();


                SibTreeNode<T> tempNode = QueueNode.FirstChild;
                while (tempNode != null)
                {
                    newQueue.Enqueue(tempNode);
                    tempNode = tempNode.NextSibling;
                }
            }

            Goon(newQueue);
        }
    }
    #endregion

    #region BinaryTree

    public class BinaryTreeNode<T>
    {
        private T _Item;

        public T Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private BinaryTreeNode<T> _Parent;

        public BinaryTreeNode<T> Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }
        private BinaryTreeNode<T> _First;

        public BinaryTreeNode<T> First
        {
            get { return _First; }
            set { _First = value; }
        }
        private BinaryTreeNode<T> _Second;

        public BinaryTreeNode<T> Second
        {
            get { return _Second; }
            set { _Second = value; }
        }

        public void InOrder()
        {
            if (this._First != null)
                this._First.InOrder();
            this.Visit();
            if (this._Second != null)
                this._Second.InOrder();

        }

        private void Visit()
        { 
        
        }
    }

    public class BinaryTree<T>
    {
        private BinaryTreeNode<T> _Root;

        public BinaryTreeNode<T> Root
        {
            get { return _Root; }
            set { _Root = value; }
        }

        private int _Size = 0;

        public int Size
        {
            get { return _Size; }
            protected set { _Size = value; }
        }
    }
    #endregion

    #region BinarySearchTree

    interface IOrderDictionary<K,V>
    {
        Entry<K, V> Insert(K key, V value);
        Entry<K, V> Find(K key);
        Entry<K, V> Min();
        Entry<K, V> Max();
        Entry<K, V> Remove(K key);
    }

    public class BinarySearchTree<K, V> : BinaryTree<Entry<K, V>>,IOrderDictionary<K,V>
        where K:IComparable<K>
    {

        public Entry<K, V> Insert(K key, V value)
        {

            Entry<K,V> newEntry = new Entry<K,V>(){Key = key, Value = value};
            
            if (Root == null)
            {
                Root = new BinaryTreeNode<Entry<K, V>>();
                Root.Item = newEntry;
            }
            else
            {
                BinaryTreeNode<Entry<K, V>> tempNode = Root;

                while (tempNode != null)
                {
                    if (key.CompareTo(tempNode.Item.Key) <= 0)
                    {
                        if (tempNode.First == null)
                        {
                            tempNode.First = new BinaryTreeNode<Entry<K, V>>();
                            tempNode.First.Parent = tempNode;
                            tempNode.First.Item = newEntry;
                            break;
                        }
                        else
                        {
                            tempNode = tempNode.First;
                        }
                    }
                    else
                    {
                        if (tempNode.Second == null)
                        {
                            tempNode.Second = new BinaryTreeNode<Entry<K, V>>();
                            tempNode.Second.Parent = tempNode;
                            tempNode.Second.Item = newEntry;
                            break;
                        }
                        else
                        {
                            tempNode = tempNode.Second;
                        }
                    }
                }
            }
            Size++;
            return newEntry;
        }

        public Entry<K, V> Find(K key)
        {
            BinaryTreeNode<Entry<K, V>> resultNode = _Find(key);
            if (resultNode != null)
                return resultNode.Item;
            else
                return null;
        }

        public Entry<K, V> Min()
        {
            if (Root == null)
                return null;

            Queue<BinaryTreeNode<Entry<K, V>>> queue = new Queue<BinaryTreeNode<Entry<K,V>>>();
            queue.Enqueue(Root);
            BinaryTreeNode<Entry<K, V>> minNode = Root;
            FindMaxMinTree(queue, ref minNode, true);
            return minNode.Item;
        }

        public Entry<K, V> Max()
        {
            if (Root == null)
                return null;

            Queue<BinaryTreeNode<Entry<K, V>>> queue = new Queue<BinaryTreeNode<Entry<K, V>>>();
            queue.Enqueue(Root);
            BinaryTreeNode<Entry<K, V>> maxNode = Root;
            FindMaxMinTree(queue, ref maxNode, false);
            return maxNode.Item;
        }

        public Entry<K, V> Remove(K key)
        {
            BinaryTreeNode<Entry<K, V>> resultNode = _Find(key);
            if (resultNode != null)
            {
                if (resultNode.First != null && resultNode.Second != null)
                    RemoveNodeWithTwoChild(resultNode);
                else if (resultNode.First == null && resultNode.Second == null)
                    RemoveNodeWithoutChild(resultNode);
                else
                    RemoveNodeWithOneChild(resultNode);
                Size--;

                return resultNode.Item;
            }
            else
                return null;
                
        }

        private void RemoveNodeWithoutChild(BinaryTreeNode<Entry<K, V>> node)
        {
            if (node.First != null || node.Second != null)
                throw new Exception("has child");
            if (node.Parent == null)
            {
                Root = null;
                return;
            }

            if (node.Item.Key.CompareTo(node.Parent.Item.Key) <= 0)
                node.Parent.First = null;
            else
                node.Parent.Second = null;
            node.Parent = null;

        }

        private void RemoveNodeWithOneChild(BinaryTreeNode<Entry<K, V>> node)
        {
            if (node.First != null && node.Second != null)
                throw new Exception("Has two child");
            else if (node.First == null && node.Second == null)
                throw new Exception("Has no child");

            BinaryTreeNode<Entry<K, V>> childNode = node.First == null? node.Second:node.First;
            childNode.Parent = node.Parent;
            node.First = null;
            node.Second = null;

            if (node.Parent == null)
            {
                Root = childNode;
                return;
            }

            if (node.Item.Key.CompareTo(node.Parent.Item.Key) <= 0)
                node.Parent.First = childNode;
            else
                node.Parent.Second = childNode;


            node.Parent = null;
        }

        private void RemoveNodeWithTwoChild(BinaryTreeNode<Entry<K, V>> node)
        {
            if (!(node.First != null && node.Second != null))
                throw new Exception("Has less than two child");

            Queue<BinaryTreeNode<Entry<K, V>>> queue = new Queue<BinaryTreeNode<Entry<K, V>>>();
            queue.Enqueue(node.Second);
            BinaryTreeNode<Entry<K, V>> minNode = node.Second;
            FindMaxMinTree(queue, ref minNode, true);



            if (minNode.First == null && minNode.Second == null)
                RemoveNodeWithoutChild(minNode);
            else
                RemoveNodeWithOneChild(minNode);
            node.Item = minNode.Item;
           
        }

        private BinaryTreeNode<Entry<K, V>> _Find(K key)
        {
            BinaryTreeNode<Entry<K, V>> tempNode = Root;
            while (tempNode != null)
            {
                if (key.CompareTo(tempNode.Item.Key) == 0)
                    return tempNode;
                else if (key.CompareTo(tempNode.Item.Key) < 0)
                    tempNode = tempNode.First;
                else
                    tempNode = tempNode.Second;
            }
            return null;
        }

        private void FindMaxMinTree(Queue<BinaryTreeNode<Entry<K, V>>> queue, ref BinaryTreeNode<Entry<K, V>> ExtremeNode, bool IsMin)
        {
            //Queue<BinaryTreeNode<Entry<K, V>>> newQueue = new Queue<BinaryTreeNode<Entry<K, V>>>();
            while (queue.Count != 0)
            {
                BinaryTreeNode<Entry<K, V>> tempNode = queue.Dequeue();
                if (IsMin)
                {
                    if (ExtremeNode.Item.Key.CompareTo(tempNode.Item.Key) >= 0)
                    {
                        ExtremeNode = tempNode;

                        if (tempNode.First != null)
                            queue.Enqueue(tempNode.First);
                    }
                }
                else
                {
                    if (ExtremeNode.Item.Key.CompareTo(tempNode.Item.Key) <= 0)
                    {
                        ExtremeNode = tempNode;

                        if (tempNode.Second != null)
                            queue.Enqueue(tempNode.Second);
                    }
                }

            }

            //if(newQueue.Count != 0)
            //    FindMaxMinTree(newQueue, ref ExtremeNode, IsMin);
        }
    }

    #endregion

    #region Priority Queue

    public class Entry<K, V>
    {
        private K _Key;

        public K Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private V _Value;

        public V Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }

    interface PriorityQueue<K, V>
    {
        int GetSize();
        bool isEmpty();

        Entry<K, V> Insert(K Key, V Value);
        Entry<K, V> Min();
        Entry<K, V> RemoveMin();

    }

    public class BinaryHeap<K, V> : BinaryTree<Entry<K, V>>, PriorityQueue<K, V>
        where K:IComparable<K>
    {
        public int GetSize()
        {
            return this.Size;
        }

        public bool isEmpty()
        {
            if (this.Size != 0)
                return false;
            else
                return true;
        }

        public Entry<K, V> Insert(K key, V value)
        {
            Entry<K, V> entry = new Entry<K, V>();
            entry.Key = key;
            entry.Value = value;

            if (Root == null)
            {
                Root = new BinaryTreeNode<Entry<K, V>>();
                Root.Item = entry;
                this.Size++;
                return entry;
            }

            Queue<BinaryTreeNode<Entry<K, V>>> queue = new Queue<BinaryTreeNode<Entry<K,V>>>();
            queue.Enqueue(Root);

            BinaryTreeNode<Entry<K, V>> parentNode = FindNULLPos(queue);

            BubbleUp(parentNode, entry);

            this.Size++;
            return entry;
        }

        private void BubbleUp(BinaryTreeNode<Entry<K, V>> parentNode, Entry<K,V> entry)
        {
            Stack<BinaryTreeNode<Entry<K, V>>> stack = new Stack<BinaryTreeNode<Entry<K, V>>>();
            BinaryTreeNode<Entry<K, V>> newNode;

            if (parentNode.First == null)
            {
                newNode = parentNode.First = new BinaryTreeNode<Entry<K, V>>();
                newNode.Parent = parentNode;
            }
            else if (parentNode.Second == null)
            {
                newNode = parentNode.Second = new BinaryTreeNode<Entry<K, V>>();
                newNode.Parent = parentNode;
            }
            else
                throw new Exception("FindNULLPos cheat me!");

            while (entry.Key.CompareTo(newNode.Parent.Item.Key) < 0)
            {
                newNode.Item = newNode.Parent.Item;
                newNode = newNode.Parent;
                if (newNode.Parent == null)
                    break;
            }

            newNode.Item = entry;
        }

        private BinaryTreeNode<Entry<K, V>> FindNULLPos(Queue<BinaryTreeNode<Entry<K, V>>> queue)
        {
            //Queue<BinaryTreeNode<Entry<K, V>>> newQueue = new Queue<BinaryTreeNode<Entry<K, V>>>();
            while(queue.Count>0)
            { 
                BinaryTreeNode<Entry<K, V>> node = queue.Dequeue();
                if (node.First == null || node.Second == null)
                    return node;
                else
                {
                    queue.Enqueue(node.First);
                    queue.Enqueue(node.Second);
                }

            }

            return null;
        }


        public Entry<K, V> Min()
        {
            return Root == null ? null : Root.Item;
        }

        public Entry<K, V> RemoveMin()
        {
            if (this.isEmpty())
                return null;


            Entry<K, V> Min = new Entry<K, V>() { Key = this.Root.Item.Key, Value = this.Root.Item.Value };

            Queue<BinaryTreeNode<Entry<K, V>>> queue = new Queue<BinaryTreeNode<Entry<K,V>>>();
            queue.Enqueue(Root);
            BinaryTreeNode<Entry<K, V>> lastNode = FindLastPos(queue, 0);

            if (lastNode.Parent != null)
            {
                if (lastNode.Parent.Second != null)
                    lastNode.Parent.Second = null;
                else
                    lastNode.Parent.First = null;
            }
            BubbleDown(lastNode.Item);

            Size--;
            return Min;
        }

        private void BubbleDown(Entry<K,V> entry)
        {
            BinaryTreeNode<Entry<K, V>> tempNode = Root;
            BinaryTreeNode<Entry<K, V>> SelectedChild = null;

            if (Root.First == null)
            {
                Root = null;
                return;
            }

            Func<BinaryTreeNode<Entry<K, V>>, BinaryTreeNode<Entry<K, V>>, bool> SelectChildNode = (first, second)=>
                {
                    if(first == null)
                        throw new Exception("No Children");
                    if(second == null)
                    {
                        SelectedChild = first;
                        return true;
                    }
                    else if(first.Item.Key.CompareTo(second.Item.Key) <= 0)
                    {
                        SelectedChild = first;
                        return true;
                    }
                    else
                    {
                        SelectedChild = second;
                        return false;
                    }

                };
            while (entry.Key.CompareTo(SelectChildNode(tempNode.First, tempNode.Second)? tempNode.First.Item.Key : tempNode.Second.Item.Key) > 0)
            {
                tempNode.Item = SelectedChild.Item;
                tempNode = SelectedChild;
                if (tempNode.First == null)
                    break;
            }

            tempNode.Item = entry;
        }

        private BinaryTreeNode<Entry<K, V>> FindLastPos(Queue<BinaryTreeNode<Entry<K, V>>> queue, int depth)
        {
            Queue<BinaryTreeNode<Entry<K, V>>> newQueue = new Queue<BinaryTreeNode<Entry<K, V>>>();
            if (queue.Count() == Math.Pow(2, depth))
            {
                if (queue.Peek().First != null)
                {
                    while (queue.Count() != 0)
                    {
                        BinaryTreeNode<Entry<K, V>> node = queue.Dequeue();

                        if (node.First != null)
                        {
                            newQueue.Enqueue(node.First);
                        }
                        if(node.Second !=null)
                        {
                            newQueue.Enqueue(node.Second);
                        }
                    }
                    depth++;
                    return FindLastPos(newQueue, depth);
                }
                else
                {
                    return queue.Last();
                }
            }
            else
            {
                return queue.Last();
            }
        }
    }
    #endregion
}
