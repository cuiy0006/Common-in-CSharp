using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Collection
    {
        public static void ShowCollectionMethods()
        {
            //ShowList();


            //ShowDictionary();

            ShowLinkedList();

        }

        private static void ShowList()
        {
            List<string> list = new List<string>() { "friend", "boy", "girl", "HelloKitty", "environment", "idiot", "collection", "jerk", "abstract" };
            
            //Add(), Remove()
            list.Add("dog");
            if (list.Contains("HelloKitty"))
                list.Remove("HelloKitty");

            //Sort()
            list.Sort();
            list.Sort(new StringComparer());

            //RemoveAt()
            while (list.Count() > 5)
                list.RemoveAt(list.Count() - 1);

            //BinarySearch()  Insert()
            int search = list.BinarySearch("zippo");
            if (search < 0)
            {
                list.Insert(~search, "zippo");
            }

            //findall()
            List<string> ContainO = list.FindAll(item => item.Contains("o"));

            //Test
            List<object> Olist = new List<object>() { "a", "b", 1, 2 };
            object member = Olist[2];
            member = 9;
            PatentData.Print<object>(Olist);
            Console.WriteLine(Environment.NewLine);
            
            PatentData.Print<string>(ContainO);
        }

        private static void ShowDictionary()
        {
            Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
            string value = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                Guid id = Guid.NewGuid();
                value += "a";
                dictionary.Add(id,value);
            }

            
            foreach (KeyValuePair<Guid,string> pair in dictionary)
            {
                Console.WriteLine("{0}    {1}", pair.Key, pair.Value);
            }
        }

        private static void ShowLinkedList()
        {
            LinkedList<string> linklist = new LinkedList<string>();
            linklist.AddFirst("First");
            linklist.AddLast("Last");
            LinkedListNode<string> FirstNode = linklist.First;
            LinkedListNode<string> LastNode = linklist.Last;
            linklist.AddAfter(FirstNode, "1");
            linklist.AddAfter(LastNode, "TrueLast");

            linklist.RemoveFirst();

            PatentData.Print<string>(linklist);

        }
    }


    class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Length > y.Length)
                return 1;
            else if (x.Length == y.Length)
                return 0;
            else
                return -1;
        }
    }
}
