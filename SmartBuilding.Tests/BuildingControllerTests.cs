﻿using System;
using NSubstitute;
using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    /// <summary>
    /// Unit tests for the BuildingController class.
    /// </summary>
    [TestFixture]
    public class BuildingControllerTests
    {
        private ILightManager _lightManager;
        private IDoorManager _doorManager;
        private IFireAlarmManager _fireAlarmManager;
        private IWebService _webService;
        private IEmailService _emailService;
        
        /// <summary>
        /// Create substitutes of the dependencies for BuildingController.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _lightManager = Substitute.For<ILightManager>();
            _doorManager = Substitute.For<IDoorManager>();
            _fireAlarmManager = Substitute.For<IFireAlarmManager>();
            _webService = Substitute.For<IWebService>();
            _emailService = Substitute.For<IEmailService>();
        }
        
        #region Tests for Constructor(id)
        
        /// <summary>
        /// Test for requirements L1R1 and L1R2.
        ///
        /// Constructing a BuildingController with an ID should set buildingID.
        /// GetBuildingID() should return the value of buildingID.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenID_SetsBuildingID()
        {
            #region Arrange

            // If we construct a BuildingController with an ID we can expect
            // it to be the same when we get it with GetBuildingID().
            string testId = "building-100";
            string expectedId = testId;
            
            #endregion

            #region Act
            
            // Construct a BuildingController with our test ID.
            BuildingController buildingController = new BuildingController(testId);
            string actualId = buildingController.GetBuildingID();

            #endregion

            #region Assert

            // Assert that the ID retrievable by GetBuildingID() is what we expected.
            Assert.AreEqual(expectedId, actualId);

            #endregion
        }

        /// <summary>
        /// Test for requirement L1R3.
        ///
        /// The ID given when constructing a BuildingController should be
        /// converted to lower case.
        /// </summary>
        [TestCase("BUILDING-100", ExpectedResult = "building-100")]
        [TestCase("bUiLdInG-100", ExpectedResult = "building-100")]
        [TestCase("12345", ExpectedResult = "12345")]
        [TestCase("BUILDING", ExpectedResult = "building")]
        [TestCase("building", ExpectedResult = "building")]
        public string Constructor_WhenGivenID_ConvertsCaseToLower(string testId)
        {
            #region Act
            
            // Construct a BuildingController with our test ID and get
            // the converted value with GetBuildingID().
            BuildingController buildingController = new BuildingController(testId);
            string actualId = buildingController.GetBuildingID();
            
            #endregion
            
            #region Assert

            // NUnit will check that if the result is what we expected.
            return actualId;

            #endregion
        }
        
        /// <summary>
        /// Test for requirement L1R5 and L1R6.
        ///
        /// The initial value of currentState should be set to "out of hours".
        /// GetCurrentState() should return the value of currentState.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenID_SetsInitialCurrentState()
        {
            #region Arrange

            // If we construct a BuildingController with an ID we can expect
            // that the initial currentState will be "out of hours".
            string testId = "building-100";
            string expectedState = "out of hours";

            #endregion
            
            #region Act

            // Construct a BuildingController with our test ID and get
            // the initial currentState with GetCurrentState().
            BuildingController buildingController = new BuildingController(testId);
            string actualState = buildingController.GetCurrentState();

            #endregion
            
            #region Assert
            
            // Assert that the initial value of currentState is what we expected.
            Assert.AreEqual(expectedState, actualState);
            
            #endregion
        }

        /// <summary>
        /// The constructor should throw an ArgumentException when the given
        /// ID is an empty string.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenEmptyID_ThrowsArgumentException()
        {
            #region Arrange

            // Giving an empty ID string will cause an ArgumentException.
            string testId = "";

            #endregion

            #region Act & Assert

            // Assert that the constructor throws an ArgumentException when given
            // an empty string.
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new BuildingController(testId);
            });

            #endregion
        }

        /// <summary>
        /// The constructor should throw an ArgumentNullException when the
        /// given ID is null.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenNullID_ThrowsArgumentNullException()
        {
            #region Arrange

            // Giving a null ID will cause an ArgumentNullException.
            string testId = null;

            #endregion

            #region Act & Assert

            // Assert that the constructor throws an ArgumentNullException
            // when given null.
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                // ReSharper disable once ExpressionIsAlwaysNull
                new BuildingController(testId);
            });

            #endregion
        }

        #endregion

        #region Tests for Constructor(id, startState)

        /// <summary>
        /// Test for requirements L2R3 and L1R6.
        ///
        /// Constructing a BuildingController with a state should set the
        /// buildingID and currentState.
        /// GetCurrentState() should return the value of currentState.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenStartState_SetsBuildingIDAndCurrentState()
        {
            #region Arrange

            // If we construct a BuildingController with an ID and state
            // we can expect them to be the same when we get them with
            // GetBuildingID() and GetCurrentState().
            string testId = "building-100";
            string expectedId = "building-100";
            
            string testState = "out of hours";
            string expectedState = testState;
            
            #endregion

            #region Act
            
            // Construct a BuildingController with our test ID and state.
            BuildingController buildingController = new BuildingController(testId, testState);
            string actualId = buildingController.GetBuildingID();
            string actualState = buildingController.GetCurrentState();

            #endregion

            #region Assert

            // Assert that the buildingID and currentState are what we expected.
            Assert.AreEqual(expectedId, actualId);
            Assert.AreEqual(expectedState, actualState);

            #endregion
        }
        

        /// <summary>
        /// Test for requirement L2R3.
        ///
        /// The state given when constructing a BuildingController may
        /// only be "closed", "out of hours or "open" and will throw
        /// an ArgumentException if provided anything else.
        /// </summary>
        [TestCase("closed", ExpectedResult = true)]
        [TestCase("open", ExpectedResult = true)]
        [TestCase("out of hours", ExpectedResult = true)]
        [TestCase("invalid", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        public bool Constructor_WhenGivenInvalidStartState_ShouldThrowArgumentException(string testState)
        {
            #region Arrange

            // Provide a valid building ID to test with.
            string testId = "building-100";

            #endregion
            
            #region Act
            
            // Construct a BuildingController with our test state and catch
            // an ArgumentException if possible.
            ArgumentException argumentException = null;
            
            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                new BuildingController(testId, testState);
            }
            catch (ArgumentException ex)
            {
                argumentException = ex;
            }

            // If an ArgumentException is thrown, ensure it has the correct message.
            if (argumentException != null)
            {
                Assert.AreEqual("Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'",
                    argumentException.Message);
            }
            
            #endregion
            
            #region Assert

            // Return true if when no exception is thrown so NUnit can check
            // if an exception was to be expected or not.
            return argumentException == null;

            #endregion
        }
        
        /// <summary>
        /// Test for requirement L2R3.
        ///
        /// The state given when constructing a BuildingController should
        /// be converted to lower case.
        /// </summary>
        [TestCase("CLOSED", ExpectedResult = "closed")]
        [TestCase("oPeN", ExpectedResult = "open")]
        [TestCase("OUT of HOURS", ExpectedResult = "out of hours")]
        public string Constructor_WhenGivenStartState_ConvertsCaseToLower(string testState)
        {
            #region Arrange

            // Provide a valid building ID to test with.
            string testId = "building-100";

            #endregion
            
            #region Act
            
            // Construct a BuildingController with our test state and get
            // the converted value with GetCurrentState().
            BuildingController buildingController = new BuildingController(testId, testState);
            string actualState = buildingController.GetCurrentState();
            
            #endregion
            
            #region Assert

            // NUnit will check that if the result is what we expected.
            return actualState;

            #endregion
        }
        
        
        /// <summary>
        /// The constructor should throw an ArgumentNullException when the
        /// given state is null.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenNullStartState_ThrowsArgumentNullException()
        {
            #region Arrange

            // Giving a null startState will cause an ArgumentNullException.
            string testId = "building-100";
            string testState = null;

            #endregion

            #region Act & Assert

            // Assert that the constructor throws an ArgumentNullException
            // when given a null startState.
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                // ReSharper disable once ExpressionIsAlwaysNull
                new BuildingController(testId, testState);
            });

            #endregion
        }

        #endregion

        #region Tests for Constructor(id, lightManager, fireAlarmManager, doorManager, webService, emailService)

        /// <summary>
        /// Test for requirement L3R1.
        ///
        /// Constructing a BuildingController with an ID and
        /// implementations of the manager and service interfaces
        /// allows for dependency injection.
        /// </summary>
        [Test]
        public void Constructor_WhenGivenInterfaces()
        {
            #region Arrange

            // Provide a valid building ID to test with.
            string testId = "building-100";
            
            #endregion

            #region Act

            // Create an instance of BuildingController with our
            // dependency injected substitute classes.
            
            // ReSharper disable once ObjectCreationAsStatement
            new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);

            #endregion
        }

        #endregion

        #region Tests for SetBuildingID(id)

        /// <summary>
        /// Test for requirement L1R4.
        ///
        /// SetBuildingID() should set the value of buildingID to
        /// the given ID.
        /// </summary>
        [Test]
        public void SetBuildingID_WhenGivenID_SetsBuildingID()
        {
            #region Arrange

            // If we call SetBuildingID with a valid ID we can expect
            // it to be the same when we get it with GetBuildingID().
            string testId = "building-100";
            string expectedId = testId;

            // Create an instance of BuildingController with a different
            // ID to start with.
            string startingId = "building-0";
            BuildingController buildingController = new BuildingController(startingId);

            #endregion

            #region Act

            // Set the building ID with SetBuildingID() then retrieve it
            // with GetBuildingID().
            buildingController.SetBuildingID(testId);
            string actualId = buildingController.GetBuildingID();

            #endregion

            #region Assert

            // Assert that the ID retrieved by GetBuildingID() is
            // what we expected.
            Assert.AreEqual(expectedId, actualId);

            #endregion
        }

        /// <summary>
        /// Test for requirement L1R4.
        ///
        /// The ID given when calling SetBuildingID() should be converted
        /// to lower case.
        /// </summary>
        [TestCase("BUILDING-100", ExpectedResult = "building-100")]
        [TestCase("bUiLdInG-100", ExpectedResult = "building-100")]
        [TestCase("12345", ExpectedResult = "12345")]
        [TestCase("BUILDING", ExpectedResult = "building")]
        [TestCase("building", ExpectedResult = "building")]
        public string SetBuildingID_WhenGivenID_ConvertsCaseToLower(string testId)
        {
            #region Arrange
            
            // Create an instance of BuildingController with a different
            // ID to start with.
            string startingId = "building-0";
            BuildingController buildingController = new BuildingController(startingId);

            #endregion
            
            #region Act
            
            // Call SetBuildingID() with a test ID then get
            // the converted value.
            buildingController.SetBuildingID(testId);
            string actualId = buildingController.GetBuildingID();
            
            #endregion
            
            #region Assert

            // NUnit will check that if the result is what we expected.
            return actualId;

            #endregion
        }
        
        /// <summary>
        /// SetBuildingID() should throw an ArgumentException when the given
        /// ID is an empty string.
        /// </summary>
        [Test]
        public void SetBuildingID_WhenGivenEmptyID_ThrowsArgumentException()
        {
            #region Arrange

            // Giving an empty ID string will cause an ArgumentException.
            string testId = "";
            
            // Create an instance of BuildingController with a different
            // ID to start with.
            string startingId = "building-0";
            BuildingController buildingController = new BuildingController(startingId);

            #endregion

            #region Act & Assert

            // Assert that SetBuildingID() throws an ArgumentException when given
            // an empty string.
            Assert.Throws<ArgumentException>(() => buildingController.SetBuildingID(testId));

            #endregion
        }

        /// <summary>
        /// SetBuildingID() should throw an ArgumentNullException when the
        /// given ID is null.
        /// </summary>
        [Test]
        public void SetBuildingID_WhenGivenNullID_ThrowsArgumentNullException()
        {
            #region Arrange

            // Giving a null ID will cause an ArgumentNullException.
            string testId = null;
            
            
            // Create an instance of BuildingController with a different
            // ID to start with.
            string startingId = "building-0";
            BuildingController buildingController = new BuildingController(startingId);

            #endregion

            #region Act & Assert

            // Assert SetBuildingID() throws an ArgumentNullException
            // when given null.
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                buildingController.SetBuildingID(testId);
            });

            #endregion
        }
        
        #endregion

        #region Tests for SetCurrentState(state)

        /// <summary>
        /// Test for requirement L1R7.
        ///
        /// SetCurrentState() should set the value of currentState to
        /// the given state.
        /// </summary>
        [Test]
        public void SetCurrentState_WhenGivenState_SetsCurrentState()
        {
            #region Arrange

            // Set the substitute of IDoorManager to always open
            // all doors successfully so the open state can be reached.
            _doorManager.OpenAllDoors().Returns(true);
            
            // If we call SetCurrentState() with a valid state we can expect
            // it to be the same when we get it with GetCurrentState().
            string testState = "open";
            string expectedState = testState;

            // Create an instance of BuildingController.
            string testId = "building-100";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);

            #endregion

            #region Act

            // Set the state with SetCurrentState() then retrieve it
            // with GetBuildingID().
            buildingController.SetCurrentState(testState);
            string actualState = buildingController.GetCurrentState();

            #endregion

            #region Assert

            // Assert that the ID retrieved by GetBuildingID() is
            // what we expected.
            Assert.AreEqual(expectedState, actualState);

            #endregion
        }

        /// <summary>
        /// Test for requirements L2R1 and L1R7.
        ///
        /// SetCurrentState() should only allow state changes according to
        /// the state transition diagram.
        /// SetCurrentState() should return false and stay the same state
        /// if called with an invalid state change.
        /// </summary>
        [TestCase("open", "closed", ExpectedResult = false)]
        [TestCase("closed", "open", ExpectedResult = false)]
        [TestCase("open", "out of hours", ExpectedResult = true)]
        [TestCase("out of hours", "open", ExpectedResult = true)]
        [TestCase("closed", "out of hours", ExpectedResult = true)]
        [TestCase("out of hours", "closed", ExpectedResult = true)]
        [TestCase("open", "fire drill", ExpectedResult = true)]
        [TestCase("out of hours", "fire drill", ExpectedResult = true)]
        [TestCase("closed", "fire drill", ExpectedResult = true)]
        [TestCase("open", "fire alarm", ExpectedResult = true)]
        [TestCase("out of hours", "fire alarm", ExpectedResult = true)]
        [TestCase("closed", "fire alarm", ExpectedResult = true)]
        [TestCase("open", "invalid", ExpectedResult = false)]
        [TestCase("closed", "invalid", ExpectedResult = false)]
        [TestCase("out of hours", "invalid", ExpectedResult = false)]
        public bool SetCurrentState_WhenGivenInvalidStateChange_ReturnsFalse(string startState, string testState)
        {
            #region Arrange

            // Set the substitute of IDoorManager to always open
            // all doors successfully so the open state can be reached.
            _doorManager.OpenAllDoors().Returns(true);
            
            // Create a BuildingController.
            string testId = "building-100";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion

            #region Act
            
            // Set the starting state then change it to the test state.
            buildingController.SetCurrentState(startState);
            bool success = buildingController.SetCurrentState(testState);

            #endregion'
            
            #region Assert

            // NUnit will assert the state change success against
            // the expected result.
            return success;

            #endregion
        }
        
        /// <summary>
        /// Test for requirements L2R1 and L1R7.
        ///
        /// SetCurrentState() should only allow state changes according to
        /// the state transition diagram.
        /// SetCurrentState() should return false and stay the same state
        /// if called with an invalid state change.
        /// </summary>
        [TestCase("open", "fire drill", "open", ExpectedResult = true)]
        [TestCase("out of hours", "fire drill", "out of hours", ExpectedResult = true)]
        [TestCase("closed", "fire drill", "closed", ExpectedResult = true)]
        [TestCase("open", "fire drill", "closed", ExpectedResult = false)]
        [TestCase("out of hours", "fire drill", "open", ExpectedResult = false)]
        [TestCase("closed", "fire drill", "out of hours", ExpectedResult = false)]
        [TestCase("open", "fire drill", "invalid", ExpectedResult = false)]
        [TestCase("open", "fire alarm", "open", ExpectedResult = true)]
        [TestCase("out of hours", "fire alarm", "out of hours", ExpectedResult = true)]
        [TestCase("closed", "fire alarm", "closed", ExpectedResult = true)]
        [TestCase("open", "fire alarm", "closed", ExpectedResult = false)]
        [TestCase("out of hours", "fire alarm", "open", ExpectedResult = false)]
        [TestCase("closed", "fire alarm", "out of hours", ExpectedResult = false)]
        [TestCase("open", "fire alarm", "invalid", ExpectedResult = false)]
        [TestCase("open", "fire drill", "fire alarm", ExpectedResult = false)]
        [TestCase("open", "fire alarm", "fire drill", ExpectedResult = false)]
        public bool SetCurrentState_WhenGivenInvalidChangeFromFireAlarmOrDrill_ReturnsFalse(string state1,
            string state2, string state3)
        {
            #region Arrange

            // Set the substitute of IDoorManager to always open
            // all doors successfully so the open state can be reached.
            _doorManager.OpenAllDoors().Returns(true);
            
            // Create a BuildingController.
            string testId = "building-100";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion

            #region Act
            
            // Change the state in the order provided.
            buildingController.SetCurrentState(state1);
            buildingController.SetCurrentState(state2);
            bool success = buildingController.SetCurrentState(state3);

            #endregion'
            
            #region Assert

            // NUnit will assert the state change success against
            // the expected result.
            return success;

            #endregion
        }
        
        /// <summary>
        /// Test for requirements L2R2 and L1R7.
        ///
        /// SetCurrentState() should keep currentState the same and return
        /// true when currentState is the same as the state provided.
        /// </summary>
        [TestCase("open")]
        [TestCase("closed")]
        [TestCase("out of hours")]
        public void SetCurrentState_WhenGivenSameState_RemainsSame(string testState)
        {
            #region Arrange
            
            // Create a BuildingController with the given test state.
            string testId = "building-100";
            BuildingController buildingController = new BuildingController(testId, testState);

            #endregion

            #region Act

            bool result = buildingController.SetCurrentState(testState);
            string newState = buildingController.GetCurrentState();

            #endregion

            #region Assert

            // Assert that the state change result is true and
            // the state is still the same.
            Assert.IsTrue(result);
            Assert.AreEqual(testState, newState);

            #endregion
        }

        /// <summary>
        /// Test for requirements L3R4 and L3R5.
        ///
        /// When SetCurrentState() is called with "open" DoorManager.OpenAllDoors()
        /// should be called by the BuildingController.
        /// The state should only become open if OpenAllDoors() returns true,
        /// else false will be returned and the previous state is kept.
        /// </summary>
        [TestCase(true)]
        [TestCase(false)]
        public void SetCurrentState_WhenGivenOpen_BecomesOpenIfOpenAllDoorsSuccessful(bool openedSuccessfully)
        {
            #region Arrange

            // Set the substitute of IDoorManager to return
            // the desired success value.
            _doorManager.OpenAllDoors().Returns(openedSuccessfully);
            
            // Create a BuildingController.
            string testId = "building-100";
            string testState = "open";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion

            #region Act

            //Attempt
            bool result = buildingController.SetCurrentState(testState);
            string newState = buildingController.GetCurrentState();

            #endregion

            #region Assert

            // Ensure DoorManager.OpenAllDoors() was called.
            _doorManager.Received().OpenAllDoors();
            
            // The result of SetCurrentState("open") should be
            // whether or not the doors opened successfully.
            Assert.AreEqual(openedSuccessfully, result);

            // If the doors didn't open successfully we can expect
            // the state to be the same as what we started with.
            if (!openedSuccessfully)
            {
                Assert.AreEqual("out of hours", newState);
            }

            #endregion
        }

        /// <summary>
        /// Test for requirement L4R1.
        ///
        /// When SetCurrentState is called with "closed", DoorManager.LockAllDoors()
        /// and LightManager.SetAllLights(false) should be called by the BuildingController.
        /// </summary>
        [Test]
        public void SetCurrentState_WhenGivenClosed_LocksAllDoorsAndTurnsOffAllLights()
        {
            #region Arrange
            
            // Create a BuildingController.
            string testId = "building-100";
            string testState = "closed";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion

            #region Act

            // Set the current state to "closed".
            bool result = buildingController.SetCurrentState(testState);

            #endregion

            #region Assert

            // Setting the state to "closed" isn't dependent on the
            // DoorManager or LightManager, so it should be successful.
            Assert.IsTrue(result);
            
            // Check that all doors were locked and all of the
            // lights were turned off.
            _doorManager.Received().LockAllDoors();
            _lightManager.Received().SetAllLights(false);

            #endregion
        }

        /// <summary>
        /// Test for requirement L4R2.
        ///
        /// When SetCurrentState is called with "fire alarm", FireAlarmManager.SetAlarm(true),
        /// DoorManager.OpenAllDoors(), LightManager.SetAllLights(true) and
        /// WebService.LogFireAlarm("fire alarm") should be called by the BuildingController.
        /// </summary>
        [Test]
        public void SetCurrentState_WhenGivenFireAlarm_StartsAlarmAndOpensAllDoors()
        {
            #region Arrange
            
            // Create a BuildingController.
            string testId = "building-100";
            string testState = "fire alarm";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion
            
            #region Act

            // Set the current state to "fire alarm".
            bool result = buildingController.SetCurrentState(testState);

            #endregion
            
            #region Assert

            // Setting the state to "fire alarm" should always be successful.
            Assert.IsTrue(result);
            
            // Check that the alarm was enabled, all doors were opened,
            // all lights were turned on and the alarm was logged with the
            // WebService.
            _fireAlarmManager.Received().SetAlarm(true);
            _doorManager.Received().OpenAllDoors();
            _lightManager.Received().SetAllLights(true);
            _webService.Received().LogFireAlarm("fire alarm");

            #endregion
        }

        /// <summary>
        /// Test for requirement L4R4.
        ///
        /// If WebService.LogFireAlarm() throws an Exception when the fire alarm state
        /// is triggered an email should be sent using EmailService.SendMail().
        /// </summary>
        [Test]
        public void SetCurrentState_WhenGivenFireAlarm_SendsEmailOnWebServiceException()
        {
            #region Arrange
            
            // Set the substitute of IWebService to throw an Exception
            // when calling LogFireAlarm().
            Exception exception = new Exception("This is a test Exception.");
            _webService
                .When(service => service.LogFireAlarm("fire alarm"))
                .Do((info => throw exception));
            
            // Create a BuildingController.
            string testId = "building-100";
            string testState = "fire alarm";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);

            #endregion
            
            #region Act

            // Set the current state to "fire alarm".
            bool result = buildingController.SetCurrentState(testState);

            #endregion
            
            #region Assert

            // Setting the state to "fire alarm" should always be successful.
            Assert.IsTrue(result);
            
            // Check that the alarm was enabled, all doors were opened,
            // all lights were turned on and the alarm was logged with the
            // WebService.
            _fireAlarmManager.Received().SetAlarm(true);
            _doorManager.Received().OpenAllDoors();
            _lightManager.Received().SetAllLights(true);
            _webService.Received().LogFireAlarm("fire alarm");
            
            // Check that an email was sent using EmailService.
            _emailService.Received().SendMail(
                "smartbuilding@uclan.ac.uk", 
                "failed to log alarm",
                exception.Message
            );

            #endregion
        }

        /// <summary>
        /// SetCurrentState() should return false when the given state is null.
        /// </summary>
        [Test]
        public void SetCurrentState_WhenGivenNull_ReturnsFalse()
        {
            #region Arrange
            
            // Create a BuildingController.
            string testId = "building-100";
            string testState = null;
            BuildingController buildingController = new BuildingController(testId);

            #endregion

            #region Act

            // Set the current state to null and get the new value.
            
            // ReSharper disable once ExpressionIsAlwaysNull
            bool result = buildingController.SetCurrentState(testState);
            string newState = buildingController.GetCurrentState();

            #endregion

            #region Assert

            // Assert that the state change result is false and
            // the state is the same as what we started with
            Assert.IsFalse(result);
            Assert.AreEqual("out of hours", newState);

            #endregion
        }
        
        #endregion

        #region Tests for GetStatusReport()

        /// <summary>
        /// Test for requirement L3R3.
        /// 
        /// GetStatusReport() should call IManager.GetStatus() for every manager
        /// on the BuildingController class (Light, Door, Fire alarm) and append
        /// their outputs together to produce the status report.
        /// </summary>
        [Test]
        public void GetStatusReport_WhenCalled_AppendsStatusOfEveryManager()
        {
            #region Arrange

            // Set the substitutes of the managers to return a status string
            // in the required format.
            _lightManager.GetStatus().Returns("Lights,OK,OK,OK,");
            _doorManager.GetStatus().Returns("Doors,OK,OK,OK,");
            _fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,");

            // We can expect the report to be the status strings joined
            // together like this.
            string expectedReport = "Lights,OK,OK,OK,Doors,OK,OK,OK,FireAlarm,OK,OK,OK,";

            // Create a BuildingController.
            string testId = "building-100";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion

            #region Act

            // Get the status report from the BuildingController.
            string actualReport = buildingController.GetStatusReport();

            #endregion

            #region Assert

            // Ensure GetStatus() was called on every manager.
            _lightManager.Received().GetStatus();
            _doorManager.Received().GetStatus();
            _fireAlarmManager.Received().GetStatus();
            
            // Ensure the generated report is what we expected.
            Assert.AreEqual(expectedReport, actualReport);

            #endregion
        }

        /// <summary>
        /// Test for requirements L3R3 and L4R3.
        /// 
        /// GetStatusReport() should call IWebService.LogEngineerRequired() when
        /// one or more managers reports a fault.
        /// LogEngineerRequired() should be given the type of device that failed,
        /// and given a comma seperated list of device types if there is more than
        /// one, the list must contain a trailing comma.
        /// </summary>
        [TestCase("Lights,OK,OK,", "Doors,OK,OK,", "FireAlarm,OK,OK,", ExpectedResult = "")]
        [TestCase("Lights,FAULT,OK,", "Doors,OK,OK,", "FireAlarm,OK,OK,", ExpectedResult = "Lights")]
        [TestCase("Lights,OK,FAULT,", "Doors,FAULT,OK,", "FireAlarm,OK,OK,", ExpectedResult = "Lights,Doors,")]
        [TestCase("Lights,FAULT,OK,", "Doors,OK,FAULT,", "FireAlarm,FAULT,OK,", ExpectedResult = "Lights,Doors,FireAlarm,")]
        [TestCase("Lights,", "Doors,", "FireAlarm,", ExpectedResult = "")]
        public string GetStatusReport_WhenCalledWithFaults_CallsLogEngineerRequired(string lightReport, string doorReport, string fireAlarmReport)
        {
            #region Arrange
           
            // Set the substitutes of the managers to return the
            // given status string.
            _lightManager.GetStatus().Returns(lightReport);
            _doorManager.GetStatus().Returns(doorReport);
            _fireAlarmManager.GetStatus().Returns(fireAlarmReport);

            // Set the substitute of IWebService up to allow us to get the
            // log details provided from BuildingController.
            string actualLogDetails = "";
            _webService.LogEngineerRequired(Arg.Do<string>(logDetails =>
            {
                actualLogDetails = logDetails;
            }));
            
            // Create a BuildingController.
            string testId = "building-100";
            BuildingController buildingController = new BuildingController(testId, _lightManager, _fireAlarmManager, _doorManager, _webService, _emailService);
            
            #endregion
            
            #region Act

            // Get the status report from the BuildingController.
            buildingController.GetStatusReport();

            #endregion

            #region Assert

            // Returning the log details will allow NSubstitute to assert
            // against the expected value.
            return actualLogDetails;

            #endregion
        }
        
        #endregion
    }
}