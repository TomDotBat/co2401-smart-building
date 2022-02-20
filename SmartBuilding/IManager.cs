namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for the Manager class.
    /// </summary>
    public interface IManager
    {
        string GetStatus();
        bool SetEngineerRequired(bool needsEngineer);
        void RegisterDevice(IDevice device);
    }
}