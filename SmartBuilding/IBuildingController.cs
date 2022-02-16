namespace SmartBuilding
{
    public interface IBuildingController
    {
        string GetCurrentState();
        bool SetCurrentState(string state);
        string GetBuildingID(); //Should be GetBuildingId & SetBuildingId
        void SetBuildingID(string id);
        string GetStatusReport();
    }
}