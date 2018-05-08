using AppLogging;
using System;

namespace Heartbeat
{
    class HeartbeatBeat
    {
        ApplicationLogging Log = new ApplicationLogging();

        public void Run()
        {
            try
            {
                InitializeProgram();
                Log.Info("Intializations complete...");
                Log.Info("Sending heartbeat");
                GmailEmail.SendEmail();
                Log.Info("Heartbeat sent: " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Log.Error("Return code: " + Environment.ExitCode);
                Program.PreserveStackTrace(ex);
                throw;
            }
            finally
            {
                if (Log != null)
                    Log.Close();
            }
        }

        /// <summary>
        /// Initialize te program by getting all of the settings and starting the log file
        /// </summary>
        private void InitializeProgram()
        {
            InitializeLog();
            Log.Runtime("Start Log: Session: " + DateTime.Now.ToString());
        }

        public void InitializeLog()
        {
            try
            {
                Log.LogInit(@"C:\DATA\TestHeart",
                            "Heartbeat",
                            ".log",
                            true,
                            true);
                Log.Runtime("Heartbeat - Version: " +
                    System.Reflection.Assembly
                    .GetExecutingAssembly().GetName().Version.ToString());
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 65;
                Program.PreserveStackTrace(ex);
                throw;
            }
        }
    }
}
