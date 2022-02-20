namespace SmartBuilding
{
    public interface IDoorManager : IManager<IDoor>
    {
        bool OpenDoor(int doorID);
        bool LockDoor(int doorID);
        bool OpenAllDoors();
        bool LockAllDoors();
    }
}