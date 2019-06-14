using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Mushroom : Item
    {

        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Console.WriteLine();
                Player p = (Player)o; // must cast
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You ate the mushroom.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The mushroom was poisenous!");
                p.Damage(20);
                Console.ResetColor();
            }
            else
            {
                // Object o is not a Person
                System.Console.WriteLine("You can't eat the mushroooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
            }
        }

        public override void use()
        {
            System.Console.WriteLine("Item::use()");
        }
    }
}
