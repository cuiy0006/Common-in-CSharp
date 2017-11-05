using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace DemoEx
{
    /// <summary>
    /// Extension Method
    /// </summary>
    public static class DirectoryExtention
    {
        // class
        public static void CopyAt(this DirectoryInfo DirectorySource, int a, string b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.Read();
        }

        //interface
        public static void SetAt<T>(this IComparable<T>[] obj, int[] Ints)
        {
            
            IEnumerator Enumerator = Ints.GetEnumerator();
            while (Enumerator.MoveNext())
            {

                Console.WriteLine(Enumerator.Current);
            }
        }

    }


    public class TestThis
    {
        public static void TestConvertAnB()
        {
            ConvertClassA A = new ConvertClassA();
            A.Name = "CY";
            ConvertClassB B = (ConvertClassB)A; // Explicit
            ConvertClassA C = B;                // Implicit

            ConvertClassA D = A + B;            // +

            if (D != B)                         // !=
            { 
                
            }

            if (A == B)                         // ==
            { 
            
            }


            Pair<string> p = new Pair<string>("FFF", "SSS"); //Index
            string PItem = p[PairItem.First];
            p[PairItem.First] = "TTT";
            PItem = p[PairItem.First];
        }
    }
    /// <summary>
    /// Overload Convert and Operator
    /// </summary>
    public class ConvertClassA
    {
        public string Name;

        public static explicit operator ConvertClassB(ConvertClassA obj)
        {
            return new ConvertClassB() { _Name = obj.Name };
        }

        public static ConvertClassA operator +(ConvertClassA objA, ConvertClassB objB)
        {
     
            return new ConvertClassA() { Name = objA.Name + objB._Name };
        }


        public static bool operator !=(ConvertClassA objA, ConvertClassB objB)
        {
            if (objA.Name == objB._Name)
                return false;
            else
                return true;
        }


        public static bool operator ==(ConvertClassA objA, ConvertClassB objB)
        {
            if (objA.Name == objB._Name)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            int NameHashCode = Name.GetHashCode();
            return NameHashCode * 11 ^ NameHashCode;
        }
    }

    public class ConvertClassB
    {
        public string _Name;

        public static implicit operator ConvertClassA(ConvertClassB obj)
        {
            return new ConvertClassA() { Name = obj._Name};
        }
    }

    /// <summary>
    /// Index
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPair<T>
    {
        T First { get; }
        T Second { get; }
        T this[PairItem Index]
        {
            get;
        }
    }

    public enum PairItem
    { 
        First,
        Second
    }

    public class Pair<T> : IPair<T>
    {
        private T _First;
        private T _Second;

        public Pair(T first, T second)
        {
            this._First = first;
            this._Second = second;
        }

        public T First
        {
            private set { _First = value; }
            get { return _First;}
        }

        public T Second
        {
            private set { _Second = value; }
            get { return _Second; }
        }

        public T this[PairItem Index]
        {
            get
            {
                switch (Index)
                {
                    case PairItem.First:
                        return First;
                    case PairItem.Second:
                        return Second;
                    default:
                        throw new Exception();
                }
            }
            set
            {
                switch (Index)
                {
                    case PairItem.First:
                        First = value;
                        break;
                    case PairItem.Second:
                        Second = value;
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
    }

    public struct mnbv
    {
        public string jet;
        public int Seeek;

        public mnbv(string jeke)
        {
            jet = "acc";
            Seeek = 19;
        }
    }

    public class mnbvCollection
    {
        public mnbv MNBV
        {
            set
            {
                MMMM = value;
            }

            get
            {
                return MMMM;
            }
        }

        private mnbv MMMM;
    }
}
