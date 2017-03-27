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
    public class CommandArgumentOptionsTests
    {
        [TestMethod()]
        public void CommandArgumentOptions_GetUsageTest_IsNotNull()
        {
            // Arrange
            string expectedres = "";
            var cao = new CommandArgumentOptions();
            // Act          
            var results = cao.GetUsage();
            // Assert  
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void CommandArgumentOptions_WhenNoArguementsGetUsageTest_hasData()
        {
            // Arrange
            var cao = new CommandArgumentOptions();
            // Act          
            var results = cao.GetUsage();
            // Assert  
            Assert.AreEqual(results.Length >0, true);
        }

        [TestMethod()]
        public void CommandArgumentOptions_WhenNoArgumentsGetUsage_hasVersion()
        {
            // Arrange
            var cao = new CommandArgumentOptions();
            // Act          
            var results = cao.GetUsage();
            // Assert  
            Assert.AreEqual(results.Contains("Version"), true);
        }

      

    }
}