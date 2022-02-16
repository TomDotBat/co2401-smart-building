namespace SmartBuilding
{
    public interface ILightManager
    {
        void SetLight(bool isOn, int lightId);
        void SetAllLights(bool isOn);
    }
}