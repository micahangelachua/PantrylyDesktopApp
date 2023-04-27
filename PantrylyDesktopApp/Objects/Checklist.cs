using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantrylyDesktopApp.Objects
{
    public class Checklist
    {
        public string Checklist_Id { get; set; }
        public string Checklist_Name { get; set; }
        public List<Item> ChecklistItems { get; set; }
        public string CreatorEmail { get; set; }
        public Checklist() { }
    }
}
