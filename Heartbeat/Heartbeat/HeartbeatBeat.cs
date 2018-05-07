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
                GmailEmail.SendEmail();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
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
            Log.Runtime("Start Log");
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
                Program.PreserveStackTrace(ex);
                throw;
            }
        }
    }
}
