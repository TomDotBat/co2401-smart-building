namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for a Device.
    /// </summary>
    public interface IDevice
    {
        string GetState();
        string SetState();
    }
}