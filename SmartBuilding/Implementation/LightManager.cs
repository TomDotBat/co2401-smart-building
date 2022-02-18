using System;

namespace SmartBuilding.Implementation
{
    public class LightManager : ILightManager
    {
        public string GetStatus()
        {
            return "Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";
        }
        
        public bool SetEngineerRequired(bool needsEngineer)
        {
            _engineerRequired = needsEngineer;
            return true;
        }
        
        public void SetLight(bool isOn, int lightId)
        {
            throw new NotImplementedException();
        }

        public void SetAllLights(bool isOn)
        {
            throw new NotImplementedException();
        }

        private bool _engineerRequired;
    }
}