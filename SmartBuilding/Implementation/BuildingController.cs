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
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Argument Null Exception: BuildingController must be initialised with a valid ID.");
            }
            
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Argument Exception: BuildingController must be initialised with a valid ID.");
            }

            buildingID = id.ToLower();
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
            if (startState == null)
            {
                throw new ArgumentNullException(nameof(startState), "Argument Null Exception: BuildingController must be initialised with a valid startState.");
            }

            startState = startState.ToLower();
            
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
        public BuildingController(string id, ILightManager lightManager, IFireAlarmManager fireAlarmManager,
            IDoorManager doorManager, IWebService webService, IEmailService emailService) : this(id)
        {
            _deviceManagers = new IManager[]
            {
                lightManager, doorManager, fireAlarmManager
            };

            this.webService = webService;
            this.emailService = emailService;
        }

        /// <summary>
        /// Gets the ID of the building.
        /// </summary>
        /// <returns>The value of the buildingID variable.</returns>
        public string GetBuildingID()
        {
            return buildingID;
        }
        
        public void SetBuildingID(string id)
        {
            throw new NotImplementedException();
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

        private string buildingID;
        private string currentState;
        private IManager[] _deviceManagers;
        private IWebService webService;
        private IEmailService emailService;
    }
}