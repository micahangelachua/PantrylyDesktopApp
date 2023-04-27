using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantrylyDesktopApp
{
    public class PantryItems
    {
        public int PantryItem_Id {  get; set; }
        public string PantryItemName { get; set; }
        //public string PantryItemDescription { get; set; } 
        public int PantryItem_Quantity { get; set; }
        public Guid PantryItem_Pantry {  get; set; }
        
        //public float PantryItem_Price { get; set;}
    }
}
