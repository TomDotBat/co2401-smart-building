using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    [TestFixture]
    public class FireAlarmManagerTests
    {
        /// <summary>
        /// L3R2
        ///
        /// Test that GetStatus returns the correct output.
        /// </summary>
        [Test]
        public void WhenGetStatus_IsCalled_ReturnFireAlarmStatuses()
        {
            #region Arrange

            string expectedStatuses = "FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";

            #endregion
            
            #region Act & Assert

            FireAlarmManager fireAlarmManager = new FireAlarmManager();
            Assert.AreEqual(expectedStatuses, fireAlarmManager.GetStatus());

            #endregion
        }
    }
}