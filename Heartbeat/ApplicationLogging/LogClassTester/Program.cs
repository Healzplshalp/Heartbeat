using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogging;

namespace LogClassTester
{
    class Program
    {
        static void Main(string[] args)
      {
            ApplicationLogging newLog = new ApplicationLogging();
            newLog.LogInit(@"C:\Data\TestAppLogging", @"KenSucks", ".log", true, true);

            newLog.Info("Test Info Ken Sucks");
            newLog.Error("Test Error Ken Sucks");
            newLog.Runtime("Test Runtime Ken Sucks");
            newLog.DataList("Test, Test, Test, Test");
            newLog.Close();
        }
    }
}
