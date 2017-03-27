using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureDeployment.Test.Unit
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod]
        public void Main_Compile()
        {
            // Arrange
            var p = new Program();

            // Act          


            // Assert          
            Assert.IsNotNull(p);

        }

    

    }
}