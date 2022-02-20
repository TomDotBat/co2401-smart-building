namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for the LightManager class.
    /// </summary>
    public interface ILightManager : IManager
    {
    void SetLight(bool isOn, int lightID);
    void SetAllLights(bool isOn);
    }
}