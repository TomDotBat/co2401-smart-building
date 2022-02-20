namespace SmartBuilding
{
    public interface IFireAlarm : IDevice
    {
        void SetActive(bool isActive);
    }
}