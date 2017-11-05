using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class LoadMethodTrial
    {
        public string Name { set; get; }
        public int Age { set; get; }

        public Result Print(int ID)
        {
            Console.WriteLine("Name : {0}   Age : {1}  ID : {2}", Name, Age, ID);
            return new Result() { Name = this.Name, Age = this.Age, ID = ID };
        }



    }

        public struct Result
        {
            public string Name { set; get; }
            public int Age{set;get;}
            public int ID { set; get; }
        }
}
