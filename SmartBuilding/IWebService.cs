namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for the WebService class.
    /// </summary>
    public interface IWebService
    {
        void LogStateChange(string logDetails);
        void LogEngineerRequired(string logDetails);
        void LogFireAlarm(string logDetails);
    }
}