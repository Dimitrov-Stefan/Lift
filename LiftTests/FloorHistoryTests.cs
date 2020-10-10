using Lift;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LiftTests
{
    /// <summary>
    /// A test class for testing the floor history. 
    /// </summary>
    [TestClass]
    public class FloorHistoryTests
    {
        /// <summary>
        /// Tests whether the floor history is correct after all passengers are delivered. Should be true.
        /// </summary>
        [TestMethod]
        public void FloorHistory_Matches_True()
        {
            // Arrange
            var floorList = GetFloors();
            LiftConfig liftConfig = new LiftConfig();
            liftConfig.MaxFloors = 11;
            liftConfig.Floors = GetFloors();
            liftConfig.Capacity = 5;

            // Act
            Lift.Lift lift = new Lift.Lift(liftConfig);
            lift.Move(0);

            var floorHistory = string.Join("-", lift.GetFloorHistory());

            // Assert
            Assert.IsTrue(floorHistory == "1-2-4-5-6-0-10-9-4-3-2-1-7-6-5-4-3-9-10-2-0");
        }

        /// <summary>
        /// Tests whether the floor history is correct after all passengers are delivered. Should be false.
        /// </summary>
        [TestMethod]
        public void FloorHistory_Matches_False()
        {
            // Arrange
            var floorList = GetFloors();
            LiftConfig liftConfig = new LiftConfig();
            liftConfig.MaxFloors = 11;
            liftConfig.Floors = GetFloors();
            liftConfig.Capacity = 5;
            Lift.Lift lift = new Lift.Lift(liftConfig);

            // Act
            lift.Move(0);

            var floorHistory = string.Join("-", lift.GetFloorHistory());

            // Assert
            Assert.IsTrue(floorHistory != "1-2-4-5-6-0-10-9-4-3-2-1-7-6-5-4-3-9-10-2");
        }

        /// <summary>
        /// Checks whether there are passengers that didn't reach their destination. Should be false.
        /// </summary>
        [TestMethod]
        public void Passengers_NotArrivedAtDestination_False()
        {
            // Arrange
            var floorList = GetFloors();
            LiftConfig liftConfig = new LiftConfig();
            liftConfig.MaxFloors = 11;
            liftConfig.Floors = GetFloors();
            liftConfig.Capacity = 5;
            Lift.Lift lift = new Lift.Lift(liftConfig);

            // Act
            lift.Move(0);

            // Assert
            Assert.IsFalse(lift.GetFloors().Any(f=> f.Passengers.Any(p => p.CurrentFloor != p.Destination)));
        }

        /// <summary>
        /// Gets a list of floors as an input to the test.
        /// </summary>
        /// <returns>A list of floors.</returns>
        private static List<Floor> GetFloors()
        {
            return new List<Floor>(){
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
