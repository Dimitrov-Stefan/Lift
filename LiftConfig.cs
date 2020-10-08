using System;
using System.Collections.Generic;
using System.Text;

namespace Lift
{
    public class LiftConfig
    {
        public int MaxFloors { get; set; } = 11;

        public int Capacity { get; set; } = 5;

        public List<Floor> Floors = new List<Floor>();
    }
}
