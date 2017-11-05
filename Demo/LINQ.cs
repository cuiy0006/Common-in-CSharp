using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class LINQ
    {
        public static void ShowLINQ()
        {
            //////LINQ

            //IEnumerable<string> KeywordsStar = from word in LINQ.KeyWords
            //                              where !word.Contains("*")
            //                              select word;

            //PatentData.Print(KeywordsStar);
            //Console.WriteLine(Environment.NewLine);

            ///// from where Select
            //var KeywordCollection = from word in LINQ.KeyWords
            //                                        where !word.Contains("*")
            //                                        select new { word,
            //                                           firstChar = word[0],
            //                                           secondChar = word[1]};

            //PatentData.Print(KeywordCollection);
            //Console.WriteLine(Environment.NewLine);

            ///// from where Select orderby let
            //Func<string, int> CalLength = word => word.Length;

            //var KeywordShortCollection = from word in LINQ.KeyWords
            //                             let Length = word.Length
            //                            where LINQ.IsWordShort(word)
            //                            orderby Length,word descending
            //                            select new{word, length = CalLength(word)};

            //PatentData.Print(KeywordShortCollection);
            //Console.WriteLine(Environment.NewLine);


            //var PatentsAfterInsect = PatentData.Patents.SelectMany(patent => patent.InventorIds,
            //    (P, I) => new
            //    {
            //        ID = I,
            //        Title = P.Title
            //    });
            ////PatentData.Print(PatentsAfterInsect);


            ///// from where Select into 
            //var PatentGroup = from item in PatentsAfterInsect//PatentData.Patents
            //                  from patent in PatentData.Patents
            //                  where item.Title == patent.Title
            //                  select new { patent.Title, patent.YearOfPublication, item.ID}
            //                  into groups
            //                  select new {groups.Title, groups.YearOfPublication, groups.ID};

            //PatentData.Print(PatentGroup); 



            ///// left join

            //var PatentsDistinct = PatentData.Patents.SelectMany(patent => patent.InventorIds,
            //    (P, I) => new
            //    {
            //        ID = I,
            //        Title = P.Title
            //    });
            ////PatentData.Print(PatentsAfterInsect);

            //var PatentGroup = from patent in PatentsDistinct
            //                  join inventor in PatentData.Inventors on patent.ID equals inventor.Id
            //                  into Group
            //                  from inventor in Group.DefaultIfEmpty()
            //                  select new
            //                      {
            //                          InventorName = inventor != null? inventor.Name:null,
            //                          Title = patent.Title,
            //                          ID = patent.ID,

            //                      };

            //PatentData.Print(PatentGroup);
            //Console.Write(Environment.NewLine);



            /////right join
            /////
            //var PatentGroupRight = from inventor in PatentData.Inventors
            //                       join patent in PatentsDistinct on inventor.Id equals patent.ID
            //                       into Group
            //                       from patent in Group.DefaultIfEmpty()
            //                       select new
            //                           {
            //                               InventorName = inventor.Name,
            //                               Title = patent != null ? patent.Title : null,
            //                               ID = patent != null ? patent.ID : -1
            //                           };

            //PatentData.Print(PatentGroupRight);  
        }


        public static string[] KeyWords = new string[] { "abstract","add","alias","as","ascending*",
        "async*","await*","base","bool","break",
        "by*","byte","case","catch","char","checked"};

        public static bool IsWordShort(string word)
        {
            return word.Length < 5;
        }
    }


}
