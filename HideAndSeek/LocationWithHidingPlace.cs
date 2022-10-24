using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek
{
    public class LocationWithHidingPlace : Location
    {
        /// <summary>
        /// Name of the hiding place in this location
        /// </summary>
        public readonly string HidingPlace;

        /// <summary>
        /// Opponents hidden in this location's hiding place
        /// </summary>
        private List<Opponent> hiddenOpponents = new List<Opponent>();

        /// <summary>
        /// Constructor setting the location name and hiding place name
        /// </summary>
        public LocationWithHidingPlace(string name, string hidingPlace) : base(name) => HidingPlace = hidingPlace;

        /// <summary>
        /// Hides opponent in the hiding place
        /// </summary>
        /// <param name="opponent">Opponent to hide</param>
        public void Hide(Opponent opponent) => hiddenOpponents.Add(opponent);


        public IEnumerable<Opponent> CheckHidingPlace()
        {
            var foundOpponents = new List<Opponent>(hiddenOpponents);
            hiddenOpponents.Clear();
            return foundOpponents;
        }
    }
}
