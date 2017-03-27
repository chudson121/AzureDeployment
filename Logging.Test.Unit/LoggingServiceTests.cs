using Logging.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Test.Unit
{
    [TestClass]
    public class LoggingServiceTests
    {
        private readonly ILogger _logger = new LoggingService();

        [TestMethod]
        public void LoggingServiceWhenInitializedIsNotNull()
        {
            Assert.IsNotNull(_logger, "LoggingService is null");
        }
        
        [TestMethod]
        public void LoggerName_CallingAssemblyName()
        {
            // Arrange
            var service = new LoggingServiceSpy();

            // Act
            service.Info("Test");

            // Assert 
            Assert.AreEqual("Logging.Test.Unit", service.LastLoggerNameUsed);
        }

        [TestMethod]
        public void LoggerName_CallingAssemblyNameWithStaticMethod()
        {
            Assert.AreEqual("Logging.Test.Unit", CallingAssemblyNameWithStaticMethod());
        }

        private static string CallingAssemblyNameWithStaticMethod()
        {
            // Arrange
            var service = new LoggingServiceSpy();

            // Act
            service.Info("messageouput");
            return service.LastLoggerNameUsed;

        }

        private class LoggingServiceSpy : LoggingService
        {
            public string LastLoggerNameUsed { get; private set; }

            public override void Info(string message)
            {
                LastLoggerNameUsed = Name;
                base.Info(message);
            }
        }
    }
}