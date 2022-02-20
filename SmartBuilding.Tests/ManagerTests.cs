using NSubstitute;
using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        /// <summary>
        /// Test for requirement L3R2.
        ///
        /// GetStatus() should return a list of comma seperated values, the
        /// first value being the type of device being managed, the rest
        /// being the states of the managed devices.
        /// </summary>
        [Test]
        public void GetStatus_WhenCalled_ReturnsManagerStatus()
        {
            #region Arrange
            
            // Create a device manager with the device type "TestDevice".
            string deviceType = "TestDevice";
            Manager<IDevice> manager = Substitute.For<Manager<IDevice>>(deviceType);
            
            // Register 3 devices, the first and last being normal, the
            // middle being a faulty device.
            IDevice normalDevice = Substitute.For<IDevice>();
            normalDevice.GetState().Returns("OK");
            
            IDevice faultyDevice = Substitute.For<IDevice>();
            faultyDevice.GetState().Returns("FAULT");
            
            manager.RegisterDevice(normalDevice);
            manager.RegisterDevice(faultyDevice);
            manager.RegisterDevice(normalDevice);

            // We can expect the following string to be returned.
            string expectedStatus = "TestDevice,OK,FAULT,OK,";
            
            #endregion
            
            #region Act

            // Get the status string from the device manager.
            string actualStatus = manager.GetStatus();

            #endregion

            #region Assert

            // Ensure all devices were asked for their state.
            normalDevice.Received().GetState();
            faultyDevice.Received().GetState();
            
            // Assert that the status string we got is what we expected.
            Assert.AreEqual(expectedStatus, actualStatus);

            #endregion
        }
    }
}