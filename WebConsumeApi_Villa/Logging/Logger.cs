using System;

namespace WebConsumeApi_Villa.Logging
{
    public class Logger : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.WriteLine("ERORR - " + message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        public void LogWithColor(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ERORR - " + message);
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
             if (type == "warning")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERORR - " + message);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
