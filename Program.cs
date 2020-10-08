using System;
using System.Collections.Generic;

namespace Lift
{
    class Program
    {
        static void Main(string[] args)
        {
            LiftConfig liftConfig = new LiftConfig();
            liftConfig.MaxFloors = 11;
            liftConfig.Floors = GetFloors();
            liftConfig.Capacity = 5;


            Lift lift = new Lift(liftConfig);
            lift.Move(0);
        }

        private static List<Floor> GetFloors()
        {
            return  new List<Floor>(){
            new Floor(){
                FloorNumber = 0,
                Passengers = new List<Person>()
            },
            new Floor(){
                FloorNumber = 1,
                Passengers = new List<Person>() { new Person(1, 6), new Person(1, 5), new Person(1, 2) }
            },
            new Floor()
            {
                FloorNumber = 2,
                Passengers = new List<Person>() { new Person(2, 4) }
            },
            new Floor()
            {
                FloorNumber = 3,
                Passengers = new List<Person>()
            },
            new Floor()
            {
                FloorNumber = 4,
                Passengers = new List<Person>() { new Person(4 ,0), new Person(4, 0), new Person(4, 0) }
            },
            new Floor()
            {
                FloorNumber = 5,
                Passengers = new List<Person>()
            },
            new Floor()
            {
                FloorNumber = 6,
                Passengers = new List<Person>()
            },
            new Floor()
            {
                FloorNumber = 7,
                Passengers = new List<Person>() { new Person(7, 3), new Person(7, 6), new Person(7, 4), new Person(7, 5), new Person(7, 6) }
            },
            new Floor()
            {
                FloorNumber = 8,
                Passengers = new List<Person>()
            },
            new Floor()
            {
                FloorNumber = 9,
                Passengers = new List<Person>() { new Person(9, 1), new Person(9, 10), new Person(9, 2) }
            },
            new Floor()
            {
                FloorNumber = 10,
                Passengers = new List<Person>() { new Person(10, 1), new Person(10, 4), new Person(10, 3), new Person(10, 2) }
            }};
        }
    }
}
