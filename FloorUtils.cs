using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    /// <summary>
    /// A helper class for floor operations.
    /// </summary>
    public static class FloorUtils
    {
        /// <summary>
        /// Checks whether all passengers are delivered to their desired floors.
        /// </summary>
        /// <param name="floors">A list of floors to check.</param>
        /// <returns>True if all passengers are delivered, false otherwise.</returns>
        public static bool AreAllDelivered(this List<Floor> floors)
        {
            return !floors.Any(f => f.Passengers.Any(p => p.Destination != p.CurrentFloor));
        }

        /// <summary>
        /// Gets the lowest floor that has waiting passengers.
        /// </summary>
        /// <param name="floors">A list of floors to check.</param>
        /// <returns>The lowest floor that has waiting passengers.</returns>
        public static int GetMinFloorWaitingPerson(this List<Floor> floors)
        {
            return floors.SelectMany(f => f.Passengers)
                .Where(p => p.CurrentFloor != p.Destination)
                .Select(p => p.CurrentFloor)
                .Min();
        }

        /// <summary>
        /// Gets the highest floor that has waiting passengers.
        /// </summary>
        /// <param name="floors">A list of floors to check.</param>
        /// <returns>The highest floor that has waiting passengers.</returns>
        public static int GetMaxFloorWaitingPerson(this List<Floor> floors)
        {
            return floors.SelectMany(f => f.Passengers)
                .Where(p => p.CurrentFloor != p.Destination)
                .Select(p => p.CurrentFloor)
                .Max();
        }
    }
}
