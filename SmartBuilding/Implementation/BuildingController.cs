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
        public string GetBuildingID()
        {
            return buildingID;
        }

        /// <summary>
        /// Sets the ID of the building.
        /// </summary>
        /// <param name="id">The new value of the buildingID variable.</param>
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

        public bool SetCurrentState(string state)
        {
            throw new NotImplementedException();
        }

        public string GetStatusReport()
        {
            throw new NotImplementedException();
        }

        // ReSharper disable once InconsistentNaming
        private string buildingID;
        private string currentState;

        private string _lastNormalState;

        private ILightManager _lightManager;
        private IDoorManager _doorManager;
        private IFireAlarmManager _fireAlarmManager;
        private IWebService _webService;
        private IEmailService _emailService;
    }
}