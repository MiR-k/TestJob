using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestJob.core.helpers
{
    public static class Logger
    {
        public static void Info(string message)
        {
            string timeString = DateTime.Now.ToLongTimeString();
            Console.WriteLine(String.Concat(timeString, " - ", message));
        }

        public static void Step(int step)
        {
            string strBuffer = DateTime.Now.ToLongTimeString() + " - Шаг:" + step;
            Console.WriteLine(strBuffer);
        }

        public static void Step(int stepOf, int stepTo)
        {
            string strBuffer = DateTime.Now.ToLongTimeString() + " - Шаги: " + stepOf + "-" + stepTo;
            Console.WriteLine(strBuffer);
        }
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Info(message);
            Console.ResetColor();
        }
    }
}
