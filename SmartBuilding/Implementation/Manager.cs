using System;
using System.Collections.Generic;

namespace SmartBuilding.Implementation
{
    /// <summary>
    /// Abstract class to implement IManager methods.
    /// </summary>
    public abstract class Manager : IManager
    {
        /// <summary>
        /// Instantiates a Manager with the device type "Unknown".
        /// This constructor shouldn't normally exist, but in this
        /// case it does for testing purposes.
        /// </summary>
        public Manager() : this("Unknown") { }
        
        /// <summary>
        /// Instantiates a Manager with the given device type.
        /// </summary>
        /// <param name="deviceType">The type of device being managed.</param>
        /// <exception cref="ArgumentNullException">Thrown when the device type is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the device type is an empty string or whitespace.</exception>
        protected Manager(string deviceType)
        {
            // The device type is null, throw an exception.
            if (deviceType == null)
            {
                throw new ArgumentNullException(nameof(deviceType), "Argument Null Exception: Device type must be present.");
            }
        
            // The device type is an empty string/whitespace, throw an exception.
            if (String.IsNullOrWhiteSpace(deviceType))
            {
                throw new ArgumentException("Argument Exception: Device type cannot be empty or whitespace.");
            }
            
            // Set the device type to the given value,
            // default engineerRequired to false and
            // initialise the devices list.
            DeviceType = deviceType;
            engineerRequired = false;
            _devices = new List<IDevice>();
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
        
        /// <summary>
        /// Registers a device as a managed device.
        /// </summary>
        /// <param name="device">The device to manage.</param>
        /// <exception cref="ArgumentNullException">Thrown when the device is null.</exception>
        public void RegisterDevice(IDevice device)
        {
            // The device is null, throw an exception.
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Argument Null Exception: Cannot register null as a managed device.");
            }
            
            _devices.Add(device);
        }

        /// <summary>
        /// Registers a device as a managed device.
        /// </summary>
        /// <param name="device">The device to manage.</param>
        public void RegisterDevice(IDevice device)
        {
            _devices.Add(device);
        }

        protected readonly string DeviceType;
        // ReSharper disable once InconsistentNaming
        protected bool engineerRequired;
        // ReSharper disable once InconsistentNaming
        protected List<IDevice> _devices;
    }
}