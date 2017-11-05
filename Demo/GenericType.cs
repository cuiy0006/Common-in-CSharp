using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{

    interface IPair<T>
    {
        T First { set; get; }
        T Second { set; get; }
    }


    public struct Pair<T> : IPair<T>
    {
        public Pair(T first, T second)
        {
            _First = first;
            _Second = default(T);
        }

        public T First
        {
            set { _First = value; }
            get { return _First; }
        }
        public T Second
        {
            set { _Second = value; }
            get { return _Second; }
        }

        T _First;
        T _Second;
    }

    interface IPair2<TFirst, TSecond>
    {
        TFirst First { set; get; }
        TSecond Second { set; get; }
    }

    public struct Pair2<TFirst, TSecond> : IPair2<TFirst, TSecond>
        where TFirst:IComparable<TFirst>
        where TSecond:IComparable<TSecond>
    {
        TFirst _First;
        TSecond _Second;

        public TFirst First
        {
            set { _First = value; }
            get { return _First; }
        }

        public TSecond Second
        {
            set { _Second = value; }
            get { return _Second; }
        }
    }


    public class BinaryTree<T>
        where T : Awesome, IComparable<T>
    {
        public BinaryTree(T item)
        {

            _Item = item;
        }

        public T Item
        {
            set { _Item = value; }
            get { return _Item; }
        }
        T _Item;

        public Pair<BinaryTree<T>> SubItem
        {
            set 
            {
                BinaryTree<T> first = value.First;
                if (first.Item.CompareTo(value.Second.Item)>0)
                {
                    value.Second.Item = this.Item;
                }
            }

            get
            {
                return _SubItem;
            }

        }

        Pair<BinaryTree<T>> _SubItem;
    }

    public class Awesome
    {
        public T Max<T>(T first, params T[] TArray)
            where T : IComparable<T>
        {
            T max = first;
            foreach (T element in TArray)
            {
                if (max.CompareTo(element) < 0)
                {
                    max = element;
                }
            }
            return max;
        }
    }

    public class Aweful:Awesome
    {
        string name;
        public Aweful(string Name)
        {
            name = Name;
        }

        public override string ToString()
        {
            return name;
        }
    }


    interface IReadOnlyPair<out T>
    {
        T First { get; }
        T Second { get;}
    }

    public struct OutPair<T> : IReadOnlyPair<T>
    {
        public OutPair(T first, T second)
        {
            _First = first;
            _Second = second;
        }
        public T First { get { return _First; } }
        public T Second { get { return _Second; } }

        T _First;
        T _Second;
    }

    interface IWriteOnly<in T>
    {
        bool Write(T Thing);
    }

    public class WriteOnly<T> : IWriteOnly<T>
    {
        T Com;
        public bool Write(T Thing)
        {
            Com = Thing;
            return true;
        }
    }



}
