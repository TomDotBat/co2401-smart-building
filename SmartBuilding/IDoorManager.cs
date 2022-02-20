namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for the DoorManager class.
    /// </summary>
    public interface IDoorManager : IManager
    {
        bool OpenDoor(int doorID);
        bool LockDoor(int doorID);
        bool OpenAllDoors();
        bool LockAllDoors();
    }
}