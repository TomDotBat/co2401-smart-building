using System.Collections.Generic;

namespace SmartBuilding.Implementation
{
    public abstract class Manager : IManager
    {
        /// <summary>
        /// Instantiates a Manager with a device type used by GetStatus().
        /// </summary>
        /// <param name="deviceType">The type of device being managed.</param>
        protected Manager(string deviceType)
        {
            // Set the device type to the given value
            // and default engineerRequired to false.
            DeviceType = deviceType;
            engineerRequired = false;
        }

        /// <summary>
        /// Sets whether or not an engineer is required for this type of device.
        /// </summary>
        /// <param name="needsEngineer">True if an engineer is required, false if not.</param>
        /// <returns>True if the state was set successfully, false if not.</returns>
        public bool SetEngineerRequired(bool needsEngineer)
        {
            // Set engineerRequired to the given value
            // and return true as it was successful.
            engineerRequired = needsEngineer;
            return true;
        }
        
        /// <summary>
        /// Gets the status of the Manager.
        /// </summary>
        /// <returns>The Manager device type followed by a list of device statuses.</returns>
        public string GetStatus()
        {
            // Create a string with the device type
            // followed by a comma.
            string statusString = DeviceType + ",";
            
            // Append each device state followed by a comma
            // to the status string.
            foreach (IDevice device in _devices)
            {
                statusString += device.GetState() + ",";
            }

            return statusString;
        }

        protected readonly string DeviceType;
        // ReSharper disable once InconsistentNaming
        protected bool engineerRequired;
        // ReSharper disable once InconsistentNaming
        protected List<IDevice> _devices;
    }
}