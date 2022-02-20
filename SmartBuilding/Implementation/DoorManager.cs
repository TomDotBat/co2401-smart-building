namespace SmartBuilding.Implementation
{
    public class DoorManager : Manager<IDoor>, IDoorManager
    {
        /// <summary>
        /// Instantiates a DoorManager and sets the manager type.
        /// </summary>
        public DoorManager() : base("Doors") { }
        
        /// <summary>
        /// Opens a managed door by its ID.
        /// </summary>
        /// <param name="doorID">The ID of the door to open.</param>
        /// <returns>True if the door opened successfully, false if not.</returns>
        public bool OpenDoor(int doorID)
        {
            IDevice device = _devices[doorID];
            
            // If there is no door with the given ID, return false.
            if (device == null)
            {
                return false;
            }
            
            // Attempt to open the door and return the result.
            return ((IDoor) device).Open();
        }

        /// <summary>
        /// Locks a managed door by its ID.
        /// </summary>
        /// <param name="doorID">The ID of the door to lock.</param>
        /// <returns>True if the door locked successfully, false if not.</returns>
        public bool LockDoor(int doorID)
        {
            IDevice device = _devices[doorID];
            
            // If there is no door with the given ID, return false.
            if (device == null)
            {
                return false;
            }
            
            // Attempt to lock the door and return the result.
            return ((IDoor) device).Lock();
        }

        /// <summary>
        /// Opens every managed door.
        /// </summary>
        /// <returns>True if all doors opened successfully, false if one or more failed.</returns>
        public bool OpenAllDoors()
        {
            bool success = true;

            // Attempt to open every door, if Open() returned by any door
            // it should be returned by this method.
            
            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (IDoor door in _devices)
            {
                if (!door.Open())
                {
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Locks every managed door.
        /// </summary>
        /// <returns>True if all doors locked successfully, false if one or more failed.</returns>
        public bool LockAllDoors()
        {
            bool success = true;
            
            // Attempt to lock every door, if Lock() returned by any door
            // it should be returned by this method.
            
            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (IDoor door in _devices)
            {
                if (!door.Lock())
                {
                    success = false;
                }
            }

            return success;
        }
    }
}