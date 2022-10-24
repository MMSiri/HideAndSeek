using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HideAndSeek
{
    public class Location
    {
        /// <summary>
        /// The name of this location
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The exits out of this location
        /// </summary>
        public IDictionary<Direction, Location> Exits { get; private set; } = new Dictionary<Direction, Location>();

        /// <summary>
        /// Constructor setting the location name
        /// </summary>
        /// <param name="name">Name of the location</param>
        public Location(string name) => Name = name;

        public override string ToString() => Name;

        /// <summary>
        /// Returns sequence of descriptions of exits, sorted by direction
        /// </summary>
        public IEnumerable<string> ExitList => Exits.OrderBy(keyValuePair => 
        (int)keyValuePair.Key).OrderBy(keyValuePair => Math.Abs((int)keyValuePair.Key)).Select(keyValuePair => $"the {keyValuePair.Value} is {DescribeDirection(keyValuePair.Key)}");

        /// <summary>
        /// Adds an exit to this location
        /// </summary>
        /// <param name="direction">Direction of the connecting location</param>
        /// <param name="connectingLocation">Connecting location to add</param>
        public void AddExit(Direction direction, Location connectingLocation)
        {
            Exits.Add(direction, connectingLocation);
            connectingLocation.AddReturnExit(direction, this);
        }

        /// <summary>
        /// Adds return exit to a connecting location
        /// </summary>
        /// <param name="direction">Direction of the connecting location</param>
        /// <param name="connectingLocation">Location to add the return exit</param>
        private void AddReturnExit(Direction direction, Location connectingLocation) => Exits.Add((Direction)(-(int) direction), connectingLocation);

        /// <summary>
        /// Gets the exit location in a direction
        /// </summary>
        /// <param name="direction">Direction of the exit location</param>
        /// <returns>The exit location, or this if there is no exit in that direction</returns>
        public Location GetExit(Direction direction) => Exits.ContainsKey(direction) ? Exits[direction] : this;

        /// <summary>
        /// Decribes a direction (e.g. "in" vs. "to the North")
        /// </summary>
        /// <param name="d">Direction to describe</param>
        /// <returns>string describing the direction</returns>
        private string DescribeDirection(Direction d) => d switch
        {
            Direction.Up => "Up",
            Direction.Down => "Down",
            Direction.In => "In",
            Direction.Out => "Out",
            _ => $"to the {d}",
        };
    }
}
