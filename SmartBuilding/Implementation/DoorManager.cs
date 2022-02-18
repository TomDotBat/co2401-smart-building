using System;

namespace SmartBuilding.Implementation
{
    public class DoorManager : IDoorManager
    {
        public string GetStatus()
        {
            return "Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";
        }
        
        public bool SetEngineerRequired(bool needsEngineer)
        {
            _engineerRequired = needsEngineer;
            return true;
        }
        
        public bool OpenDoor(int doorId)
        {
            throw new NotImplementedException();
        }

        public bool LockDoor(int doorId)
        {
            throw new NotImplementedException();
        }

        public bool OpenAllDoors()
        {
            throw new NotImplementedException();
        }

        public bool LockAllDoors()
        {
            throw new NotImplementedException();
        }

        private bool _engineerRequired;
    }
}