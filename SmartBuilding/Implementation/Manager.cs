using System;

namespace SmartBuilding.Implementation
{
    public abstract class Manager
    {
        public string GetStatus()
        {
            throw new NotImplementedException();
        }

        public bool SetEngineerRequired(bool needsEngineer)
        {
            throw new NotImplementedException();
        }
    }
}