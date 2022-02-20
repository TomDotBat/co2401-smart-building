namespace SmartBuilding.Implementation
{
    public class FireAlarmManager : Manager<IFireAlarm>, IFireAlarmManager
    {
        /// <summary>
        /// Instantiates a FireAlarmManager and sets the manager type.
        /// </summary>
        public FireAlarmManager() : base("FireAlarm") { }

        /// <summary>
        /// Sets active state of all managed fire alarms.
        /// </summary>
        /// <param name="isActive">True if the fire alarm is active, false if not.</param>
        public void SetAlarm(bool isActive)
        {
            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (IFireAlarm fireAlarm in _devices)
            {
                fireAlarm.SetActive(isActive);
            }
        }
    }
}