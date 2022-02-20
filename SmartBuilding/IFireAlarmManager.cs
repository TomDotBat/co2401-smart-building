namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for the FireAlarm class.
    /// </summary>
    public interface IFireAlarmManager : IManager
    {
    void SetAlarm(bool isActive);
    }
}