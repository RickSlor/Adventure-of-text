using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ZuulCS
{
	public class Game
	{
        private Parser parser;
        private Player player;

        public Game ()
		{
			parser = new Parser();
            player = new Player();

            //Knife
            Knife item1 = new Knife();

            item1.Name = "Knife";
            item1.Description = "A sharp and pointy object, used for stabbing.";
            //item1.Bad = true;
            //item1.Damage = 20;
            //item1.SetPlayer = this.player;

            Bandage item2 = new Bandage();
            item2.Name = "Bandage";
            item2.Description = "A crude bandage, not the best but it'll help.";

            Rifle item3 = new Rifle();
            item3.Name = "Rifle";
            item3.Description = "An old hunting rifle, probably still works.";

            Key item4 = new Key();
            item4.Name = "Key";
            item4.Description = "Use this key to open a door.";

            Mushroom item5 = new Mushroom();
            item5.Name = "Red_Mushroom";
            item5.Description = "A red mushroom.";

            // Spawner for item by adding them to a room (i) //
            for (int i
                = 0; i < player.CheckRoom.Count; i++)
            {
                player.CheckRoom[0].Item = item2;
                player.CheckRoom[3].Item = item4;
                player.CheckRoom[4].Item = item5;
                player.CheckRoom[1].Item = item1;
                player.CheckRoom[2].Item = item3;
				player.CheckRoom[5].Item = item2;
				player.CheckRoom[6].Item = item3;


				//Setting Locked Doors
				player.CheckRoom[4].isLocked = true;
            }
        }

        public Player GetPlayer
        {
            get { return this.player; }
        }

        public void play()
		{
			printWelcome();
            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            bool finished = false;
			while (! finished) {
				Command command = parser.getCommand();
				finished = processCommand(command);
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("You wake up next to a crashed plane.");
			Console.WriteLine("Try to look around and look if you can find anything usefull");
			Console.WriteLine("Type 'help' if you need help.");

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Type inventory to see all current items in your inventory.           ");
            Console.WriteLine("Type look to see all items in your current room.                     ");
            Console.WriteLine("Type add + ITEMNAMEHERE or just add, to add something to your inventory.         ");
            Console.WriteLine("Type remove + ITEMNAMEHERE, to destroy something from your inventory.");
            Console.ResetColor();
            Console.WriteLine();
			Console.WriteLine(player.currentRoom.getLongDescription());
        }

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
					printHelp();
					break;
				case "go":
					goRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
                case "look":
                    Look();
                    break;
                case "inventory":
                    CheckInv();
                    break;
                case "remove":
                    RemoveItem(command);
                    break;
                case "add":
                    AddItem();
                    break;
                case "use":
                    UseItem(command);
                    break;
            }

			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */

        private void CheckInv()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("- Items in Inventory -");
            Console.WriteLine();
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                if (player.Inventory[i].Amount > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Item Name: " + player.Inventory[i].Name);
                    Console.WriteLine("Item Amount: " + player.Inventory[i].Amount);
                    Console.WriteLine("Item Description: " + player.Inventory[i].Description);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            if(player.Inventory.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You don't have any items in your inventory");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void RemoveItem(Command command)
        {
            string itemCommand = command.getSecondWord();
            for(int i = 0; i < player.Inventory.Count; i++)
            {
                if(player.Inventory[i].Name == itemCommand && player.Inventory[i].Amount > 0)
                {
                    player.Inventory[i].SetAmount -= 1;
                    player.Inventory.Remove(player.Inventory[i]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You removed " + itemCommand + " from your inventory.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private void UseItem(Command command)
        {
            string itemCommand = command.getSecondWord();
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                if (player.Inventory[i].Name == itemCommand && player.Inventory[i].Amount > 0 && player.isAlive == true)
                {
                    Console.WriteLine("You used " + player.Inventory[i].Name);
                    player.Inventory[i].use(player);
                    player.Inventory.Remove(player.Inventory[i]);
                    break;
                }
            }
        }

        private void AddItem()
        {
            for (int i = 0; i < player.CheckRoom.Count; i++)
            {
                if (i == player.currentRoom.ID)
                {
                    if (player.CheckRoom[i].ItemAmount > 0 && player.CheckRoom[i].Item != null && player.CheckRoom[i].Item.Bad == false && player.isAlive == true)
                    {
                        Item item = player.CheckRoom[i].Item;
                        item.SetAmount = 1;
                        player.Inventory.Add(item);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine();
                        Console.WriteLine("Added " + player.CheckRoom[i].Item.Name + " to inventory.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        player.CheckRoom[i].ItemAmount -= 1;
                    }

                    if(player.CheckRoom[i].Item == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There are no items in this room.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if(player.CheckRoom[i].Item != null && player.CheckRoom[i].Item.Bad == true && player.isAlive == true)
                    {
                        player.CheckRoom[i].Item.ApplyDamage(player.CheckRoom[i].Item.Damage);
                        Console.WriteLine("Owh.... this item damaged you. ");
                        player.IsAlive();
                        player.CheckRoom[i].Item = null;
                    }

                    if(player.isAlive == false)
                    {
                        player.IsAlive();
                    }
                }
            }
        }

        private void printHelp()
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All commands are:");
            Console.ResetColor();
			parser.showCommands();
            
		}

        private void Look()
        {
            if (player.currentRoom.Item == null)
            {
                Console.WriteLine("There is nothing here...");
            }

            for (int i = 0; i < player.CheckRoom.Count; i++)
            {
                if(i == player.currentRoom.ID && player.currentRoom.ItemAmount > 0 && player.currentRoom.Item != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("You see an item!");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-- Item name --");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(player.CheckRoom[i].Item.Name);
                    Console.ResetColor();
                }
            }

            if(player.currentRoom.ItemAmount <= 0)
            {
                Console.WriteLine("You don't see any items in this room");
            }
        }
		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */

        private void GetDescription()
        {
            Console.WriteLine(player.currentRoom.getLongDescription());
        }
       
		private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.getSecondWord();

            // Try to leave current room.
            Console.ForegroundColor = ConsoleColor.Yellow;
			Room nextRoom = player.currentRoom.getExit(direction);
            Console.ResetColor();

            if(player.isAlive == true)
            {
                if (nextRoom == null)
                {
                    Console.WriteLine("There is no door to " + direction + "!");
                }
                else if(nextRoom.isLocked == false)
                {
                    Console.WriteLine();
                    player.currentRoom = nextRoom;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(player.currentRoom.getLongDescription());
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Room ID: " + player.currentRoom.ID);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
     
                    if(player.isBleeding == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You are bleeding from the crash...");
                        Console.WriteLine();
                        player.Damage(5);
                        Console.ForegroundColor = ConsoleColor.White;
                        player.IsAlive();
                    }
                    Console.WriteLine();
                }
                if (nextRoom != null && nextRoom.isLocked == true)
                {
                    Console.WriteLine("The door is locked...");
                }
            }
            else
            {
                Console.WriteLine("You can't move because you are dead, type quit.");
            }
		}

	}
}
