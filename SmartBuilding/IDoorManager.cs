namespace SmartBuilding
{
    public interface IDoorManager : IManager
    {
        bool OpenDoor(int doorId);
        bool LockDoor(int doorId);
        bool OpenAllDoors();
        bool LockAllDoors();
    }
}