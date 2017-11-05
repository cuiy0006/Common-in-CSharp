using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using Utility;

namespace Demo
{
    public class MultiThreads
    {
        public static void ShowMultiThread()
        {
            //ThreadStartMethod();
            //ThreadPoolMethod();

            //ShowTask();
            //ShowTaskEXCEPTION();
            //ShowTaskCANCEL();

            //ShowTaskHighDelaySync();

            ///

            //ShowTaskHighDelayAsync1();

            //ShowTaskHighDelayAsync2();

            //ShowTaskHighDelayAsync3();

            //ShowTaskHighDelayAsync4();

            ///
            //ParallalFor();

            //ParallalForeach("D:\\Application", "*.exe");

            ///
            unsafe
            {
                byte[] redpill = 
                {
                    0x0f,0x01,0x0d,0x00,0x00,0x00,0x00,0xc3
                };

                fixed (byte* matrix = new byte[6], redpillptr = redpill)
                {
                    *(uint*)&redpillptr[3] = (uint)&matrix[0];
                }
            }
        }


        #region Traditional
        private const int Repetitions = 1000;
        private static void ThreadStartMethod()
        {
            //ThreadStart threadstart = DoWork;
            Thread thread = new Thread((ThreadStart)DoWork);
            thread.Start();
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("-");
            }

