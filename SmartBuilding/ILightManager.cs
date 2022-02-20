namespace SmartBuilding
{
    public interface ILightManager : IManager<ILight>
    {
        void SetLight(bool isOn, int lightID);
        void SetAllLights(bool isOn);
    }
}