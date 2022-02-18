using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    [TestFixture]
    public class LightManagerTests
    {
        /// <summary>
        /// L3R2
        ///
        /// Test that GetStatus returns the correct output.
        /// </summary>
        [Test]
        public void WhenGetStatus_IsCalled_ReturnLightStatuses()
        {
            #region Arrange

            string expectedStatuses = "Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";

            #endregion
            
            #region Act & Assert

            LightManager lightManager = new LightManager();
            Assert.AreEqual(expectedStatuses, lightManager.GetStatus());

            #endregion
        }
    }
}