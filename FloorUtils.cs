using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public static class FloorUtils
    {
        public static bool AreAllDelivered(this List<Floor> floors)
        {
            return !floors.Any(f => f.Passengers.Any(p => p.Destination != p.CurrentFloor));
        }

        public static int GetMinFloorWaitingPerson(this List<Floor> floors)
        {
            return floors.SelectMany(f => f.Passengers)
                .Where(p => p.CurrentFloor != p.Destination)
                .Select(p => p.CurrentFloor)
                .Min();
        }

        public static int GetMaxFloorWaitingPerson(this List<Floor> floors)
        {
            return floors.SelectMany(f => f.Passengers)
                .Where(p => p.CurrentFloor != p.Destination)
                .Select(p => p.CurrentFloor)
                .Max();
        }
    }
}
