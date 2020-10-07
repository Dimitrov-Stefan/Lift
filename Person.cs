using System;
using System.Collections.Generic;
using System.Text;

namespace Lift
{
    public class Person
    {
        public Person(int destination)
        {
            Destination = destination;
        }
        public int CurrentFloor { get; set; }

        public int Destination { get; set; }

        public bool IsGoingUp { get { return CurrentFloor < Destination; } }
    }
}
