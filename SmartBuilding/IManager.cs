namespace SmartBuilding
{
    public interface IManager<T> where T : IDevice
    {
        string GetStatus();
        bool SetEngineerRequired(bool needsEngineer);
        void RegisterDevice(IDevice device);
    }
}