namespace SmartBuilding
{
    public interface IBuildingController
    {
        string GetCurrentState();
        bool SetCurrentState(string state);
        string GetBuildingID(); //bad casing but ok
        void SetBuildingID(string id); //im fuming
        string GetStatusReport();
    }
}