using System;
using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class Lift
    {
        public Lift(LiftConfig liftConfig)
        {
            MaxFloors = liftConfig.MaxFloors;
            Capacity = liftConfig.Capacity;
            Floors = liftConfig.Floors;
        }

        private int MaxFloors { get; set; }

        private List<Floor> Floors { get; set; }

        private int Capacity { get; set; }

        private List<Person> Passengers { get; set; } = new List<Person>();

        private int CurrentFloor { get; set; }

        private int PreviousFloor { get; set; }

        private bool IsFull()
        {
            return Passengers.Count == Capacity;
        }

        private bool IsEmpty()
        {
            return Passengers.Count == 0;
        }

        private void LeaveLift(List<Person> leavingPassengers)
        {
            Passengers = Passengers.Except(leavingPassengers).ToList();
        }

        private Floor GetFloor(int floorNumber)
        {
            return Floors[floorNumber];
        }

        private int NextFloor()
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
                if (PreviousFloor > CurrentFloor)
                {
                    return Floors.GetMinFloorWaitingPerson();
                }
                else if (CurrentFloor > PreviousFloor)
                {
                    return Floors.GetMaxFloorWaitingPerson();
                }

            }

            return 0;
        }

        public void Move(int currentFloor)
        {
            PreviousFloor = CurrentFloor;
            CurrentFloor = currentFloor;

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

        private void DropPassengers(int floorNumber)
        {
            if (IsEmpty())
            {
                return;
            }

            var floor = GetFloor(floorNumber);

            var droppedPassangers = new List<Person>();

            foreach (var passenger in Passengers)
            {
                if (passenger.Destination == floorNumber)
                {
                    passenger.CurrentFloor = floorNumber;
                    floor.Passengers.Add(passenger);
                    droppedPassangers.Add(passenger);
                }
            }

            LeaveLift(droppedPassangers);
        }

        private void PickPassengers(int floorNumber)
        {
            var floor = GetFloor(floorNumber);

            if (floor.IsEmpty())
            {
                return;
            }

            var pickedUpPassangers = new List<Person>();

            foreach (var passenger in floor.Passengers)
            {
                if (passenger.Destination != floorNumber && !IsFull())
                {
                    Passengers.Add(passenger);
                    pickedUpPassangers.Add(passenger);
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

            Console.WriteLine($"Lift: {string.Join(", ", Passengers.Select(p => p.Destination).ToList()) }");
            Console.WriteLine($"Previous floor: {PreviousFloor}");
            Console.WriteLine($"Current floor: {CurrentFloor}");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
    }


}
