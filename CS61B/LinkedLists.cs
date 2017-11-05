using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class LinkedLists
    {
        //Array-based lists:
        //Advantages: Very fast access of each item
        //Disadvatages: 1) insert item in the beginning or middle
        //                 takes time proportional to the length of the list
        //              2) Array has a fixed length

        //Linked Lists: (a recursively data type)
        //Made up of nodes. Each node has:
        //1) an item
        //2) a reference to next node in list
        //Advantage :
        //1) inserting item into middle of linked list takes constant time (if have a reference of previous node)
        //2) list can keep growing until memory runs out.
        //3) if enforces 2 invariants 1. "size" is always correct 2. a list is never circularly linked, the tail node's next is null
        //Disadvantage:
        //Finding nth item of a linked list, time proportional to n, start at head, walk n-1 nodes
    }

    //Problem with SlistNodes
    //1) Insert new item at beginning of the list fisrtnode = mew SListNode("Soap", firstnode);
    //2) How represent an empty list? x= null; x.InsetAfterThis(item);
    // Solution: seperate Slist Class maintain the head of the list.
    public class SListNode<T>
    {
        public T Item;
        public SListNode<T> Next;
        public SListNode(T item, SListNode<T> next)
        {
            this.Next = next;
            this.Item = item;
        }

        public SListNode(T item) : this(item, null)
        {

        }

        public void InsertAfterThis(T item)
        {
            this.Next = new SListNode<T>(item, this.Next);
        }
    }

    public class SList<T>
    {
        private SListNode<T> Head;
        private int size;
        public SList()
        {
            Head = null;
            size = 0;
        }

        public void InsertFront(T item)
        {
            this.Head = new SListNode<T>(item, this.Head);
            size++;
        }


    }

    //Double Linked List
    //S Insering/deleting at the end of the list difficult

    public class DListNode<T>
    {
        public T Item { set; get; }
        public DListNode<T> Next { set; get; }

        public DListNode<T> Prev { set; get; }


        public DListNode(T item, DListNode<T> next, DListNode<T> prev)
        {
            this.Item = item;
            this.Next = next;
            this.Prev = prev;
        }
        public void InsertAfterThis(T item)
        {
            DListNode<T> node = new DListNode<T>(item, this.Next, this);
            if (this.Next != null)
                this.Next.Prev = node;
            this.Next = node;
        }
    }

    public class DList<T>
    {
        private DListNode<T> _Head;
        private DListNode<T> _Tail;
        private long _Size;

        public DList()
        {
            _Head = null;
            _Tail = null;
            _Size = 0;
        }

        public void InsertEnd(T item)
        {
            DListNode<T> node = new DListNode<T>(item, null, this._Tail);
            if (this._Tail != null)
                _Tail.Next = node;
            this._Tail = node;

            if (this._Head == null)
                this._Head = this._Tail;

            _Size++;
        }

        public void InsertFront(T item)
        {
            DListNode<T> node = new DListNode<T>(item, this._Head, null);
            if (this._Head != null)
                this._Head.Prev = node;
            this._Head = node;

            if (this._Tail == null)
                this._Tail = this._Head;
            _Size++;
        }

        public void RemoveTail() // 2 or more
        {
            if (this._Tail == null)
                throw new Exception();

            if (this._Tail.Prev == null)
            {
                this._Tail = null;
                this._Head = null;
            }
            else
            {
                this._Tail.Prev.Next = null;
                this._Tail = this._Tail.Prev;
            }

            _Size--;
        }
    }

    //sentinel node -----> special node not store item
    // There is always a sentinel node, so This.Head != null
    // its previous node is Head node
    // its Next node is tail node
    // Tail node is no longer needed
    // Head node is pointed to sentinel node////  sentinel node.next.next...next = sentinel node
    // Size not count sentinel node

    public class SentinelList<T>
    {
        private DListNode<T> _Head;
        private long _Size;

        public SentinelList()
        {
            _Head = new DListNode<T>(default(T), null, null);
            _Head.Next = _Head;
            _Head.Prev = _Head;
            _Size = 0;
        }

        public void InsertFront(T item)
        {

            DListNode<T> node = new DListNode<T>(item, this._Head.Next, this._Head);
            this._Head.Next.Prev = node;
            this._Head.Next = node;
            _Size++;
        }

        public void InsertEnd(T item)
        {
            DListNode<T> node = new DListNode<T>(item, this._Head, this._Head.Prev);
            this._Head.Prev.Next = node;
            this._Head.Prev = node;
            _Size++;
        }

        public void RemoveTail()
        {
            if (this._Head.Prev == this._Head)
            {
                throw new Exception();
            }

            this._Head.Prev.Prev.Next = this._Head;
            this._Head.Prev = _Head.Prev.Prev;
            _Size--;
        }
    }

    public abstract class IQueue<T>
    {
        public abstract void Enqueue(T value);
        public abstract T Dequeue();
        public abstract T Peek();
        public abstract int Count();
        public abstract void Reverse();
        //Normally, queue does not provide features below.
        public abstract bool Contains(T value);
        public abstract T this[int index] { get; set; }
        public abstract T Bottom();
    }

    public class myQueue<T> : IQueue<T>, IEnumerable<T>
    {                          
        private ListNode<T> _head;
        private ListNode<T> _tail;
        private int _size;
        
        public override int Count()
        {
            return _size;
        }

        public override void Enqueue(T value)
        {
            if (_head == null)
            {
                _head = new ListNode<T>(value);
                _tail = _head;
            }
            else
            {
                _tail.Next = new ListNode<T>(value);
                _tail = _tail.Next;
            }
            _size++;
        }

        public override T Dequeue()
        {
            if (_head == null)
                throw new Exception("queue is empty");
            _size--;
            ListNode<T> node = _head;
            _head = _head.Next;
            if (_head == null)
                _tail = null;
            return node.Value;
        }

        public override T Peek()
        {
            if (_head == null)
                throw new Exception("Queue is empty");
            return _head.Value;
        }

        public override T Bottom()
        {
            if(_head == null)
                throw new Exception("Queue is empty");
            return _tail.Value;
        }

        public override bool Contains(T value)
        {
            if (_head == null)
                return false;
            ListNode<T> node = _head;
            while (node != null)
            {
                if (node.Value.Equals(value))
                    return true;
                node = node.Next;
            }
            return false;
        }

        public override void Reverse()
        {
            if (_head == null || _head.Next == null)
                return;
            ListNode<T> curr = _head;
            ListNode<T> last = _head;
            ListNode<T> next = _head.Next;
            _head.Next = null;
            while (next != null)
            {
                curr = next;
                next = next.Next;
                curr.Next = last;
                last = curr;
            }
            _tail = _head;
            _head = curr;
        }

        public override T this[int index]
        {
            get
            {
                ListNode<T> node = _head;
                if(node == null)
                    throw new Exception("Queue is empty");
                if (index < 0)
                    throw new Exception("Index out of range");
                while (index > 0)
                {
                    if(node == null)
                        throw new Exception("Index out of range");
                    node = node.Next;
                    index--;
                }
                return node.Value;
            }

            set
            {
                ListNode<T> node = _head;
                if (node == null)
                    throw new Exception("Queue is empty");
                if (index < 0)
                    throw new Exception("Index out of range");
                while (index > 0)
                {
                    if (node == null)
                        throw new Exception("Index out of range");
                    node = node.Next;
                    index--;
                }
                node.Value = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListNode<T> node = _head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ListNode<T>
    {
        public T Value;
        public ListNode<T> Next;
        public ListNode(T value):this(value, null)
        {
        }

        public ListNode(T value, ListNode<T> next)
        {
            this.Value = value;
            this.Next = next;
        }
    }
}
