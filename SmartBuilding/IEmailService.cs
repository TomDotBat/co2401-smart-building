namespace SmartBuilding
{
    /// <summary>
    /// Interface definition for the EmailService class.
    /// </summary>
    public interface IEmailService
    {
        void SendMail(string emailAddress, string subject, string message);
    }
}