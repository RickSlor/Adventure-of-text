using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    public class Inventory
    {

        private List<Item> inventoryList = new List<Item>();

        public List<Item> GetInventory
        {
            get { return this.inventoryList; }
            set { this.inventoryList = value; }
        }
    }
}
