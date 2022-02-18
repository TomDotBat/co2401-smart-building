using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    [TestFixture]
    public class DoorManagerTests
    {
        /// <summary>
        /// L3R2
        ///
        /// Test that GetStatus returns the correct output.
        /// </summary>
        [Test]
        public void WhenGetStatus_IsCalled_ReturnDoorStatuses()
        {
            #region Arrange

            string expectedStatuses = "Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";

            #endregion
            
            #region Act & Assert

            DoorManager doorManager = new DoorManager();
            Assert.AreEqual(expectedStatuses, doorManager.GetStatus());

            #endregion
        }
    }
}