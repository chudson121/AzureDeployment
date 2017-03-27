using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureDeployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeployment.Tests
{
    [TestClass()]
    public class CommandLineProcessorTests
    {
        private static ILogger logger = LoggingService.GetLoggingService();
        [TestMethod]
        public void CommandLineProcessor_nullArgs_DisplayUsage()
        {
            // Arrange
            CommandLineProcessor p = new CommandLineProcessor(null, logger );

            // Act   
            var results = p.Options;

            // Assert  
            Assert.IsNotNull(results, "Command Line null error");

        }



        [TestMethod]
        public void CommandLineProcessor_IsThereAnActionWhenCommandLineIsSet_ActionLoadedintoProperty()
        {
            // Arrange
            string[] args = new string[] { "-a listsites" };

            // Act   
            CommandLineProcessor p = new CommandLineProcessor(args, logger);

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

            // Act   
            CommandLineProcessor p = new CommandLineProcessor(args, logger);

            var results = p.Options.SiteName;
            // Assert  
            Assert.AreEqual(results.Length > 0, true);
            Assert.AreEqual(results, "testsite");

        }



    }
}