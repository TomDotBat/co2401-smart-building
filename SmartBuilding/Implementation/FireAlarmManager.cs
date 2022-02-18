using System;

namespace SmartBuilding.Implementation
{
    public class FireAlarmManager : IFireAlarmManager
    {
        public string GetStatus()
        {
            return "FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";
        }
        
        public bool SetEngineerRequired(bool needsEngineer)
        {
            _engineerRequired = needsEngineer;
            return true;
        }
        
        public void SetAlarm(bool isActive)
        {
            throw new NotImplementedException();
        }

        private bool _engineerRequired;
    }
}