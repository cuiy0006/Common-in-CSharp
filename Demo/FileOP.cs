using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Demo
{
    class FileOP
    {
        public void GiveArgs(int Num, string Str = "Who am I", int Num1 = 2, string str1 = "I am your father")
        {
            Console.WriteLine(Str);
            Console.WriteLine(str1);
            Console.WriteLine(Num1);
        }

        public Tuple<int, string, char> GiveArgsByTuple(int x, string y, char z)
        {
            return Tuple.Create<int, string, char>(x, y, z);
        }

        public int CountDirectoryLineTest(string DirectoryPath)
        {
            int LineCount = 0;
            string[] FilesPath = Directory.GetFiles(DirectoryPath, "*.cs",SearchOption.AllDirectories);
            foreach (string FilePath in FilesPath)
            {
                LineCount += CountFileLine(FilePath);
            }
            return LineCount;
        }
        public int CountDirectoryLine(string DirectoryPath)
        {
            int LineCount = 0;
            string[] FilesPath = Directory.GetFiles(DirectoryPath, "*.cs");
            foreach (string FilePath in FilesPath)
            {
                LineCount += CountFileLine(FilePath);
            }

            string[] OtherDirectoryPath = Directory.GetDirectories(DirectoryPath);
            foreach (string OneDirectoryPath in OtherDirectoryPath)
            {
                LineCount += CountDirectoryLine(OneDirectoryPath);
            }
            return LineCount;
        }

        private int CountFileLine(string FilePath)
        {
            int LineCount = 0;
            using (FileStream FS = new FileStream(FilePath,FileMode.Open))
            {
                using (StreamReader Reader = new StreamReader(FS))
                {
                    string Line = Reader.ReadLine();
                    while (Line != null)
                    {
                        if (Line.Trim() != "")
                        {
                            LineCount++;

                        }
                        Line = Reader.ReadLine();
                    }

                }
            }
            return LineCount;
        }

        public static int ComeCome()
        {
            return 10;
        }
    }
}
