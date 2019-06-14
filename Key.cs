using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Key : Item
    {
        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Console.WriteLine();
                Player p = (Player)o; // must cast
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You opened the door");
                if(p.currentRoom.ID == 3)
                {
                    p.CheckRoom[4].isLocked = false;
                }
                Console.ResetColor();
            }
            else
            {
                // Object o is not a Person
                System.Console.WriteLine("Can't use a knife on this Object");
            }
        }

        public override void use()
        {
            System.Console.WriteLine("Item::use()");
        }
    }
}
