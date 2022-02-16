using System;

namespace SmartBuilding.Implementation
{
    public class BuildingController : IBuildingController
    {
        public BuildingController(string id) { }
        
        public string GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public bool SetCurrentState(string state)
        {
            throw new NotImplementedException();
        }

        public string GetBuildingID()
        {
            throw new NotImplementedException();
        }

        public void SetBuildingID(string id)
        {
            throw new NotImplementedException();
        }

        public string GetStatusReport()
        {
            throw new NotImplementedException();
        }
    }
}