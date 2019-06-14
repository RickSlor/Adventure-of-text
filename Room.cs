using System.Collections.Generic;

namespace ZuulCS
{
	public class Room
	{
		private string description;
		private Dictionary<string, Room> exits; // stores exits of this room.
        private int roomID;
        private Item roomItem = null;
        private float roomItemAmount = 1;
        private bool locked = false;

        public bool isLocked
        {
            get { return this.locked; }
            set { this.locked = value; }
        }

		public Room(string description)
		{
			this.description = description;
			exits = new Dictionary<string, Room>();
		}
        public Item Item
        {
            get { return this.roomItem; }
            set { this.roomItem = value; }
        }

        public float ItemAmount
        {
            get { return this.roomItemAmount; }
            set { this.roomItemAmount = value; }
        }

        public int ID
        {
            get { return this.roomID; }
            set { this.roomID = value; }
        }
		/**
	     * Define an exit from this room.
	     */
		public void setExit(string direction, Room neighbor)
		{
			exits[direction] = neighbor;
		}

		/**
	     * Return the description of the room (the one that was defined in the
	     * constructor).
	     */
		public string getShortDescription()
		{
			return description;
		}

		/**
	     * Return a long description of this room, in the form:
	     *     You are in the kitchen.
	     *     Exits: north west
	     */
		public string getLongDescription()
		{
			string returnstring = "You are ";
			returnstring += description;
			returnstring += ".\n";
			returnstring += getExitstring();
			return returnstring;
		}

		/**
	     * Return a string describing the room's exits, for example
	     * "Exits: north, west".
	     */
		private string getExitstring()
		{
			string returnstring = "Exits:";

			// because `exits` is a Dictionary, we can't use a `for` loop
			int commas = 0;
			foreach (string key in exits.Keys) {
				if (commas != 0 && commas != exits.Count) {
					returnstring += ",";
				}
				commas++;
				returnstring += " " + key;
			}
			return returnstring;
		}

		/**
	     * Return the room that is reached if we go from this room in direction
	     * "direction". If there is no room in that direction, return null.
	     */
		public Room getExit(string direction)
		{
			if (exits.ContainsKey(direction)) {
				return (Room)exits[direction];
			} else {
				return null;
			}

		}
	}
}
