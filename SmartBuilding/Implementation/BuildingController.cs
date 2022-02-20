using System;

namespace SmartBuilding.Implementation
{
    public class BuildingController
    {
        /// <summary>
        /// Instantiates a BuildingController with the given ID.
        /// </summary>
        /// <param name="id">The ID of the building being controlled.</param>
        /// <exception cref="ArgumentNullException">Thrown when the ID is null.</exception>
        /// <exception cref="ApplicationException">Thrown when the ID is an empty string or whitespace.</exception>
        public BuildingController(string id)
        {
            // Call SetBuildingID with the given ID to eliminate repeated code.
            SetBuildingID(id);
            
            // Set the initial value of currentState to "out of hours".
            currentState = "out of hours";
        }

        /// <summary>
        /// Instantiates a BuildingController with the given ID and state.
        /// </summary>
        /// <param name="id">The ID of the building being controlled.</param>
        /// <param name="startState">The starting state of the building.</param>
        /// <exception cref="ArgumentNullException">Thrown when the ID or state is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the ID is an empty string or whitespace or when the state is not 'open', 'closed' or 'out of hours'.</exception>
        public BuildingController(string id, string startState) : this(id)
        {
            // The start state is null, throw an exception.
            if (startState == null)
            {
                throw new ArgumentNullException(nameof(startState), "Argument Null Exception: Start state cannot be initialised to null.");
            }

            // Convert the start state to lower case.
            startState = startState.ToLower();
            
            // If the start state is valid, set the currentState variable to it.
            // Throw an exception if the start state is invalid.
            switch (startState)
            {
                case "open":
                case "out of hours":
                case "closed":
                    currentState = startState;
                    break;
                default:
                    throw new ArgumentException("Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'");
            }
        }

        /// <summary>
        /// Instantiates a BuildingController with the given ID and injects the given dependencies.
        /// </summary>
        /// <param name="id">The ID of the building being controlled.</param>
        /// <param name="lightManager">An implementation of ILightManager.</param>
        /// <param name="fireAlarmManager">An implementation of IFireAlarmManager.</param>
        /// <param name="doorManager">An implementation of IDoorManager.</param>
        /// <param name="webService">An implementation of IWebService.</param>
        /// <param name="emailService">An implementation of IEmailService.</param>
        /// <exception cref="ArgumentNullException">Thrown when the ID is null.</exception>
        /// <exception cref="ApplicationException">Thrown when the ID is an empty string or whitespace.</exception>
        public BuildingController(string id, ILightManager lightManager, IFireAlarmManager fireAlarmManager,
            IDoorManager doorManager, IWebService webService, IEmailService emailService) : this(id)
        {

            // Store the implementations of the dependencies
            this._lightManager = lightManager;
            this._doorManager = doorManager;
            this._fireAlarmManager = fireAlarmManager;
            this._webService = webService;
            this._emailService = emailService;
        }

        /// <summary>
        /// Gets the ID of the building.
        /// </summary>
        /// <returns>The value of the buildingID variable.</returns>
        // ReSharper disable once InconsistentNaming
        public string GetBuildingID()
        {
            return buildingID;
        }

        /// <summary>
        /// Sets the ID of the building.
        /// </summary>
        /// <param name="id">The new value of the buildingID variable.</param>
        // ReSharper disable once InconsistentNaming
        public void SetBuildingID(string id)
        {
            // The ID is null, throw an exception.
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Argument Null Exception: Building ID must be present.");
            }
            
            // The ID is an empty string/whitespace, throw an exception.
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Argument Exception: Building ID cannot be empty or whitespace.");
            }
            
