using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Item
    {
        private string name;
        private string description;
        private bool usable;
        private float amount;
        private float room;
        private bool isBad;
        private Player player;
        private float damage;


        public virtual void use(Object o)
        {
            System.Console.WriteLine("Item::use(Object o)");
        }

        public virtual void use()
        {
            System.Console.WriteLine("Item::use()");
        }

        public float Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public bool Bad
        {
            get { return this.isBad; }
            set { this.isBad = value; }
        }

        public Player SetPlayer
        {
            get { return this.player; }
            set { this.player = value; }
        }

        public void ApplyDamage(float Damage)
        {
            player.Damage(Damage);
        }

        public string Description
        {
            get { return this.description;  }
            set { this.description = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool IsUsable
        {
            get { return this.usable; }
            set { this.usable = value; }
        }

        public float SetAmount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }

        public float Amount
        {
            get { return this.amount; }
        }

        public float Room
        {
            get { return this.room; }
            set { this.room = value; }
        }

    }
}
