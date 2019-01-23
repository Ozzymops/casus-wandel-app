using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WandelApp.Models
{
    public class Logger
    {
        public void WriteToLog(string entry)
        {
            string logString = "Loggy Boi! [" + DateTime.Now.TimeOfDay.ToString() + "] - " + entry;
            Debug.WriteLine(logString);
            // Write to actual log file
        }
    }
}
