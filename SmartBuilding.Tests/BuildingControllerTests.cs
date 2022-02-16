using System.Collections.Generic;
using NUnit.Framework;
using SmartBuilding.Implementation;

namespace SmartBuilding.Tests
{
    [TestFixture]
    public class BuildingControllerTests
    {
        #region Level 1 Requirements
        
        //L1R1 & L1R2 & L1R3
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

        //L1R4 & L1R2
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

        //L1R5 & L1R6
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

        //L1R7 & L1R6
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
        
        //L1R7 & L1R6
        [Test]
        public void WhenSetCurrentState_IsSuppliedInvalidState_ReturnFalse()
        {
            #region Arrange

            string invalidState = "invalid";
            IBuildingController buildingController = new BuildingController("BUILD-100");

            #endregion

            #region Act

            bool returnedState = buildingController.SetCurrentState(invalidState);

            #endregion

            #region Assert

            Assert.IsFalse(returnedState);
            Assert.AreNotEqual(invalidState, buildingController.GetCurrentState());

            #endregion
        }
        
        #endregion
    }
}