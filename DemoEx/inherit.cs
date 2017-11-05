using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEx
{
    public class inherit
    {
        public virtual string Name { set; get; }
        public int Age { set; get; }
        private int Index { set; get; }

        protected string Alias
        {
            set;
            get;

        }

        public void PrintName()
        {
            Console.WriteLine("Class inherit");
        }
    }

    public class A : inherit
    {
        public string Personality { set; get; }

        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                string[] strName = value.Split(' ');
                base.Name = strName[0];
            }
        }

        public new virtual void PrintName()
        {
            Console.WriteLine("Class A");
        }

        public virtual string PrintAge()
        {
            return "Class A Age";
        }
    }

    public class B : A
    {
        public int PennisLength
        { get; set; }
        
        public override string PrintAge()
        {
            return "Class B Age";
        }

        public override void PrintName()
        {
            Console.WriteLine(base.PrintAge() + "------");
        }


    }

    /// <summary>
    /// Test shadow field
    /// </summary>
    public class C
    {
        int x = 2;
        public virtual void Write()
        {
            Console.WriteLine(x);
        }

        public static void Writelike()
        {
            Console.WriteLine(2);
        }
    }

    public class D : C
    {
        int x = 4;

        public new virtual void Write()
        {
            Console.WriteLine(x);
        }

        public static new void Writelike()
        {
            Console.WriteLine(4);
        }
    }

    public struct StructA
    {
        public int Age;
        public string Name;

        public StructA(string x)
        {
            Age = 1;
            Name = "";

            object xxx = this;
            Console.WriteLine(typeof(StructA));
            
        }
    }


    /// <summary>
    /// Test Same Signature
    /// </summary>
    interface sameFunction
    {
        void MyName(int x);
        void HisName(int x);

        string HerName { set; get; }
    }

    interface Samefunction
    {
        void MyName(int x);
        int HisName();

        string HerName { set; get; }
    }

    public class ShareSameName : sameFunction, Samefunction
    {
        public void MyName(int x)
        { 
        
        }

        public void HisName(int x)
        { 
        
        }

        public int HisName()
        {
            return 0;
        }

        public string HerName
        {
            set;get;
        }
    }



}
