using System;
using System.Reflection;

namespace Heartbeat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Heartbeat Pulse";
            HeartbeatBeat HB = new HeartbeatBeat();
            Console.WriteLine("Send heartbeat...");
            HB.Run();
            Console.WriteLine("Hearbeat sent...");
        }

        /// <summary>
        /// This method should be called everytime an exception is thrown
        /// from a catch block. If it's not, the line number of an exception 
        /// thrown from inside the matching try block is lost.
        /// </summary>
        /// <param name="exception">Exception</param>
        public static void PreserveStackTrace(Exception exception)
        {
            MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
            BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(exception, null);
        }
    }
}
