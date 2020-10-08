using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class Floor
    {
        public int FloorNumber { get; set; }
        public List<Person> Passengers { get; set; }

        public void LeaveFloor(List<Person> leavingPassangers)
        {
            Passengers = Passengers.Except(leavingPassangers).ToList();
        }

        public bool IsEmpty()
        {
            return Passengers.Count == 0;
        }
    }
}
