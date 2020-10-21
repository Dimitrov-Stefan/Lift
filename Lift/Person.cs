using System.Runtime.Serialization;

namespace Lift
{
    /// <summary>
    /// The Person class.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes an instance of the Person class.
        /// </summary>
        /// <param name="currentFloor">The current floor the person is.</param>
        /// <param name="destination">The destination floor of the person.</param>
        /// <param name="isMechanic">Indicates whether the person is a mechanic.</param>
        public Person(int currentFloor, int destination, bool isMechanic = false)
        {
            CurrentFloor = currentFloor;
            Destination = destination;
            IsMechanic = isMechanic;
        }

        /// <summary>
        /// The current floor.
        /// </summary>
        public int CurrentFloor { get; set; }

        /// <summary>
        /// The destination floor.
        /// </summary>
        public int Destination { get; set; }

        /// <summary>
        /// Indicates whether the person is a mechanic.
        /// </summary>
        public bool IsMechanic { get; set; }
    }
}
