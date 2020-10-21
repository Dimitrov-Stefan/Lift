using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    /// <summary>
    /// The Floor class.
    /// </summary>
    public class Floor
    {
        /// <summary>
        /// THe number of the floor.
        /// </summary>
        public int FloorNumber { get; set; }

        /// <summary>
        /// The passerngers waiting on the floor.
        /// </summary>
        public List<Person> Passengers { get; set; }

        /// <summary>
        /// Moves the passengers from the floor to the lift.
        /// </summary>
        /// <param name="leavingPassengers">The list of passangers that will board the lift.</param>
        public void LeaveFloor(List<Person> leavingPassengers)
        {
            Passengers = Passengers.Except(leavingPassengers).ToList();
        }

        /// <summary>
        /// Checks whether the floor is empty.
        /// </summary>
        /// <returns>True if empty, otherwise false.</returns>
        public bool IsEmpty()
        {
            return Passengers.Count == 0;
        }

        /// <summary>
        /// Checks whether the floor has waiting passengers on it.
        /// </summary>
        /// <returns>True if there are waiting mechanics on the floor, otherwise false.</returns>
        public bool HasWaitingMechanics()
        {
            return Passengers.Any(p => p.IsMechanic && p.CurrentFloor != p.Destination);
        }
    }
}
