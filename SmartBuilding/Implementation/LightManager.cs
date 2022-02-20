namespace SmartBuilding.Implementation
{
    public class LightManager : Manager<ILight>, ILightManager
    {
        /// <summary>
        /// Instantiates a LightManager and sets the manager type.
        /// </summary>
        public LightManager() : base("Lights") { }
        
        /// <summary>
        /// Sets the on state of a managed light by its ID.
        /// </summary>
        /// <param name="isOn">True if the light should be on, false if not.</param>
        /// <param name="lightID">The ID of the light to change the state of.</param>
        public void SetLight(bool isOn, int lightID)
        {
            IDevice device = _devices[lightID];
            
            // If there is no door with the given ID, return false.
            if (device == null)
            {
                return;
            }
            
            // Set the on state of the light to the given value.
            ((ILight) device).SetOn(isOn);
        }

        /// <summary>
        /// Sets the on state of every managed light.
        /// </summary>
        /// <param name="isOn">True if the lights should be on, false if not.</param>
        public void SetAllLights(bool isOn)
        {
            // Iterate over the list of lights, setting the state to the given value.
            
            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (ILight light in _devices)
            {
                light.SetOn(isOn);
            }
        }
    }
}