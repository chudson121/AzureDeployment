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
    public class LoggingServiceTests
    {
        private static ILogger logger = LoggingService.GetLoggingService();

       
        [TestMethod()]
        public void GetLoggingServiceTest()
        {
            Assert.IsNotNull(logger, "LoggingService is null");
        }

    }
}