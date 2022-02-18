namespace SmartBuilding
{
    public interface ILightManager : IManager

    {
    void SetLight(bool isOn, int lightId);
    void SetAllLights(bool isOn);
    }
}