namespace SmartBuilding
{
    public interface IDoor : IDevice
    {
        bool Open();
        bool Lock();
    }
}