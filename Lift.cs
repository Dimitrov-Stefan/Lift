using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lift
{
    public class Lift
    {
        public int MaxFloors { get; set; } = 11;

        public int Capacity { get; set; } = 5;

        public List<Person> Passengers { get; set; } = new List<Person>();

        public bool IsGoingUp { get; set; } = true;

        public int CurrentFloor { get; set; }

        public void CheckDirectionChange()
        {
            // (Moving UP && No passengers going UP && Has passengers going DOWN) || Current floor = max floor
            if ((IsGoingUp && !Passengers.Any(p => p.IsGoingUp) && Passengers.Any(p => !p.IsGoingUp)) || CurrentFloor == MaxFloors)
            {
                IsGoingUp = false;
            }
            // (Moving DOWN && Has passangers going UP && No passangers going DOWN) || Current floor = 0
            else if ((!IsGoingUp && Passengers.Any(p => p.IsGoingUp) && !Passengers.Any(p => !p.IsGoingUp)) || CurrentFloor == 0)
            {
                IsGoingUp = true;
            }
        }

        public void Move(int currentFloor, int counter = 0)
        {
            Console.WriteLine($"Moving to floor {currentFloor}.");

            DropPassengers(currentFloor);
            PickPassengers(currentFloor);

            CurrentFloor = currentFloor;
            Passengers.ForEach(p => p.CurrentFloor = currentFloor);

            CheckDirectionChange();

            if (IsGoingUp)
            {
                CurrentFloor++;
            }
            else
            {
                CurrentFloor--;
            }

            counter++;

            if (!Floors.Any(f => f.Passengers.Any(p => p.Destination != p.CurrentFloor)) || counter == 100)
            {
                return;
            }

            Move(CurrentFloor, counter);
        }

        public void ChangeDirection()
        {
            if (Passengers.Any(p => p.IsGoingUp))
            {

            }
        }

        public void DropPassengers(int floorNumber)
        {
            var floor = Floors.SingleOrDefault(f => f.FloorNumber == floorNumber);

            if (floor == null || floor.Passengers.Count == 0 || Passengers.Count == Capacity)
            {
                return;
            }

            var droppedPassangers = new List<Person>();

            foreach (var pass in Passengers)
            {
                if (pass.Destination == floorNumber)
                {
                    pass.CurrentFloor = CurrentFloor;
                    Console.WriteLine($"Dropping passenger.");
                    droppedPassangers.Add(pass);
                    floor.Passengers.Add(pass);
                }
            }

            Passengers = Passengers.Except(droppedPassangers).ToList();
        }

        public void PickPassengers(int floorNumber)
        {
            var floor = Floors.SingleOrDefault(f => f.FloorNumber == floorNumber);

            if (floor == null || floor.Passengers.Count == 0)
            {
                return;
            }

            var pickedUpPassangers = new List<Person>();

            for (int i = 0; i < floor.Passengers.Count; i++)
            {
                var pass = floor.Passengers[i];
                if (pass.Destination != floorNumber && Passengers.Count < Capacity)
                {
                    Passengers.Add(pass);
                    pickedUpPassangers.Add(pass);
                    Console.WriteLine($"Picking passanger from floor {CurrentFloor}");
                }
            }

            floor.Passengers = floor.Passengers.Except(pickedUpPassangers).ToList();
        }

        List<Floor> Floors = new List<Floor>(){
            new Floor(){
                FloorNumber = 0,
                Passengers = new List<Person>()
            },
            new Floor(){
                FloorNumber = 1,
                Passengers = new List<Person>() { new Person(6), new Person(5), new Person(2) }
            },
            new Floor()
            {
                FloorNumber = 2,
                Passengers = new List<Person>() { new Person(4) }
            },
            new Floor()
            {
                FloorNumber = 4,
                Passengers = new List<Person>() { new Person(0), new Person(0), new Person(0) }
            },
            new Floor()
            {
                FloorNumber = 7,
                Passengers = new List<Person>() { new Person(3), new Person(6), new Person(4), new Person(5), new Person(6) }
            },
            new Floor()
            {
                FloorNumber = 9,
                Passengers = new List<Person>() { new Person(1), new Person(10), new Person(2) }
            },
            new Floor()
            {
                FloorNumber = 10,
                Passengers = new List<Person>() { new Person(1), new Person(4), new Person(3), new Person(2) }
            }};
    }


}
