namespace SmartBuilding
{
    public interface IManager
    {
        string GetStatus();
        bool SetEngineerRequired(bool needsEngineer);
    }
}