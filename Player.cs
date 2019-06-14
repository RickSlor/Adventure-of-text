using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ZuulCS;

public class Player
{
    public Room currentRoom;
    private Inventory inventory;

    private float health = 100.0f;
    public bool isAlive = true;
    private bool Bleed = true;

    private List<Room> rooms = new List<Room>();

    public Player()
    {
        createRooms();
        inventory = new Inventory();
    }

    public bool isBleeding
    {
        get { return this.Bleed; }
        set { this.Bleed = value; }
    }
    public List<Item> Inventory
    {
        get { return this.inventory.GetInventory; }
        set { this.inventory.GetInventory = value; }
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
    }

    public List<Room> CheckRoom
    {
        get { return this.rooms; }
    }

    public void IsAlive()
    {
        if(health == 0 || health < 0)
        {
            //Player Died;
            Console.WriteLine("The player has died!");
            isAlive = false;
        }
        if (health > 0)
        {
            //Player Alive;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("The player is alive!");
            Console.WriteLine("Health left: " + health);
            Console.ResetColor();
        }
        if (health > 100)
        {
            //Player has too much health;
            health--;
        }
    }


    private void createRooms()
    {
        Room room0, room1, room2, room3, room4, room5, room6;

        // create the rooms
        room0 = new Room("next to a crashed plane.");
        room1 = new Room("inside a cave. you can't see all that well.");
        room2 = new Room("outside in a forest. it's freezing.");
        room3 = new Room("in a old cabin. It's pretty warm");
        room4 = new Room("second floor of the cabin. you see a bed and some chairs.");
        room5 = new Room("above the plane. you don't see any buildings in the distance.");
        room6 = new Room("inside the plane. some passengers didn't make it.");

        // initialise room exits
        room6.setExit("up", room0);
        room5.setExit("down", room0);

        room0.setExit("east", room1);
        room0.setExit("south", room3);
        room0.setExit("west", room2);
        room0.setExit("up", room5);
        room0.setExit("down", room6);

        room1.setExit("west", room0);

        room2.setExit("east", room0);

        room3.setExit("north", room0);
        room3.setExit("east", room4);

        room4.setExit("west", room3);

        rooms.Add(room0);
        rooms.Add(room1);
        rooms.Add(room2);
        rooms.Add(room3);
        rooms.Add(room4);
        rooms.Add(room5);
        rooms.Add(room6);

        for(int i = 0; i < rooms.Count; i++)
        {
            rooms[i].ID = i;
        }

        currentRoom = room0;  // start game room0
    }
}
