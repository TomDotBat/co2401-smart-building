using System;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    /// <summary>
    /// Unit tests for the Manager class.
    /// </summary>
    [TestFixture]
    public class ManagerTests
    {
        #region Tests for Constructor(deviceType)

        /// <summary>
        /// The constructor should throw an ArgumentException when the given
        /// device type is an empty string.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenEmptyDeviceType_ThrowsArgumentException()
        {
            #region Arrange

            // Giving an empty ID string will cause an ArgumentException.
            string deviceType = "";

            #endregion

            #region Act & Assert

            // Assert that the constructor throws an ArgumentException when given
            // an empty string.
            // Because we're using a substitute class we need to retrieve the
            // ArgumentException from a TargetInvocationException.
            TargetInvocationException exception = Assert.Throws<TargetInvocationException>(() =>
            {
                Substitute.For<Manager>(deviceType);
            });
            
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(typeof(ArgumentException), exception.InnerException.GetType());

            #endregion
        }

        /// <summary>
        /// The constructor should throw an ArgumentNullException when the
        /// given device type is null.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenNullDeviceType_ThrowsArgumentNullException()
        {
            #region Arrange

            // Giving a null ID will cause an ArgumentNullException.
            string deviceType = null;

            #endregion

            #region Act & Assert
            
            // Assert that the constructor throws an ArgumentNullException
            // when given null.
            // Because we're using a substitute class we need to retrieve the
            // ArgumentNullException from a TargetInvocationException.
            TargetInvocationException exception = Assert.Throws<TargetInvocationException>(() =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                Substitute.For<Manager>(deviceType);
            });
            
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception.InnerException);
            Assert.AreEqual(typeof(ArgumentNullException), exception.InnerException.GetType());

            #endregion
        }
        
        #endregion

        #region Tests for SetEngineerRequired(needsEngineer)

        /// <summary>
        /// SetEngineerRequired() should set the value of engineerRequired
        /// to the given engineerRequired state.
        /// </summary>
        [Test]
        public void SetEngineerRequired_WhenGivenEngineerRequiredState_SetsEngineerRequired()
        {
            #region Arrange

            // Create a device manager with the device type "TestDevice".
            string deviceType = "TestDevice";
            Manager manager = Substitute.For<Manager>(deviceType);

            #endregion

            #region Act

            // Set the engineerRequired state to true as the default is false.
            manager.SetEngineerRequired(true);
            bool actualState = manager.GetEngineerRequired();

            #endregion
            
            #region Assert
            
            // Assert that the engineerRequired state is true.
            Assert.IsTrue(actualState);
            
            #endregion
        }

        #endregion
        
        #region Tests for GetStatus()
        
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
            Manager manager = Substitute.For<Manager>(deviceType);
            
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
        
        #endregion

        #region Tests for RegisterDevice(device)

        /// <summary>
        /// RegisterDevice() should throw an ArgumentNullException when the
        /// given device null.
        /// </summary>
        [Test]
        public void RegisterDevice_WhenGivenNull_ThrowsArgumentNullException()
        {
            #region Arrange

            // Giving a null device will cause an ArgumentNullException.
            IDevice device = null;
            
            // Create a device manager with the device type "TestDevice".
            string deviceType = "TestDevice";
            Manager manager = Substitute.For<Manager>(deviceType);

            #endregion

            #region Act & Assert

            // Assert RegisterDevice() throws an ArgumentNullException
            // when given null.
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                manager.RegisterDevice(device);
            });

            #endregion
        }
        

        #endregion
    }
}