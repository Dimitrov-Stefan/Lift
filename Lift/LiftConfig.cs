using System;
using System.Collections.Generic;
using System.Text;

namespace Lift
{
    /// <summary>
    /// A class containing lift configuration data.
    /// </summary>
    public class LiftConfig
    {
        /// <summary>
        /// The number of floors the lift can travel to.
        /// </summary>
        public int MaxFloors { get; set; }

        /// <summary>
        /// A list pf all the floors that have passengers.
        /// </summary>
        public List<Floor> Floors = new List<Floor>();

        /// <summary>
        /// The number of people that can be in the lift at once.
        /// </summary>
        public int Capacity { get; set; }
    }
}
