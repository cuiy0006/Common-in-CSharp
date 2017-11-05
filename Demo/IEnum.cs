using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class IEnum
    {
        public static void ShowEnum()
        { 
        
            //IEnumerable<Patent> Patents = PatentData.Patents;

            //IEnumerable<Patent> IfPatent = Patents.Where(item =>item.YearOfPublication.StartsWith("17"));

            //string[] Titles = Patents.Where(item => item.YearOfPublication.StartsWith("17")).Select(item =>
            //    {
            //        return item.Title.ToString();
            //    }
            //    ).ToArray();

            //PatentData.Print<Patent>(IfPatent);

            //PatentData.Print<string>(Titles);

            ///
            //IEnumerable<Patent> Patents = PatentData.Patents;
            //bool result;

            //Patents = Patents.Where(patent =>
            //    {
            //        if (result = patent.YearOfPublication.StartsWith("19"))
            //        {
            //            Console.WriteLine(patent.Title);
            //        }
            //        return result;
            //    });

            //Console.WriteLine("1. Foreach");
            //foreach (Patent patent in Patents)
            //{ 
            
            //}

            //Console.WriteLine("2. Count()");
            //Console.WriteLine(Patents.Count());

            //Console.WriteLine("3. ToArray() + Count()");
            //Patents = Patents.ToArray();
            //Console.WriteLine(Patents.Count());


            ///
            //IEnumerable<Patent> Patents = PatentData.Patents.OrderBy(patent =>
            //    {
            //        return patent.YearOfPublication;
            //    }).ThenBy(patent =>
            //    {
            //        return patent.Title;
            //    });

            //PatentData.Print<Patent>(Patents);


            ///
            //var Patents = PatentData.Patents.Join(PatentData.Connections,
            //    P => P.Title,
            //    C => C.PatentTitle,
            //    (P, C) => new { 
            //        P.Title, C.InventorId
            //    }
            //    );

            //var Inventors = PatentData.Inventors.Join(PatentData.Connections,
            //    I => I.Id,
            //    C => C.InventorId,
            //    (I, C) => new{
            //        I.Id,
            //        C.PatentTitle
            //    }
            //    );

            //var PandIConnection = Patents.Join(PatentData.Inventors,
            //    P => P.InventorId,
            //    I => I.Id,
            //    (P, I) => new
            //    {
            //        P.Title,
            //        P.InventorId,
            //        I.Name
            //    }
            //    );

            

            //PatentData.Print(Patents);
            //Console.WriteLine(Environment.NewLine);
            //PatentData.Print(Inventors);
            //Console.WriteLine(Environment.NewLine);
            //PatentData.Print(PandIConnection);


            ///

            //var Patents = PatentData.Patents.SelectMany(patent => patent.InventorIds,(P,I)=>new
            //{
            //    P.Title,
            //    P.YearOfPublication,
            //    InventorID =I
            //}
            //);

            //var PandIConnection = Patents.Join(PatentData.Inventors, P => P.InventorID, I => I.Id, (P, I) => new
            //{
            //    P.InventorID,P.Title,I.Name
            //});

            //PatentData.Print(PandIConnection);
        }
    }

    public class Patent
    {
        public string Title { set; get; }

        public string YearOfPublication { set; get; }

        public string ApplicationNumber { set; get; }

        public long[] InventorIds { set; get; }

        public override string ToString()
        {
            return string.Format("{0},{1}", Title, YearOfPublication);
        }
    }

    public class Inventor
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string City { set; get; }
        public string State { set; get; }

        public string Country { set; get; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", City, State, Country);
        }
    }

    public class Connection
    {
        public long InventorId{set;get;}
        public string PatentTitle{set;get;}
    }

    public class PatentData
    {
        public static readonly Inventor[] Inventors = new Inventor[]
        {
            new Inventor(){Name = "1", City ="1City",State="1State",Country="1Country",Id=1},
            new Inventor(){Name = "2", City ="2City",State="2State",Country="2Country",Id=2},
            new Inventor(){Name = "3", City ="1City",State="3State",Country="3Country",Id=3},
            new Inventor(){Name = "4", City ="4City",State="4State",Country="4Country",Id=4},
            new Inventor(){Name = "5", City ="5City",State="5State",Country="5Country",Id=5},
            new Inventor(){Name = "6", City ="6City",State="6State",Country="6Country",Id=6}
        };
        public static readonly Patent[] Patents = new Patent[]
        {
            new Patent(){Title="a",YearOfPublication="1890",InventorIds=new long[]{1}},
            new Patent(){Title="b",YearOfPublication="1791",InventorIds=new long[]{1}},
            new Patent(){Title="c",YearOfPublication="1992",InventorIds=new long[]{1}},
            new Patent(){Title="d",YearOfPublication="1793",InventorIds=new long[]{4}},
            new Patent(){Title="e",YearOfPublication="1694",InventorIds=new long[]{2,3}},
            new Patent(){Title="f",YearOfPublication="1995",InventorIds=new long[]{5}},
            new Patent(){Title="g",YearOfPublication="1996",InventorIds=new long[]{7}},
            new Patent(){Title="h",YearOfPublication="1997",InventorIds=new long[]{7}}
        };

        public static readonly Connection[] Connections = new Connection[]
        { 
            new Connection(){InventorId = 1, PatentTitle = "a"},
            new Connection(){InventorId = 1, PatentTitle = "b"},
            new Connection(){InventorId = 1, PatentTitle = "c"},
            new Connection(){InventorId = 2, PatentTitle = "e"},
            new Connection(){InventorId = 3, PatentTitle = "e"},
            new Connection(){InventorId = 4, PatentTitle = "d"},
            new Connection(){InventorId = 5, PatentTitle = "f"},

        };

        public static void Print<T>(IEnumerable<T> Collection)
        {
            foreach (T Item in Collection)
            {
                Console.WriteLine(Item.ToString());
            }
        }


    }


}
