namespace SmartBuilding
{
    public interface IFireAlarmManager : IManager
    {
    void SetAlarm(bool isActive);
    }
}