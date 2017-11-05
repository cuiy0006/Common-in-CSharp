using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Linq;
using System.Dynamic;

namespace Demo
{
    class ReflectionAttributeDynamic
    {
        public static void ShowReflection()
        { 
            /// Type
            ThreadPriorityLevel priority;
            //Enum.TryParse<ThreadPriorityLevel>("Idle", out priority);
            priority = (ThreadPriorityLevel)Enum.Parse(typeof(ThreadPriorityLevel), "Idle");
            Console.WriteLine(priority + Environment.NewLine);
            
            //Load DLL or Load local class
            var DLL = Assembly.LoadFile(@"C:\Users\CUIYAO\Documents\visual studio 2013\Projects\Demo\Reflection\bin\Debug\Reflection.dll");
            var type = DLL.GetType("Reflection.LoadMethodTrial");
            var instance = DLL.CreateInstance("Reflection.LoadMethodTrial");

            //Then set property
            PropertyInfo propertyName = type.GetProperty("Name");
            PropertyInfo PropertyAge = type.GetProperty("Age");
            PropertyAge.SetValue(instance, 3);
            propertyName.SetValue(instance, "CY");

            //Exec Method
            MethodInfo MethodPrint = type.GetMethod("Print");
            dynamic result0 = MethodPrint.Invoke(instance, new object[] { 9 }); //dynamic get property directly
            string Name0 = result0.Name;
            int Age0 = result0.Age;
            int ID0 = result0.ID;

            var result = MethodPrint.Invoke(instance, new object[] { 9 });
            //Handle return value
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(result);
            PropertyDescriptor pdName = pdc.Find("Name", true);
            PropertyDescriptor pdAge = pdc.Find("Age", true);
            PropertyDescriptor pdID = pdc.Find("ID", true);

            string Name = pdName.GetValue(result).ToString();
            int Age = (int)pdAge.GetValue(result);
            int ID = (int)pdID.GetValue(result);

        }

        public static void ShowGenericReflection()
        {
            // Judge Generic
            Type type = typeof(System.Nullable<DateTime>);
            Console.WriteLine(type.IsGenericType);

            type = typeof(string);
            Console.WriteLine(type.IsGenericType);

            // Get Generic parameters' types
            Dictionary<string, int> Dic = new Dictionary<string,int>();
            Type t = Dic.GetType();
            foreach(Type tp in t.GetGenericArguments())
            {
                Console.WriteLine(tp.FullName);
            }

        }


        public static void ShowDynamic()
        {
            //DynamicExample();
            DynamicXmlExample();
        }

        private static void DynamicExample()
        {
            dynamic data = "Hello! My name is CUI YAO";
            Console.WriteLine(data);

            data = (int)data.Length;
            data = data * 10 + 50;
            if (data == 300)
            {
                Console.WriteLine("{0} How Great he is!", data);
            }
        }

        private static void DynamicXmlExample()
        { 
            //Normally
            XElement person = XElement.Parse(
                @"<Person>
                   <FirstName>YAO</FirstName>
                   <LastName>CUI</LastName>
                  </Person>");
            Console.WriteLine("{0}  {1}", person.Descendants("FirstName").FirstOrDefault().Value, person.Descendants("LastName").FirstOrDefault().Value);

            dynamic person0 = DynamicXml.Parse(
                @"<Person>
                   <FirstName>YAO</FirstName>
                   <LastName>
                        <FirstChar>C</FirstChar>
                        <SecondChar>U</SecondChar>
                        <ThirdChar>I</ThirdChar>
                    </LastName>
                  </Person>");
            Console.WriteLine("{0}  {1}", person0.FirstName, person0.LastName.FirstChar + person0.LastName.SecondChar + person0.LastName.ThirdChar);

        }



    }

    class DynamicXml : DynamicObject
    {
        private XElement Element { set; get; }

        public DynamicXml(XElement element)
        {
            Element = element;
        }

        public static DynamicXml Parse(string text)
        {
            return new DynamicXml(XElement.Parse(text));
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) //binder -> property
        {
            result = null;
            bool success = false;

            XElement firstDescendant = Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDescendant != null)
            {
                if (firstDescendant.Descendants().Count() > 0)
                {
                    result = new DynamicXml(firstDescendant);
                }
                else
                {
                    result = firstDescendant.Value;
                }
                success = true;
            }
            return success;

        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            bool success = false;
            XElement firstDescendant = Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDescendant != null)
            {
                if (value.GetType() == typeof(XElement))
                {
                    firstDescendant.ReplaceWith(value);
                }
                else
                {
                    firstDescendant.Value = value.ToString();
                }
                success = true;
            }
            return success;
        }
    }
}
