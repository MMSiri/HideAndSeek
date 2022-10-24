using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HideAndSeek
{
    public class House
    {
        public static Random Random = new Random();

        /// <summary>
        /// Returns players starting location
        /// </summary>
        public static readonly Location Entry;

        /// <summary>
        /// Private collection of locations in the house
        /// </summary>
        private static IEnumerable<Location> locations;

        /// <summary>
        /// Instantiates data structure of the house
        /// </summary>
        static House()
        {
            Entry = new Location("Entry");
            var hallway = new Location("Hallway");
            var livingRoom = new LocationWithHidingPlace("Living Room", "behind the sofa");
            var kitchen = new LocationWithHidingPlace("Kitchen", "next to the stove");
            var bathroom = new LocationWithHidingPlace("Bathroom", "behind the door");
            var landing = new Location("Landing");
            var masterBedroom = new LocationWithHidingPlace("Master Bedroom", "in the closet");
            var masterBath = new LocationWithHidingPlace("Master Bath", "in the bathtub");
            var secondBathroom = new LocationWithHidingPlace("Second Bathroom", "in the shower");
            var kidsRoom = new LocationWithHidingPlace("Kids Room", "under the bed");
            var nursery = new LocationWithHidingPlace("Nursery", "under the crib");
            var pantry = new LocationWithHidingPlace("Pantry", "inside a cabinet");
            var attic = new LocationWithHidingPlace("Attic", "in a trunk");
            var garage = new LocationWithHidingPlace("Garage", "behind the car");


            Entry.AddExit(Direction.East, hallway);
            Entry.AddExit(Direction.Out, garage);
            hallway.AddExit(Direction.Northwest, kitchen);
            hallway.AddExit(Direction.North, bathroom);
            hallway.AddExit(Direction.South, livingRoom);
            hallway.AddExit(Direction.Up, landing);
            landing.AddExit(Direction.Northwest, masterBedroom);
            landing.AddExit(Direction.West, secondBathroom);
            landing.AddExit(Direction.Southwest, nursery);
            landing.AddExit(Direction.South, pantry);
            landing.AddExit(Direction.Southeast, kidsRoom);
            landing.AddExit(Direction.Up, attic);
            masterBedroom.AddExit(Direction.East, masterBath);

            // Add all locations to the private locations collection
            locations = new List<Location>()
            {
                Entry,
                hallway,
                kitchen,
                bathroom,
                livingRoom,
                landing,
                masterBedroom,
                secondBathroom,
                kidsRoom,
                nursery,
                pantry,
                attic,
                garage,
                attic,
                masterBath,
            };
        }

        /// <summary>
        /// Gets location by name
        /// </summary>
        /// <param name="name">Name of the location to find</param>
        /// <returns>The location, or Entry if no location by that name was found</returns>
        public static Location GetLocationByName(string name)
        {
            var found = locations.Where(l => l.Name == name);
            return found.Count() > 0 ? found.First() : Entry;
        }

        /// <summary>
        /// Get a random exit from the specified location
        /// </summary>
        /// <param name="location">Location to get random exit from</param>
        /// <returns>A randomly selected exit from the location</returns>
        public static Location RandomExit(Location location) => GetLocationByName(location.Exits.OrderBy(exit => exit.Value.Name).Select(exit => exit.Value.Name).Skip(Random.Next(0, location.ExitList.Count())).First());

        /// <summary>
        /// Check each hiding place to ensure no opponents remain hiding in order to reset the house between rounds
        /// </summary>
        public static void ClearHidingPlaces()
        {
            foreach (var location in locations)
            {
                if (location is LocationWithHidingPlace hidingPlace) hidingPlace.CheckHidingPlace();
            }
        }
    }
}
