using System;
using System.Collections.Generic;
using System.Text;

namespace MinecartSharp.Utils
{
    class Logger
    {
        public static bool Debug = false;

        public static void Log(LogType type, string message)
        {
            if (type == LogType.Debug && Debug == false)
                return;

            Console.Write(DateTime.Now.ToString("HH:mm:ss") + " [");

            switch (type)
            {
                case LogType.Info:
                    Console.Write("INFO");
                    break;
                case LogType.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("WARN");
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERROR");
                    break;
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("DEBUG");
                    break;
            }

            Console.ResetColor();

            Console.Write("] ");
            Console.WriteLine(message);
        }
    }

    internal enum LogType
    {
        Info,
        Warn,
        Error,
        Debug
    }
}
