namespace SmartBuilding
{
    public interface IFireAlarmManager : IManager<IFireAlarm>
    {
        void SetAlarm(bool isActive);
    }
}