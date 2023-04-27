using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantrylyDesktopApp
{
    public class Pantry
    {
        public string Pantry_Id { get; set; }
        public string PantryName { get; set; }
        public List<Item> PantryItems { get; set; }
        public string PantryOwnerEmail { get; set; }
        
        public Pantry(string ownerEmail, string name)
        {
            PantryName = name;
            PantryOwnerEmail = ownerEmail;
        }
    }
}
