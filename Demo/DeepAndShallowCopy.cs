using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public static class DeepAndShallowCopy
    {
        public static void test()
        {
            Classroom ClRoom = new Classroom(){RoomID = 1};
            Student st = new Student() { Name = "CY", ID = 15, CLRoom = ClRoom };
            Student stShallow = st.ShallowCopy();
            Student stDeep = st.DeepCopy();

            Console.WriteLine("Deep Copy :{0}", st.CLRoom.Equals(stDeep.CLRoom));//st.CLRoom== stDeep.CLRoom);//object.ReferenceEquals(st.CLRoom, stDeep.CLRoom));
            Console.WriteLine("Shallow Copy :{0}", st.CLRoom.Equals(stShallow.CLRoom));//st.CLRoom == stShallow.CLRoom);//object.ReferenceEquals(st.CLRoom, stShallow.CLRoom));



            ////Array Test
            //Classroom[] clArray = new Classroom[3];
            //Classroom clroom1 = new Classroom() { RoomID = 1 };
            //Classroom clroom2 = new Classroom() { RoomID = 2 };
            //Classroom clroom3 = new Classroom() { RoomID = 3 };

            //clArray[0] = clroom1;
            //clArray[1] = clroom2;
            //clArray[2] = clroom3;

            //Classroom[] CLArrayClone = (Classroom[])clArray.Clone();
            //Classroom[] CLArrayCopy = new Classroom[10];

            //Array.Copy(clArray, CLArrayCopy, clArray.Length);

            //CLArrayCopy[0].RoomID = 10;

            //Console.WriteLine(object.ReferenceEquals(clroom1, CLArrayClone[0]));
            //Console.WriteLine(object.ReferenceEquals(clroom1, CLArrayCopy[0]));
        }
    }

    interface IDeepCopy<T>
    {
        T DeepCopy();
    }

    interface IShallowCopy<T>
    {
        T ShallowCopy();
    }

    public class Student : IDeepCopy<Student>, IShallowCopy<Student>
    {
        public int ID { set; get; }
        public string Name { set; get; }

        public Classroom CLRoom { set; get; }

        public Student DeepCopy()
        {
            Student st = new Student();
            st.ID = this.ID;
            st.Name = this.Name;
            st.CLRoom = this.CLRoom.DeepCopy();
            return st;
        }

        public Student ShallowCopy()
        {
            return (Student)this.MemberwiseClone();
        }

    }

    public class Classroom : IDeepCopy<Classroom>, IShallowCopy<Classroom>
    {
        public int RoomID { set; get; }

        public Classroom DeepCopy()
        {
            Classroom CLRoom = new Classroom() { RoomID = this.RoomID };
            return CLRoom;
        }

        public Classroom ShallowCopy()
        {
            return (Classroom)this.MemberwiseClone();
        }
    }
}
