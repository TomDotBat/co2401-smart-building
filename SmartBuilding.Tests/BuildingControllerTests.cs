using System;
using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    [TestFixture]
    public class BuildingControllerTests
    {
        #region Level 1 Requirements

        /// <summary>
        /// L1R1 & L1R2 & L1R3
        ///
        /// Test that the constructor sets the given ID in lower case.
        /// Also tests that GetBuildingID retrieves the ID as expected.
        /// </summary>
        [Test]
        public void WhenConstructor_SetsBuildingID_AsLowerCase()
        {
            #region Arrange

            string testId = "BUILD-100";
            string expectedId = "build-100";

            #endregion

            #region Act

            IBuildingController buildingController = new BuildingController(testId);

            #endregion

            #region Assert

            Assert.AreEqual(expectedId, buildingController.GetBuildingID());

            #endregion
        }

        /// <summary>
        /// L1R4 & L1R2
        ///
        /// Test that SetBuildingID sets the given ID in lower case.
        /// Also tests that GetBuildingID retrieves the ID as expected.
        /// </summary>
        [Test]
        public void WhenSetBuildingID_SetsBuildingID_AsLowerCase()
        {
            #region Arrange

            string testId = "BUILD-100";
            string expectedId = "build-100";
            IBuildingController buildingController = new BuildingController("BUILD-000");

            #endregion

            #region Act

            buildingController.SetBuildingID(testId);

            #endregion

            #region Assert

            Assert.AreEqual(expectedId, buildingController.GetBuildingID());

            #endregion
        }

        /// <summary>
        /// L1R5 & L1R6
        ///
        /// Test that the single parameter constructor defaults the current
        /// state to "out of hours".
        /// Also tests that GetCurrentState retrieves the current state as expected.
        /// </summary>
        [Test]
        public void WhenConstructor_SetsInitialCurrentState_ToOutOfHours()
        {
            #region Arrange

            string expectedState = "out of hours";

            #endregion

            #region Act

            IBuildingController buildingController = new BuildingController("BUILD-100");

            #endregion

            #region Assert

            Assert.AreEqual(expectedState, buildingController.GetCurrentState());

            #endregion
        }

        /// <summary>
        /// L1R7 & L1R6
        ///
        /// Test that SetCurrentState allows setting to all the expected valid states.
        /// </summary>
        [Test]
        public void WhenSetCurrentState_IsSuppliedValidState_ReturnTrue()
        {
            #region Arrange

            string[] validStates = {"closed", "out of hours", "open", "fire drill", "fire alarm"};
            IBuildingController buildingController = new BuildingController("BUILD-100");

            #endregion

            #region Act

            foreach (string validState in validStates)
            {

                #region Assert

                Assert.IsTrue(buildingController.SetCurrentState(validState));
                Assert.AreEqual(validState, buildingController.GetCurrentState());

                #endregion
            }

            #endregion
        }

        /// <summary>
        /// L1R7 & L1R6
        /// 
        /// Test that SetCurrentState doesn't allow setting to an invalid value.
        /// </summary>
        [Test]
        public void WhenSetCurrentState_IsSuppliedInvalidState_ReturnFalse()
        {
            #region Arrange

            string invalidState = "invalid";
            IBuildingController buildingController = new BuildingController("BUILD-100");

            #endregion

            #region Act

            bool actualState = buildingController.SetCurrentState(invalidState);

            #endregion

            #region Assert

            Assert.IsFalse(actualState);
            Assert.AreNotEqual(invalidState, buildingController.GetCurrentState());

            #endregion
        }

        #endregion

        #region Level 2 Requirements

        /// <summary>
        /// L2R1 & L1R6
        ///
        /// Test that SetCurrentState disallows invalid state changes
        /// but allows the valid ones.
        /// </summary>
        [Test]
        public void WhenSetCurrentState_IsSuppliedDisallowedStateChange_ReturnFalse()
        {
            #region Arrange

            (string, bool)[] stateChanges =
            {
                ("out of hours", true),
                ("closed", true),
                ("closed", true),
                ("open", false), //Can't change from closed to open
                ("out of hours", true),
                ("open", true),
                ("open", true),
                ("closed", false), //Can't change from open to closed
                ("out of hours", true),
                ("fire drill", true),
                ("fire alarm", true),
                ("closed", false), //Must change back to the state before the fire drill
                ("out of hours", true),
                ("fire alarm", true),
                ("open", false), //Must change back to the state before the fire alarm
                ("fire drill", true),
                ("out of hours", true)
            };

            IBuildingController buildingController = new BuildingController("BUILD-100");

            #endregion

            #region Act

            foreach ((string, bool) stateChange in stateChanges)
            {
                string initialState = buildingController.GetCurrentState();

                #region Assert

                //Perform the state change and check if it's the value we expected
                Assert.AreEqual(stateChange.Item2, buildingController.SetCurrentState(stateChange.Item1));

                //If we expect a change, assert it
                if (initialState != stateChange.Item1)
                {
                    Assert.AreEqual(stateChange.Item2, buildingController.GetCurrentState());
                }

                #endregion
            }

            #endregion
        }

        /// <summary>
        /// L2R3 & L1R2 & L1R6
        ///
        /// Test that the 2 parameter constructor sets the building ID and state
        /// in lower case.
        /// </summary>
        [Test]
        public void WhenConstructor_SetsBuildingIDAndCurrentState()
        {
            #region Arrange

            string testId = "BUILD-100";
            string expectedId = "build-100";

            string testState = "OPEN";
            string expectedState = "open";

            #endregion

            #region Act

            IBuildingController buildingController = new BuildingController(testId, testState);

            #endregion

            #region Assert

            Assert.AreEqual(expectedId, buildingController.GetBuildingID());
            Assert.AreEqual(expectedState, buildingController.GetCurrentState());

            #endregion
        }

        /// <summary>
        /// L2R3
        ///
        /// Test that the constructor throws an ArgumentException when the provided
        /// current state is invalid.
        /// </summary>
        [Test]
        public void WhenConstructor_SetsInvalidCurrentState_ThrowsArgumentException()
        {
            #region Arrange

            string testId = "BUILD-100";
            string testState = "invalid";

            #endregion

            #region Act & Assert

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                new BuildingController(testId, testState);
            });

            if (exception != null)
            {
                Assert.Equals(exception.Message,
                    "Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'");
            }

            #endregion
        }

        #endregion

        #region Level 3 Requirements

        /// <summary>
        /// L3R1
        ///
        /// Test that...
        /// </summary>
        [Test]
        public void WhenConstructor()
        {
            string testId = "BUILD-100";
        }


        /// <summary>
        /// L3R3
        ///
        /// Test that GetStatusReport returns the correct output.
        /// </summary>
        [Test]
        public void WhenGetStatusReport_IsCalled_ReturnsExpectedOutput()
        {
            
            #region Arrange

            string expectedOutput = "Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,";
            string testId = "BUILD-100";
            IBuildingController buildingController = new BuildingController(testId);

            #endregion

            #region Act

            string output = buildingController.GetStatusReport();

            #endregion

            #region Assert

            Assert.AreEqual(expectedOutput, output);

            #endregion
        }

        #endregion
    }
}