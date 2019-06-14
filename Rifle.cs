using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Rifle : Item
    {
        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Console.WriteLine();
                Player p = (Player)o; // must cast
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Gun Blew up. you were hurt. What were you shooting at anyway?");
                Console.WriteLine();
                p.Damage(80);
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
