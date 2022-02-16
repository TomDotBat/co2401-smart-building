namespace SmartBuilding
{
    public interface IDoorManager
    {
        bool OpenDoor(int doorId);
        bool LockDoor(int doorId);
        bool OpenAllDoors();
        bool LockAllDoors();
    }
}