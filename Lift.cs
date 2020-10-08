using System;
using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class Lift
    {
        public int MaxFloors { get; set; } = 11;

        public int Capacity { get; set; } = 5;

        public List<Person> Passengers { get; set; } = new List<Person>();

        public int CurrentFloor { get; set; }

        public int PreviousFloor { get; set; }

        public int Counter { get; set; } = 0;
        public bool IsFull()
        {
            return Passengers.Count == Capacity;
        }

        public bool IsEmpty()
        {
            return Passengers.Count == 0;
        }

        public void LeaveFloor(List<Person> leavingPassangers)
        {
            Passengers = Passengers.Except(leavingPassangers).ToList();
        }

        public Floor GetFloor(int floorNumber)
        {
            return Floors[floorNumber];
        }

        public int NextFloor()
        {
            if (CurrentFloor == (MaxFloors - 1))
            {             
                return CurrentFloor - 1;
            }

            if (CurrentFloor == 0)
            {              
                return CurrentFloor + 1;
            }

            if (!IsEmpty())
            {
                if (Passengers.Select(i => i.Destination).Max() > CurrentFloor)
                {
                    return CurrentFloor + 1;
                }
                else if (Passengers.Select(i => i.Destination).Min() < CurrentFloor)
                {
                    return CurrentFloor - 1;
                }
            }
            else
            {
                if (Floors.AreAllDelivered())
                {
                    return 0;
                }
                else
                {                      
                    if ((PreviousFloor > CurrentFloor))
                    {
                        return Floors.SelectMany(f => f.Passengers)
                                               .Where(p => p.CurrentFloor != p.Destination)
                                               .Select(p => p.CurrentFloor)
                                               .Min();                                    
                    } 
                    else if (CurrentFloor > PreviousFloor)
                    {
                        return Floors.SelectMany(f => f.Passengers)
                                               .Where(p => p.CurrentFloor != p.Destination)
                                               .Select(p => p.CurrentFloor).Max();
                        
                    }
                }
            }

            return 0;
        }

        public void Move(int currentFloor)
        {
            PreviousFloor = CurrentFloor;
            CurrentFloor = currentFloor;
            Counter++;

            DropPassengers(currentFloor);

            if (IsEmpty() && Floors.AreAllDelivered())
            {
                PrintStatus();
                return;
            }

            PickPassengers(currentFloor);

            var nextFloor = NextFloor();

            PrintStatus();

            Move(nextFloor);
        }

        public void DropPassengers(int floorNumber)
        {
            var floor = GetFloor(floorNumber);

            var droppedPassangers = new List<Person>();

            foreach (var passanger in Passengers)
            {
                if (passanger.Destination == floorNumber)
                {
                    passanger.CurrentFloor = floorNumber;
                    floor.Passengers.Add(passanger);
                    droppedPassangers.Add(passanger);                  
                }
            }

            LeaveFloor(droppedPassangers);
        }

        public void PickPassengers(int floorNumber)
        {
            var floor = GetFloor(floorNumber);
            
            if (floor.IsEmpty())
            {
                return;
            }

            var pickedUpPassangers = new List<Person>();

            foreach(var passanger in floor.Passengers)
            {
                if (passanger.Destination != floorNumber && !IsFull())
                {
                    Passengers.Add(passanger);
                    pickedUpPassangers.Add(passanger);
                }
            }

            floor.LeaveFloor(pickedUpPassangers);
        }

        private void PrintStatus()
        {
            int i = 0;
            foreach (var floor in Floors)
            {
                Console.WriteLine($"Floor {i}: {string.Join(", ", floor.Passengers.Select(p => p.Destination).ToList()) }");
                Console.WriteLine(" ");

                i++;
            }

            Console.WriteLine($"Elevator: {string.Join(", ", Passengers.Select(p => p.Destination).ToList()) }");
            Console.WriteLine($"Previous floor: {PreviousFloor}");
            Console.WriteLine($"Current floor: {CurrentFloor}");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        List<Floor> Floors = new List<Floor>(){
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
