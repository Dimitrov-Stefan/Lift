using System;
using System.Collections.Generic;
using System.Text;

namespace Lift
{
    public class Lift
    {
        public bool IsGoingUp { get; set; } = true;

        public int CurrentFloor { get; set; }

        public void Move()
        {
            if (IsGoingUp && CurrentFloor < 10)
            {
                CurrentFloor++;
            }
            else
            {
                CurrentFloor--;
            }

            DropPassengers();
        }

        public void DropPassengers()
        {

        }

        List<Floor> Floors = new List<Floor>(){
            new Floor(){
                FloorNumber = 1,
                Passengers = new Queue<Person>(new List<Person> { new Person(6), new Person(5), new Person(2) }) },
            new Floor(){
                FloorNumber = 2,
                Passengers = new Queue<Person>(new List<Person> { new Person(4) }) },
            new Floor(){
                FloorNumber = 4,
                Passengers = new Queue<Person>(new List<Person> { new Person(0), new Person(0), new Person(0)  }) },
            new Floor(){
                FloorNumber = 7,
                Passengers = new Queue<Person>(new List<Person> { new Person(3), new Person(6), new Person(4), new Person(5), new Person(6)  }) },
            new Floor(){
                FloorNumber = 9,
                Passengers = new Queue<Person>(new List<Person> { new Person(1), new Person(10), new Person(2)  }) },
            new Floor(){
                FloorNumber = 10,
                Passengers = new Queue<Person>(new List<Person> { new Person(1), new Person(4), new Person(3), new Person(2)  }) },

            };
    }


}