            thread.Join();

            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("*");
            }

        }


        private static void ThreadPoolMethod()
        {
            ThreadPool.QueueUserWorkItem(DoWork, "*");
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("-");
            }
        }

        private static void DoWork()
        {
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("+");
            }
        }

        private static void DoWork(object obj)
        {
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write(obj);
            }
        }

        #endregion

        #region Task

        private static void ShowTask()
        {

            //Return nothing
            Task task = Task.Run(() => DoWork("+"));

            // Return value(string)
            Task<string> task1 = Task.Run<string>(() => DoWork0("-"));

            Task<string> task2 = new Task<string>(obj=>DoWork0(obj), "&");

            Task<string> task3 = task1.ContinueWith<string>(FormerTask => ContinueWork(FormerTask),TaskContinuationOptions.NotOnCanceled);
            task2.Start();
            
            bool flag = true;
            while (true)
            {

                if (task1.IsCompleted && flag)
                {
                    flag = false;
                    Console.WriteLine(Environment.NewLine + "Task1 : " + task1.Result + Environment.NewLine);

                    //break;
                }
                //if (task2.IsCompleted)
                //{
                //    Console.WriteLine(Environment.NewLine + "Task2 :" + task2.Result + Environment.NewLine);
                //    break;
                //}

                if (task3.IsCompleted)
                {
                    Console.WriteLine(Environment.NewLine + task3.Result+ Environment.NewLine);
                    break;
                }
                Console.Write("*");
            }

        }

        private static async void ShowTaskCANCEL()
        {            
            
            string stars = "*".PadRight(Console.WindowWidth - 1, '*');

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task task = Task.Run(() => PrintStar(cancellationTokenSource.Token), cancellationTokenSource.Token);
            Console.ReadKey();
            cancellationTokenSource.Cancel();

            Console.WriteLine(stars);
            
        }

        private static void ShowTaskEXCEPTION()
        {
            Task task = new Task(() => {
                    throw new InvalidOperationException(); 
            
            });

            ///doesn't work
            //try
            //{
            //    task.Wait();
            //}
            //catch (AggregateException ex)
            //{ 
            
            //}

            ///Doesn't work
            //bool IsParentTaskFault = false;
            //Task ContinueTask = task.ContinueWith(FormerTask =>
            //    {
            //        IsParentTaskFault = FormerTask.IsFaulted;

            //    }, TaskContinuationOptions.OnlyOnFaulted);

            //task.Start();

            //if (!task.IsFaulted)
            //{
            //    task.Wait();
            //}
            //else
            //{
            //    task.Exception.Handle(exception =>
            //        {
            //            Console.WriteLine(exception.Message);
            //            return true;
            //        });
            //}

        }

        private static void ShowTaskHighDelaySync()
        {
            //string url = "http://www.pandatv.com";
            string url = "http://www.IntelliTect.com";
            Console.Write(url);
            WebRequest webRequest = WebRequest.Create(url);

            WebResponse webResponse = webRequest.GetResponse();
            Console.Write("......");

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine(FormatBytes.FormatBytesMethod(text.Length));
            }
        }
        /// <summary>
        /// asyncMethod
        /// </summary>
        private static void ShowTaskHighDelayAsync1()
        {
            string url = "http://www.IntelliTect.com";
            Console.Write(url);

            StreamReader reader = null;
            WebRequest webRequest = WebRequest.Create(url);
            Task task = webRequest.GetResponseAsync()
                .ContinueWith(FormerTask =>
                {
                    WebResponse webResponse = FormerTask.Result;
                    reader = new StreamReader(webResponse.GetResponseStream());
                    return reader.ReadToEndAsync();
                }).Unwrap()
                .ContinueWith(FormerTask =>
                {
                    if (reader != null) reader.Dispose();
                    string text = FormerTask.Result;
                    Console.Write(FormatBytes.FormatBytesMethod(text.Length));
                });
            try
            {
                while (!task.Wait(100))
                {
                    Console.Write(".");
                }

            }
            catch (AggregateException exception)
            {

            }
        }

        /// <summary>
        /// Task.run()  Factory.StartNew()->TaskCreationOptions.LongRunning
        /// </summary>
        private static void ShowTaskHighDelayAsync2()
        {
            string url = "http://www.IntelliTect.com";
            Console.Write(url);

            StreamReader reader = null;
            WebRequest webRequest = WebRequest.Create(url);

            //Task task = Task.Run(() =>
            //{
            //    WebResponse webResponse = webRequest.GetResponse();
            //    return webResponse;
            //}).ContinueWith(FormerTask =>
            //{
            //    reader = new StreamReader(FormerTask.Result.GetResponseStream());
            //    string text = reader.ReadToEnd();
            //    return text;
            //}).ContinueWith(FormerTask =>
            //{
            //    Console.Write(FormatBytes.FormatBytesMethod(FormerTask.Result.Length));
            //});


            //Task task = Task.Run(() =>
            //{
            //    WebResponse webResponse = webRequest.GetResponse();

            //    reader = new StreamReader(webResponse.GetResponseStream());
            //    string text = reader.ReadToEnd();

            //    Console.Write(FormatBytes.FormatBytesMethod(text.Length));
            //});

            /////////////////////// LongRunning ///////////////////////
            Task task = Task.Factory.StartNew(() =>
            {
                WebResponse webResponse = webRequest.GetResponse();

                reader = new StreamReader(webResponse.GetResponseStream());
                string text = reader.ReadToEnd();

                Console.Write(FormatBytes.FormatBytesMethod(text.Length));
            }, TaskCreationOptions.LongRunning);



            try
            {

                /// Task Complete Event
                //task.GetAwaiter().OnCompleted(() =>
                //    {
                //        Console.WriteLine("Complete!");
                //    });


                while (!task.Wait(100))
                {
                    Console.Write(".");
                }

            }
            catch (AggregateException exception)
            {

            }
        }

        /// <summary>
        /// async-await
        /// </summary>
        private static void ShowTaskHighDelayAsync3()
        {
            string url = "http://www.IntelliTect.com";
            Console.Write(url);

            Task task = WriteWebRequestSizeAsync(url);

            while (!task.Wait(100))
            {
                Console.Write(".");
            }
        }


        private static async Task WriteWebRequestSizeAsync(string url)
        {

            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = await webRequest.GetResponseAsync(); //(self) block -> (callstack thread) go on

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                string text = await reader.ReadToEndAsync();  //(self) block -> (callstack thread) go on
                Console.Write(FormatBytes.FormatBytesMethod(text.Length));

            }

        }
        /// <summary>
        /// async-lambda
        /// </summary>
        private static void ShowTaskHighDelayAsync4()
        {
            string url = "http://www.IntelliTect.com";
            Console.Write(url);


            Func<string, Task> WriteWebRequestSizeAsync1 = async (WebUrl) =>
                {
                    WebRequest webRequest = WebRequest.Create(WebUrl);
                    WebResponse webResponse = await webRequest.GetResponseAsync();
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string text = await reader.ReadToEndAsync();
                        Console.WriteLine(FormatBytes.FormatBytesMethod(text.Length));
                    }
                };

           Task task = WriteWebRequestSizeAsync1(url);

           while (!task.Wait(100))
           {
               Console.Write(".");
           }
        }


        /// <summary>
        /// Parallal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static void ParallalFor()
        {
            int interations = 100000;
            string[] Section = new string[interations];
            Parallel.For(0, interations, i =>
                {
                    Section[i] = (i * 10 + i * 10 / 2).ToString();
                });

            string result = string.Join(" ", Section);
            Console.WriteLine(result);
        }

        private static void ParallalForeach(string directoryPath, string SearchPattern)
        {
            IEnumerable<string> files = Directory.GetFiles(directoryPath, SearchPattern, SearchOption.AllDirectories);

            Parallel.ForEach(files, fileName =>
                {
                    //DoSomething(fileName);
                });
        }

        private static string DoWork0(object obj)
        {
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write(obj);
            }
            return "Done";
        }

        private static string ContinueWork(object obj)
        {
            Task<string> task = obj as Task<string>;

            Console.WriteLine(Environment.NewLine + "Task3 show Task1 result " + task.Result + Environment.NewLine);

            return "Continue Done";
        }

        private static void PrintStar(CancellationToken Token)
        {
            int i =0;
            while (i <= int.MaxValue && !Token.IsCancellationRequested)
            {
                Console.Write(i++);

            }


        }
        #endregion
    }


    //
}
