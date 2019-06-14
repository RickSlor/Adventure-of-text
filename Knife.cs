using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Knife : Item
    {
        public Knife()
        {

        }

        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Console.WriteLine();
                Player p = (Player)o; // must cast
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The knife's handle broke and you cut yourself.");
                Console.WriteLine();
                p.Damage(60);
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
