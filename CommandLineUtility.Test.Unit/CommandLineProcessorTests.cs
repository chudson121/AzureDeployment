using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logging.Contracts;

namespace CommandLineUtility.Test.Unit
{
    [TestClass()]
    public class CommandLineProcessorTests
    {
        readonly MockLogger _logger =  new MockLogger();
        
        [TestMethod]
        public void CommandLineProcessor_nullArgs_DisplayUsage()
        {
            // Arrange
            ArgumentParser ap = new ArgumentParser();
            CommandLineProcessor p = new CommandLineProcessor(_logger,ap,null );

            // Act   
            var results = p.Options.GetUsage();

            // Assert  
            Assert.IsNotNull(results, "Command Line null error");

        }



        [TestMethod]
        public void CommandLineProcessor_IsThereAnActionWhenCommandLineIsSet_ActionLoadedintoProperty()
        {
            // Arrange
            string[] args = new string[] { "-a listsites" };
            ArgumentParser ap = new ArgumentParser();
            // Act   
            CommandLineProcessor p = new CommandLineProcessor(_logger, ap, args);

            var results = p.Options.Action;
            // Assert  
            Assert.AreEqual(results.Length > 0, true);
            Assert.AreEqual(results, "listsites");
        }



        [TestMethod]
        public void CommandLineProcessor_IsThereASiteWhenCommandLineIsSet_SiteLoadedintoProperty()
        {
            // Arrange
            string[] args = new string[] { "-s testsite" };
            ArgumentParser ap = new ArgumentParser();

            // Act   
            CommandLineProcessor p = new CommandLineProcessor(_logger, ap, args);

            var results = p.Options.SiteName;
            // Assert  
            Assert.AreEqual(results.Length > 0, true);
            Assert.AreEqual(results, "testsite");

        }



    }



    public class MockLogger : ILogger
    {
        public readonly List<string> Outputs = new List<string>();

        public string Name { get; }

        public void Info(string message)
        {
            Outputs.Add(message);
        }

        public void Error(string message)
        {
            Outputs.Add(message);
        }

        public void Error(string message, Exception ex)
        {
            Outputs.Add(message);
        }
    }

}