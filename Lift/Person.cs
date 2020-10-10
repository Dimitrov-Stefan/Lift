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
        public Person(int currentFloor, int destination)
        {
            CurrentFloor = currentFloor;
            Destination = destination;
        }

        /// <summary>
        /// The current floor.
        /// </summary>
        public int CurrentFloor { get; set; }

        /// <summary>
        /// The destination floor
        /// </summary>
        public int Destination { get; set; }
    }
}
