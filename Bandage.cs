using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Bandage : Item
    {
        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Console.WriteLine();
                Player p = (Player)o; // must cast
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You used a bandage to heal yourself.");
                Console.WriteLine("You aren't bleeding anymore.");
                Console.WriteLine();
                p.isBleeding = false;
                p.Heal(30);
                p.IsAlive();
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
