using System;
using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class Lift
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Lift class.
        /// </summary>
        /// <param name="liftConfig"></param>
        public Lift(LiftConfig liftConfig)
        {
            MaxFloors = liftConfig.MaxFloors;
            Capacity = liftConfig.Capacity;
            Floors = liftConfig.Floors;
        }

        #endregion Constructors

        #region Private Members

        /// <summary>
        /// The number of floors the lift can travel to.
        /// </summary>
        private int MaxFloors { get; set; }

        /// <summary>
        /// A list pf all the floors that have passengers.
        /// </summary>
        private List<Floor> Floors { get; set; }

        /// <summary>
        /// A list of all visited floors.
        /// </summary>
        private List<int> FloorHistory { get; set; } = new List<int>();

        /// <summary>
        /// The number of people that can be in the lift at once.
        /// </summary>
        private int Capacity { get; set; }

        /// <summary>
        /// A list of passangers on the lift.
        /// </summary>
        private List<Person> Passengers { get; set; } = new List<Person>();

        /// <summary>
        /// The current floor the lift is on.
        /// </summary>
        private int CurrentFloor { get; set; }

        /// <summary>
        /// The previous floor the lift was on.
        /// </summary>
        private int PreviousFloor { get; set; }

        /// <summary>
        /// A property indicating whether the lift is full.
        /// </summary>
        /// <returns>True if full, false otherwise.</returns>
        private bool IsFull()
        {
            return Passengers.Count == Capacity;
        }

        /// <summary>
        /// A property indicating whether the lift is empty.
        /// </summary>
        /// <returns>True if empty, false otherwise.</returns>
        private bool IsEmpty()
        {
            return Passengers.Count == 0;
        }

        /// <summary>
        /// Moves the passengers from the lift to the floor.
        /// </summary>
        /// <param name="leavingPassengers">A list of passengers that get off the lift.</param>
        private void LeaveLift(List<Person> leavingPassengers)
        {
            Passengers = Passengers.Except(leavingPassengers).ToList();
        }

        /// <summary>
        /// Gets a floor by floor number.
        /// </summary>
        /// <param name="floorNumber"></param>
        /// <returns>Floor</returns>
        private Floor GetFloor(int floorNumber)
        {
            return Floors[floorNumber];
        }

        /// <summary>
        /// The next floor the lift will move to.
        /// </summary>
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

        /// <summary>
        /// Drops passangers that want to get off at the given floor.
        /// </summary>
        /// <param name="floorNumber">The number of the floor.</param>
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

            if (droppedPassangers.Any())
            {
                LeaveLift(droppedPassangers);
                FloorHistory.TryAddHistoryRecord(floorNumber);
            }
        }

        /// <summary>
        /// Picks passengers from the given floor.
        /// </summary>
        /// <param name="floorNumber">The number of the floor.</param>
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

            if (pickedUpPassangers.Any())
            {
                floor.LeaveFloor(pickedUpPassangers);
                FloorHistory.TryAddHistoryRecord(floorNumber);
            }
        }

        /// <summary>
        /// Prints status data on the screen.
        /// </summary>
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

        #endregion Private Members

        #region Public Members


        /// <summary>
        /// Moves the lift up and down.
        /// </summary>
        /// <param name="currentFloor">The current floor of the lift.</param>
        public void Move(int currentFloor)
        {
            PreviousFloor = CurrentFloor;
            CurrentFloor = currentFloor;
            int nextFloor;

            DropPassengers(currentFloor);

            if (IsEmpty() && Floors.AreAllDelivered())
            {
                if (currentFloor > 0)
                {
                    currentFloor--;
                    nextFloor = currentFloor;
                    Move(nextFloor);
                }

                FloorHistory.TryAddHistoryRecord(0);
                PrintStatus();
                return;
            }

            PickPassengers(currentFloor);

            nextFloor = NextFloor();

            PrintStatus();

            Move(nextFloor);
        }

        /// <summary>
        /// Gets a list of all visited floors. 
        /// </summary>
        /// <returns>A list of all visited floors.</returns>
        public List<int> GetFloorHistory() => FloorHistory;

        #endregion Public Members
    }
}
