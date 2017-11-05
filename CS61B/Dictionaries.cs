using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CS61B
{
    //Two-word dictionary
    public class Word
    {
        public const int LETTERS = 26, WORDS = LETTERS * LETTERS;
        public string word;

        public int hashCode()
        {
            return LETTERS * (word[0] - 'a') + (word[1] - 'a');
        }
    }

    public class WordDictionary<T>
    {
        private T[] defTable = new T[Word.WORDS];

        public void Insert(Word word, T defination)
        {
            defTable[word.hashCode()] = defination;
        }

        public T Find(Word word)
        {
            return defTable[word.hashCode()];
        }
    }


    //Hash Table
    public class HWord
    {
        private string _Word;
        public string Word
        {
            get { return _Word; }
            set { _Word = value; }
        }
        public override int GetHashCode()
        {
            int hashCodeVal = 0;
            foreach (char c in _Word)
            {
                hashCodeVal = (127 * hashCodeVal + c) % 16908799;
            }
            return hashCodeVal;
        }
    }

    public class HWordComparer : IEqualityComparer<HWord>
    {
        public bool Equals(HWord x, HWord y)
        {
            if (x == null || y == null)
                return false;
            if (x.Word == y.Word)
                return true;
            else
                return false;
        }

        public int GetHashCode(HWord obj)
        {
            return obj.GetHashCode();
        }
    }

    public abstract class IHDictionary<K, V>
    {
        public abstract void Insert(K key, V value);
        public abstract bool ContainKey(K key);
        public abstract void Remove(K key);
        public abstract V this[K key] { set;  get; }
        public abstract int Count();
    }

    public class HDictionary<K,V>:IHDictionary<K,V>
    {
        private int _BucketSize = 1;
        public int BucketSize
        {
            get { return _BucketSize; }
            private set { _BucketSize = value; }
        }

        private int ActualSize = 0;

        private IEqualityComparer<K> _Comparer;
        private HSLinkList<K, V>[] defChain;
        public HDictionary(IEqualityComparer<K> comparer)
        {
            _Comparer = comparer;
            defChain = new HSLinkList<K, V>[_BucketSize];
        }

        public override void Insert(K key, V value)
        {
            int ChainPosition = Compression(key.GetHashCode());

            HSLinkList<K, V> Temp = defChain[ChainPosition];
            if (Temp == null)
                defChain[ChainPosition] = new HSLinkList<K, V>(_Comparer);
            defChain[ChainPosition].InsertFront(key, value);
            ActualSize++;
            Resize();
        }

        public override bool ContainKey(K key)
        {
            int ChainPosition = Compression(key.GetHashCode());

            if (defChain[ChainPosition] == null)
                return false;
            else
                return defChain[ChainPosition].ContainKey(key);
        }

        public override void Remove(K key)
        {
            int ChainPosition = Compression(key.GetHashCode());

            if (defChain[ChainPosition] == null)
                throw new Exception("Key doesn't exist");
            else
                defChain[ChainPosition].Remove(key);
            ActualSize--;
            if(ActualSize != 0)
                Resize();
        }

        public override V this[K key]
        {
            get 
            {
                int ChainPosition = Compression(key.GetHashCode());
                return defChain[ChainPosition].GetValue(key);
            }
            set 
            {
                int ChainPosition = Compression(key.GetHashCode());
                defChain[ChainPosition].SetValue(key, value);
            }
        }

        public override int Count()
        {
            return ActualSize;
        }

        private void Resize()
        {
            int NewBucketSize = _BucketSize;
            if (_BucketSize / ActualSize > 4)
            {
                for (int i = (ActualSize + 1) / 2; i <= _BucketSize; i++)
                {
                    if (IsPrime(i))
                    {
                        NewBucketSize = i;
                        break;
                    }
                }
            }
            else if (ActualSize / _BucketSize >= 4)
            {
                for (int i = 2 *(ActualSize + 1); i <= 10 * ActualSize; i++)
                {
                    if (IsPrime(i))
                    {
                        NewBucketSize = i;
                        break;
                    }
                }
            }
            else
                return;

            HSLinkList<K, V>[] NewList = new HSLinkList<K, V>[NewBucketSize];
            foreach (var item in defChain)
            {
                if (item != null)
                {
                    foreach (var subitem in item)
                    {
                        if (subitem != null)
                        {
                            dynamic temp = subitem;
                            //NewList.(temp.key, temp.value);
                            int Position = temp.key.GetHashCode() % NewBucketSize;
                            if(NewList[Position] == null)
                                NewList[Position] = new HSLinkList<K, V>(_Comparer);
                            NewList[Position].InsertFront(temp.key, temp.value);
                            
                        }
                    }
                }
            }
            this.defChain = NewList;
            this._BucketSize = NewBucketSize;
        }

        private int Compression(int hashCode)
        {
            return hashCode % _BucketSize;
        }

        private bool IsPrime(int number)
        {
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

    }

    class HListNode<K, V>
    {
        public K Key;
        public V Value;
        public HListNode<K,V> Next;
        public HListNode(K key, V value, HListNode<K,V> next)
        {
            this.Next = next;
            this.Value = value;
            this.Key = key;
        }

        public HListNode(K key, V value)
            : this(key, value, null)
        {
            
        }
    }
  
    class HSLinkList<K, V>:IEnumerable<object>
    {
        private HListNode<K, V> _Head;
        //private int _Size;
        IEqualityComparer<K> _Comparer;
        public HSLinkList(IEqualityComparer<K> comparer)
        {
            _Head = null;
            //_Size = 0;
            _Comparer = comparer;
        }

        public void InsertFront(K key, V value)
        {
            if (this.ContainKey(key))
                throw new Exception("Key already exists");
            _Head = new HListNode<K, V>(key, value, _Head);
        }

        public void Remove(K key)
        {
            HListNode<K, V> Temp = _Head;
            HListNode<K, V> TempPrev = null;
            Func<K, K, bool> IsEqual;
            if (_Comparer == null)
                IsEqual = (k1, k2) => k1.Equals(k2);
            else
                IsEqual = (k1, k2) => _Comparer.Equals(k1, k2);

            while (!IsEqual(Temp.Key,key))
            {
                TempPrev = Temp;
                Temp = Temp.Next;
            }

            if (Temp == null)
                throw new Exception("No Key Exists");

            if (TempPrev == null)
                _Head = Temp.Next;
            else
                TempPrev.Next = Temp.Next;
        }

        public bool ContainKey(K key)
        {

            if (_Head == null)
                return false;

            HListNode<K, V> Temp = _Head;
            HListNode<K, V> TempPrev = null;

            Func<K, K, bool> IsEqual;
            if (_Comparer == null)
                IsEqual = (k1, k2) => k1.Equals(k2);
            else
                IsEqual = (k1, k2) => _Comparer.Equals(k1, k2);

            while (!IsEqual(Temp.Key, key))
            {
                TempPrev = Temp;
                Temp = Temp.Next;
                if (Temp == null)
                    break;
            }

            if (Temp == null)
                return false;
            else
                return true;
        }

        public V GetValue(K key)
        {
            if (_Head == null)
                throw new Exception("Key doesn't exist");

            HListNode<K, V> Temp = _Head;
            HListNode<K, V> TempPrev = null;

            Func<K, K, bool> IsEqual;
            if (_Comparer == null)
                IsEqual = (k1, k2) => k1.Equals(k2);
            else
                IsEqual = (k1, k2) => _Comparer.Equals(k1, k2);

            while (!IsEqual(Temp.Key, key))
            {
                TempPrev = Temp;
                Temp = Temp.Next;
                if (Temp == null)
                    break;
            }

            if (Temp == null)
                throw new Exception("Key doesn't exist");
            else
                return Temp.Value;
        }
        public void SetValue(K key, V value)
        {
            if (_Head == null)
                throw new Exception("Key doesn't exist");

            HListNode<K, V> Temp = _Head;
            HListNode<K, V> TempPrev = null;

            Func<K, K, bool> IsEqual;
            if (_Comparer == null)
                IsEqual = (k1, k2) => k1.Equals(k2);
            else
                IsEqual = (k1, k2) => _Comparer.Equals(k1, k2);

            while (!IsEqual(Temp.Key, key))
            {
                TempPrev = Temp;
                Temp = Temp.Next;
                if (Temp == null)
                    break;
            }
            if (Temp == null)
                throw new Exception("Key doesn't exist");
            else
                Temp.Value = value;

        }

        public IEnumerator<object> GetEnumerator()
        {
            if (_Head == null)
                yield return null;
            else
            {
                yield return new { key = _Head.Key, value = _Head.Value };
                var Current = _Head.Next;
                while (Current!= null)
                {
                    yield return new { key = Current.Key, value = Current.Value };
                    Current = Current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
