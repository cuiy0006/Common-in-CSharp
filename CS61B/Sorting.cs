using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class Sorting
    {
        #region Insertion Sort
        public void InsertionSort<T>(T[] Array)
            where T : IComparable<T>
        {
            for (int i = 1; i < Array.Length; i++)
            {
                T element = Array[i];
                int pos = -1;
                if (Array[i].CompareTo(Array[i - 1]) >= 0)
                    pos = i;
                else if (Array[i].CompareTo(Array[0]) <= 0)
                    pos = 0;
                else
                    pos = BinarySearchPos<T>(Array, 0, i - 1, Array[i]);

                for (int m = i; m > pos; m--)
                {
                    Array[m] = Array[m - 1];
                }
                Array[pos] = element;
            }
        }

        private int BinarySearchPos<T>(T[] array, int left, int right, T findMe)
            where T : IComparable<T>
        {
            if (left > right)
                throw new Exception("left > right");
            if (right == left + 1)
                return right;
            int mid = (left + right) / 2;
            if (findMe.CompareTo(array[mid]) == 0)
                return mid + 1;
            else if (findMe.CompareTo(array[mid]) > 0)
                return BinarySearchPos<T>(array, mid, right, findMe);
            else
                return BinarySearchPos<T>(array, left, mid, findMe);
        }
        #endregion

        #region Heapsort
        public void Heapsort<T>(ref T[] array)
           where T : IComparable<T>
        {
            var sorted = from element in array
                         orderby element
                         select element;
            array = sorted.ToArray();
        }
        #endregion

        #region MergeSort

        private Queue<LinkedQueue<T>> makeQueueOfQueues<T>(LinkedQueue<T> q)
            where T : IComparable<T>
        {
            Queue<LinkedQueue<T>> result = new Queue<LinkedQueue<T>>();
            int qSize = q.size();
            while (qSize != 0)
            {
                LinkedQueue<T> newQueue = new LinkedQueue<T>();
                newQueue.enqueue(q.dequeue());
                result.Enqueue(newQueue);
                qSize--;
            }
            return result;
        }

        private LinkedQueue<T> mergeSortedQueues<T>(LinkedQueue<T> q1, LinkedQueue<T> q2)
            where T : IComparable<T>
        {
            LinkedQueue<T> newQueue = new LinkedQueue<T>();
            while (q1.size() != 0 && q2.size() != 0)
            {
                if (q1.front().CompareTo(q2.front()) < 0)
                {
                    newQueue.enqueue(q1.dequeue());
                }
                else
                {
                    newQueue.enqueue(q2.dequeue());
                }
            }

            while (q1.size() != 0)
            {
                newQueue.enqueue(q1.dequeue());
            }

            while (q2.size() != 0)
            {
                newQueue.enqueue(q2.dequeue());
            }

            return newQueue;
        }

        public void mergeSort<T>(ref LinkedQueue<T> q)
            where T : IComparable<T>
        {
            Queue<LinkedQueue<T>> TotalQueue = makeQueueOfQueues<T>(q);

            while (TotalQueue.Count() != 1)
            {
                LinkedQueue<T> q1 = TotalQueue.Dequeue();
                LinkedQueue<T> q2 = TotalQueue.Dequeue();
                LinkedQueue<T> q1q2 = mergeSortedQueues<T>(q1, q2);
                TotalQueue.Enqueue(q1q2);
            }

            q = TotalQueue.Dequeue();

        }

        #endregion

        #region QuickSort

        public void QuickSort<T>(ref LinkedQueue<T> inLinkedQueue)
            where T : IComparable<T>
        {
            Patition<T>(inLinkedQueue, inLinkedQueue);
        }

        private void Patition<T>(LinkedQueue<T> Storage, LinkedQueue<T> inLinkedQueue)
            where T:IComparable<T>
        {
            int inLength = inLinkedQueue.size();
            if (inLength == 0)
                return;
            if(inLength == 1)
            {
                Storage.enqueue(inLinkedQueue.dequeue());
                return;
            }

            int pivotIndex = new Random().Next(0, inLength - 1);
            T pivot = inLinkedQueue[pivotIndex];

            LinkedQueue<T> lessQueue = new LinkedQueue<T>();
            LinkedQueue<T> equalQueue = new LinkedQueue<T>();
            LinkedQueue<T> moreQueue = new LinkedQueue<T>();


            while (inLinkedQueue.size() != 0)
            {
                T item = inLinkedQueue.dequeue();
                if (item.CompareTo(pivot) == 0)
                    equalQueue.enqueue(item);
                else if (item.CompareTo(pivot) > 0)
                    moreQueue.enqueue(item);
                else
                    lessQueue.enqueue(item);
            }

            Patition<T>(Storage, lessQueue);
            Storage.append(equalQueue);
            Patition<T>(Storage, moreQueue);
        }


        public void QuickSortOnArray<T>(T[] Array, int Left, int Right)
            where T:IComparable<T>
        {
            if (Right <= Left)
                return;
            int PivotIndex = (Left + Right) / 2;
            T Pivot = Array[PivotIndex];
            Array[PivotIndex] = Array[Right];
            Array[Right] = Pivot;

            int i = Left -1;
            int j = Right;
            while (i < j)
            {
                do { i++; }
                while (Array[i].CompareTo(Pivot) < 0);


                do { j--; }
                while (Array[j].CompareTo(Pivot) > 0 && j > Left);
                
                if (i < j)
                {
                    T temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                }
            }

            Array[Right] = Array[i];
            Array[i] = Pivot;

            QuickSortOnArray<T>(Array, Left, i - 1);
            QuickSortOnArray<T>(Array, i + 1, Right);
        }
        #endregion
    }


    public class QueueNode<T>
        where T:IComparable<T>
    {
        private T _Item;

        public T Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private QueueNode<T> _Parent;

        public QueueNode<T> Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        public QueueNode(T item, QueueNode<T> parent)
        {
            this._Parent = parent;
            this._Item = item;
        }

        public QueueNode(T item)
            : this(item, null)
        {

        }

    }
    public class LinkedQueue<T>
        where T : IComparable<T>
    {
        private QueueNode<T> _Tail;
        private int _Size;
        public LinkedQueue()
        {
            _Tail = null;
            _Size = 0;
        }
        public int size()
        {
            return _Size;
        }
        public void enqueue(T item)
        {
            this._Tail = new QueueNode<T>(item, this._Tail);
            this._Size++;
        }
        public T dequeue()
        {
            QueueNode<T> node = _Tail;
            int tempSize = _Size;
            T item;

            if (tempSize == 1)
            {
                item = _Tail.Item;
                _Tail = null;
                _Size--;
                return item;
            }
            while (tempSize != 2)
            {
                node = node.Parent;

                tempSize--;
            }

            item = node.Parent.Item;
            node.Parent = null;
            _Size--;
            return item;
        }
        public T front()
        {
            QueueNode<T> node = _Tail;
            int tempSize = _Size;
            while (tempSize != 1)
            {
                node = node.Parent;

                tempSize--;
            }
            return node.Item;
        }
        //public String toString();  
        public T this[int index]
        {
            get
            {
                if (_Size == 0)
                    throw new Exception("No element");
                int step = _Size - 1 - index;
                if (step < 0)
                    throw new Exception("out of index");
                QueueNode<T> node = _Tail;
                while (step != 0)
                {
                    node = node.Parent;
                    step--;
                }
                return node.Item;
            }
        }
        public void append(LinkedQueue<T> q)
        {
            if (q.size() == 0)
                throw new Exception("q has no node");

            QueueNode<T> headNode = q._Tail;
            int step = q.size() -1;
            while (step != 0)
            {
                headNode = headNode.Parent;
                step--;
            }

            headNode.Parent = _Tail;
            _Tail = q._Tail;

            this._Size = q.size() + this._Size;
            q._Tail = null;
            q._Size = 0;
        }

    }
   
}
