using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AppLogging
{
    /// <summary>
    /// This class is the public facing class that is instantiated by the calling program.
    /// </summary>
    public class ApplicationLogging
    {
        private StreamWriter LogWriter = null;
        private string LogPath;
        private string LogName;
        private string LogExtension;
        private Boolean IsDetailedLogging;
        private Boolean IsLoggingOn;

        /// <summary>
        /// The method must be called first to initialize the log class and give it its parameters
        /// </summary>
        /// <param name="logPath">Location of the log file, this is a directory</param>
        /// <param name="logName">Name of the log file, this cannot contain illegal characters</param>
        /// <param name="logExtension">Log type</param>
        /// <param name="isDetailedLogging">If false: only Error(), StartLog(), and Close() record 
        /// to log. If true: Info(), DataList(), and RunTime() also record to log.</param>
        /// <param name="isLoggingOn">If true: logs will be created.</param>
        public void LogInit(string logPath,
               string logName,
               string logExtension,
               Boolean isDetailedLogging,
               Boolean isLoggingOn)
        {
            try
            {
                LogPath = logPath;
                LogName = logName;
                LogExtension = logExtension;
                IsDetailedLogging = isDetailedLogging;
                IsLoggingOn = isLoggingOn;

                ValidateArguments();

                if (isLoggingOn)
                    StartLog();
            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0000: Error encountered during log initialization " +
                                    Ex.Message.ToString());
            }
        }

        private void ValidateArguments()
        {
            if (!Directory.Exists(LogPath))
                throw new Exception("LOG0000: Log folder directory is unreachable");
            if (!IsValidFileName(LogName))
                throw new Exception("LOG0000: Log name contains illegal characters" +
                                "that cannot be used for a file name");
        }

        private Boolean IsValidFileName(string logName)
        {
            Regex regExp = new Regex("[" + Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars())) + "]");
            if (regExp.IsMatch(logName))
                return false;
            return true;
        }

        private void StartLog()
        {
            string LogFullPath;

            try
            {
                LogFullPath = CreateLogFullPath();
                LogWriter = File.AppendText(LogFullPath);
            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0005: Error encountered while opening log event " +
                    Ex.Message.ToString());
            }
        }

        private string CreateLogFullPath()
        {
            String timeStamp;
            String LogFullPath;

            timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            LogFullPath = string.Format(@"{0}\{1}_{2}{3}", LogPath,
                                                            LogName,
                                                            timeStamp,
                                                            LogExtension);
            return LogFullPath;
        }

        /// <summary>
        /// This method writes a datalist type message into a text file
        /// </summary>
        /// <param name="message">Comma delimited string</param>
        public void DataList(string message)
        {
            try
            {
                if (IsLoggingOn && IsDetailedLogging)
                {
                    LogWriter.WriteLine(message);
                    LogWriter.Flush();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0032: Error encountered during datalist log write " +
                                    Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Create an INFO type log message
        /// </summary>
        /// <param name="message">String content</param>
        public void Info(string message)
        {
            try
            {
                if (IsLoggingOn && IsDetailedLogging)
                {
                    LogWriter.WriteLine("INFO  " +
                                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                      " - " +
                                      message);
                    LogWriter.Flush();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0001: Error encountered during log writing INFO " +
                    Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Write error entry into the log file
        /// </summary>
        /// <param name="message">string message</param>
        public void Error(string message)
        {
            try
            {
                if (IsLoggingOn)
                {
                    LogWriter.WriteLine("ERROR  " +
                                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                      " - " +
                                      message);
                    LogWriter.Flush();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0002: Error encountered during log writing ERROR " +
                    Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Write a run type message into the log file. 
        /// </summary>
        /// <param name="message">string message</param>
        public void Runtime(string message)
        {
            try
            {
                if (IsLoggingOn)
                {
                    LogWriter.WriteLine("RUN  " +
                                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                      " - " +
                                      message);
                    LogWriter.Flush();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0003: Error encountered during log writing RUN " +
                    Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Close the log file
        /// </summary>
        public void Close()
        {
            try
            {
                if (IsLoggingOn)
                {
                    LogWriter.WriteLine("RUN  " +
                                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                      " - " +
                                      "Log is Closed");
                    LogWriter.Flush();
                }

            }
            catch (Exception Ex)
            {
                throw new Exception("LOG0004: Error encountered during closing log event " +
                    Ex.Message.ToString());
            }
            finally
            {
                if (LogWriter != null && LogWriter.BaseStream != null)
                    LogWriter.Close();
            }
        }

    }
}
