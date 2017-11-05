using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace Utility
{
    public class TimeCount
    {
        [DllImport("Kernel32.dll")]

        private static extern bool QueryPerformanceCounter(

            out long lpPerformanceCount);



        [DllImport("Kernel32.dll")]

        private static extern bool QueryPerformanceFrequency(

            out long lpFrequency);

        private long startTime, stopTime;

        private long freq;

        private bool IsStarted = false;

        public TimeCount()

        {

            if (QueryPerformanceFrequency(out freq) == false)

            {

                // 不支持高性能计数器

                throw new Win32Exception();

            }

        }

        // 开始计时器

        public void Start()
        {

            // 来让等待线程工作

            Thread.Sleep(0);

            IsStarted = true;

            QueryPerformanceCounter(out startTime);

        }



        // 停止计时器

        public double Stop()
        {

            if (IsStarted)
            {
                QueryPerformanceCounter(out stopTime);
                double Duration = (double)(stopTime - startTime) / (double)freq;

                startTime = 0;
                stopTime = 0;

                IsStarted = false;
                return Duration;
            }
            else
            {
                return -1;
            }

        }



        // 返回计时器经过时间(单位：秒)

        //private double Duration
        //{

        //    get
        //    {

        //        return (double)(stopTime - startTime) / (double)freq;

        //    }

        //}

    }
}
