using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek
{
    public class SavedGame
    {


        public string PlayerLocation { get; set; }
        public Dictionary<string, string> OpponentNamesAndLocations { get; set; }
        public List<string> FoundOpponents { get; set; }
        public int MoveNumber { get; set; }
    }
}
