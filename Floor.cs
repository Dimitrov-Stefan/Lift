using System;
using System.Collections.Generic;
using System.Text;

namespace Lift
{
    public class Floor
    {
        public int FloorNumber { get; set; }
        public Queue<Person> Passengers { get; set; }
    }
}
