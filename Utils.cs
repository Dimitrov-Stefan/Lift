using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public static class Utils
    {
        public static bool AreAllDelivered(this List<Floor> floors)
        {
            return !floors.Any(f => f.Passengers.Any(p => p.Destination != p.CurrentFloor));
        }
    }
}
