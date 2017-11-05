using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Demo
{
    class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            OPs.Add("^");
            OPs.Add("*");
            OPs.Add("/");
            OPs.Add("+");
            OPs.Add("-");
            Console.Write(">>>");
            string str = Console.ReadLine();
            while (str != "exit")
            {
                if(str.Contains("clear function "))
                {
                    string funcName = str.Substring("clear function ".Length);
                    if (func_dic.ContainsKey(funcName))
                    {
                        func_dic.Remove(funcName);
                        Console.WriteLine("function[" + funcName +"] cleared");
                    }
                    else
                        Console.WriteLine("not found");
                    Console.Write(">>>");
                    str = Console.ReadLine();
                }

                string res = Calculator(str);
                if (res != "None")
                    Console.WriteLine(res);
                Console.Write(">>>");
                str = Console.ReadLine();
            }
        }

        static List<string> OPs = new List<string>();
        static Dictionary<string, Function> func_dic = new Dictionary<string, Function>();
        static Dictionary<string, string> var_dic = new Dictionary<string, string>();
        static string Calculator(string Formula)
        {
            string No_Space_Formula = new string(Formula.ToCharArray().Where(c => c != ' ').ToArray());

            // 1. '=' f(x) = x + 1    x = 2
            if (No_Space_Formula.Contains('='))
            {
                if (No_Space_Formula[No_Space_Formula.Length - 1] == ';')
                    No_Space_Formula = No_Space_Formula.Substring(0, No_Space_Formula.Length - 1);
                string[] arr = No_Space_Formula.Split('=');
                if (arr[0].Contains('('))
                {
                    Regex rx = new Regex(@"\b[a-zA-Z_]\w*\(\b");
                    string funcName = rx.Match(arr[0]).Value;
                    if(funcName == string.Empty)
                    {
                        Console.WriteLine("Function name illegal");
                        return "None";
                    }
                    funcName = funcName.Substring(0, funcName.Length - 1);
                    if(func_dic.ContainsKey(funcName))
                        Console.WriteLine("Function name exists");
                    else
                    {
                        Function f = new Function();
                        f.funcName = funcName;
                        f.expr = func_Replace(arr[1]);
                        int leftIndex = arr[0].IndexOf('(');
                        int rightIndex = arr[0].IndexOf(')');
                        f.vars = arr[0].Substring(leftIndex + 1, rightIndex - leftIndex - 1).Split(',').ToList();
                        func_dic.Add(funcName, f);
                    }
                }
                else
                {
                    if (var_dic.ContainsKey(arr[0]))
                        var_dic[arr[0]] = Var_Replace_Calculate(func_Replace(arr[1])).ToString();
                    else
                        var_dic.Add(arr[0], Var_Replace_Calculate(func_Replace(arr[1])).ToString());
                }
                return "None";
            }

            No_Space_Formula = func_Replace(No_Space_Formula);
            return Var_Replace_Calculate(No_Space_Formula);
        }

        static string func_Replace(string No_Space_Formula)
        {
            //No_Space_Formula = "(" + No_Space_Formula + ")";
            foreach (string funcName in func_dic.Keys)
            {
                List<string> varName = func_dic[funcName].vars;
                string content = func_dic[funcName].expr;
                string pattern = @"\b" + funcName + @"\b";
                Regex rx = new Regex(pattern);
                while (rx.IsMatch(No_Space_Formula))
                {
                    var match = rx.Match(No_Space_Formula);
                    int InputLeftIndex = No_Space_Formula.IndexOf('(', match.Index);
                    int firstRightAfterLeft = -1;
                    int leftNO = 0;
                    for (int i = InputLeftIndex + 1; i < No_Space_Formula.Length; i++)
                    {
                        if (No_Space_Formula[i] == '(')
                            leftNO++;
                        else if (No_Space_Formula[i] == ')')
                        {
                            if (leftNO != 0)
                                leftNO--;
                            else
                            {
                                firstRightAfterLeft = i;
                                break;
                            }
                        }
                    }
                    string[] input = No_Space_Formula.Substring(InputLeftIndex + 1, firstRightAfterLeft - InputLeftIndex - 1).Split(',');
                    string the_content = "(" + content + ")";
                    for (int i = 0; i < varName.Count; i++)
                    {
                        Regex the_rx = new Regex(@"\b" + varName[i] + @"\b");
                        the_content = the_rx.Replace(the_content, "(" + input[i] + ")");
                    }
                    the_content = func_Replace(the_content);
                    No_Space_Formula = No_Space_Formula.Substring(0, match.Index) + the_content + No_Space_Formula.Substring(firstRightAfterLeft + 1);
                }
            }

            return No_Space_Formula;
        }

        static string Var_Replace_Calculate(string Formula)
        {
            Regex rx = new Regex(@"\b[a-zA-Z_]\w*\b");
            MatchCollection collection = rx.Matches(Formula);
            foreach (Match match in collection)
            {
                Regex the_rx = new Regex(@"\b" + match.Value + @"\b");
                Formula = the_rx.Replace(Formula, var_dic[match.Value]);
            }
            return Calculate(Formula);
        }

        static string Calculate(string Formula)
        {

            while (Formula.LastIndexOf("(") > -1)
            {
                int lastOpenIndex = Formula.LastIndexOf("(");
                int firstCloseAfterlastOpenIndex = Formula.IndexOf(")", lastOpenIndex);
                string res = ProcessOperation(Formula.Substring(lastOpenIndex + 1, firstCloseAfterlastOpenIndex - lastOpenIndex - 1));

                Formula = Formula.Substring(0, lastOpenIndex) + res + Formula.Substring(firstCloseAfterlastOpenIndex + 1);
            }

            return ProcessOperation(Formula);
        }

        static string ProcessOperation(string operation)
        {
            List<string> lst = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < operation.Length; i++)
            {
                char curr = operation[i];
                if (OPs.Contains(curr.ToString()))
                {
                    if (sb.Length != 0)
                        lst.Add(sb.ToString());
                    lst.Add(curr.ToString());
                    sb.Clear();
                }
                else
                    sb.Append(curr);
            }
            lst.Add(sb.ToString());

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] == "^")
                {
                    generateRes(lst[i], i, lst);
                    i--;
                }
            }

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] == "*" || lst[i] == "/")
                {
                    generateRes(lst[i], i, lst);
                    i--;
                }
            }

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] == "+" || lst[i] == "-")
                {
                    generateRes(lst[i], i, lst);
                    i--;
                }
            }

            return lst[0];
        }

        static void generateRes(string op, int index, List<string> lst)
        {
            double numBeforeOP = 0;
            if (index != 0)
                numBeforeOP = Convert.ToDouble(lst[index - 1]);
            double numAfterOP = 0;
            if (lst[index + 1] == "-")
            {
                lst.RemoveAt(index + 1);
                numAfterOP = -Convert.ToDouble(lst[index + 1]);
            }
            else
                numAfterOP = Convert.ToDouble(lst[index + 1]);

            switch (op)
            {
                case "^":
                    lst[index] = (Math.Pow(numBeforeOP, numAfterOP)).ToString();
                    break;
                case "*":
                    lst[index] = (numBeforeOP * numAfterOP).ToString();
                    break;
                case "/":
                    lst[index] = (numBeforeOP / numAfterOP).ToString();
                    break;
                case "+":
                    lst[index] = (numBeforeOP + numAfterOP).ToString();
                    break;
                case "-":
                    lst[index] = (numBeforeOP - numAfterOP).ToString();
                    break;
            }
            if (index + 1 < lst.Count)
                lst.RemoveAt(index + 1);
            if (index - 1 >= 0)
                lst.RemoveAt(index - 1);
        }

    }

    class Function
    {
        public string funcName = "";
        public string expr = "";
        public List<string> vars = new List<string>();
    }
}
