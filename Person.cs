namespace Lift
{
    public class Person
    {
        public Person(int currentFloor, int destination)
        {
            CurrentFloor = currentFloor;
            Destination = destination;
        }
        public int CurrentFloor { get; set; }

        public int Destination { get; set; }
    }
}
