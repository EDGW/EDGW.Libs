using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDGW.Data.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.Data.Logging.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod()]
        public void LoggerTest()
        {
            Logger lger = new("DEBUG");
            lger.Info("this is an info.");
            lger.Warn("this is a  warn.");
            lger.Error("this is an error."); 
            lger.Fatal("this is a  fatal.");
            lger.Fatal("test variables & exceptions", new NullReferenceException(), ("aaa", "this is a string"), ("ALIGNMENT TEST", "string"), ("OBJECT TST", new object()));
            
        }

    }
}