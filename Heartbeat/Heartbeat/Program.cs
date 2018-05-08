using System;
using System.Reflection;

namespace Heartbeat
{
    class Program
    {
        /// <summary>
        /// Exit code for batch job
        /// 1: Successful
        /// 2: Failure during runtime (generic)
        /// </summary>
        enum ExitCode : int
        {
            Success = 0,
            AppSettingsRetrievalError = 99,
            EmailNotificationError = 98,
            Failure = 1
        }

        static int Main(string[] args)
        {
            try
            {
                Console.Title = "Heartbeat Pulse";
                HeartbeatBeat HB = new HeartbeatBeat();
                Console.WriteLine("Send heartbeat...");
                HB.Run();
                Console.WriteLine("Hearbeat sent...");
                Console.WriteLine("Exit Code: " + (int)ExitCode.Success);
                return (int)ExitCode.Success;
            }

            catch (Exception e)
            {
                Console.WriteLine("Problem encountered during runtime call to Informatica.");
                Console.WriteLine(e.ToString());

                switch (Environment.ExitCode)
                {
                    case 55:
                        Console.WriteLine("Exit Code: " + (int)ExitCode.EmailNotificationError);
                        return (int)ExitCode.EmailNotificationError;
                    case 65:
                        Console.WriteLine("Exit Code: " + (int)ExitCode.AppSettingsRetrievalError);
                        return (int)ExitCode.AppSettingsRetrievalError;
                    default:
                        Console.WriteLine("Exit Code: " + (int)ExitCode.Failure);
                        return (int)ExitCode.Failure;
                }
            }
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