            // Convert the ID to lower case and store it in buildingID.
            buildingID = id.ToLower();
        }
        
        /// <summary>
        /// Gets the current state of the building.
        /// </summary>
        /// <returns>The value of the currentState variable.</returns>
        public string GetCurrentState()
        {
            return currentState;
        }

        /// <summary>
        /// Sets the current state of the building.
        /// </summary>
        /// <param name="state">The new state for the building.</param>
        /// <returns>True if the state change was valid and successful, false if not.</returns>
        public bool SetCurrentState(string state)
        {
            // The state is null, throw an exception.
            if (state == null)
            {
                return false;
            }

            // The state is the same as the current, return true and
            // don't change anything.
            if (state == currentState)
            {
                return true;
            }
            
            // Determine whether the state change is acceptable
            // or not.
            bool validChange = false;
            
            switch (currentState)
            {
                case "open": // Open/closed can only change to out of hours
                case "closed": // and fire alarm/fire drill states.
                    if (state == "out of hours" || state == "fire alarm" || state == "fire drill")
                    {
                        validChange = true;
                    }
                    
                    break;
                case "out of hours": // All states are accessible from out of hours.
                    if (state == "open" || state == "closed" || state == "fire alarm" || state == "fire drill")
                    {
                        validChange = true;
                    }
                    
                    break;
                case "fire alarm": // Fire alarm/fire drill states can only change to
                case "fire drill": // the last known normal operation state.
                    if (state == _lastNormalState)
                    {
                        validChange = true;
                    }
                    
                    break;
            }

            // If it was determined that the state change isn't valid
            // return false and don't change anything.
            if (!validChange)
            {
                return false;
            }

            // The state can only become open once all doors have
            // opened successfully.
            if (state == "open")
            {
                if (!_doorManager.OpenAllDoors())
                {
                    return false;
                }
            }

            // All doors should be locked and all lights should be
            // turned off when the state is set to closed.
            if (state == "closed")
            {
                _doorManager.LockAllDoors();
                _lightManager.SetAllLights(false);
            }
            
            // If the new state isn't part of normal operation store
            // the last known state before changing.
            if (state.StartsWith("fire"))
            {
                _lastNormalState = currentState;

                // If the fire alarm state is set, the alarm should be
                // triggered, all doors should be opened and all lights
                // should be turned on.
                if (state == "fire alarm")
                {
                    _fireAlarmManager.SetAlarm(true);
                    _doorManager.OpenAllDoors();
                    _lightManager.SetAllLights(true);

                    // The fire alarm should be logged to the WebService,
                    // if the WebService returns an exception an email
                    // should be sent with EmailService.
                    try
                    {
                        _webService.LogFireAlarm("fire alarm");
                    }
                    catch (Exception exception)
                    {
                        _emailService.SendMail(
                            "smartbuilding@uclan.ac.uk", 
                            "failed to log alarm",
                            exception.Message
                        );
                    }
                }
            }
            
            // The state change is acceptable, store the new
            // state in currentState.
            currentState = state;

            return true;
        }

        /// <summary>
        /// Gets the status report of the building and reports faults if any.
        /// </summary>
        /// <returns>The status of each manager combined.</returns>
        public string GetStatusReport()
        {
            // Add the device managers into an array that we can
            // loop through easily.
            IManager[] deviceManagers = {_lightManager, _doorManager, _fireAlarmManager};

            // Start generating the status report and look for faults.
            string statusReport = "";
            string faultString = "";
            
            // Iterate over each manager and append their status
            // string to the report.
            foreach (IManager manager in deviceManagers)
            {
                // Split the manager's status string into a list
                // of comma seperated values.
                string[] statusList = manager.GetStatus().Split(',');
                
                // Extract the name of the manager from the status string.
                string managerName = statusList[0];
                
                // Iterate over the status string and check if there
                // is an occurence of "FAULT", if there is we should
                // add the name of the manager to the fault string.
                for (int i = 1; i < statusList.Length; i++)
                {
                    if (statusList[i] == "FAULT")
                    {
                        faultString += managerName + ",";
                        break;
                    }
                }
                
                statusReport += manager.GetStatus();
            }

            // If the fault string isn't empty we should report it
            // to the web service.
            if (faultString.Length > 0)
            {
                string[] faultList = faultString.Split(',');
                
                // If there is only one manager with the fault status
                // we should report it without a trailing comma.
                if (faultList.Length == 2)
                {
                    _webService.LogEngineerRequired(faultList[0]);
                }
                else
                {
                    _webService.LogEngineerRequired(faultString);
                }
            }

            return statusReport;
        }

        // ReSharper disable once InconsistentNaming
        private string buildingID;
        // ReSharper disable once InconsistentNaming
        private string currentState;

        private string _lastNormalState;

        private readonly ILightManager _lightManager;
        private readonly IDoorManager _doorManager;
        private readonly IFireAlarmManager _fireAlarmManager;
        private readonly IWebService _webService;
        private readonly IEmailService _emailService;
    }
}