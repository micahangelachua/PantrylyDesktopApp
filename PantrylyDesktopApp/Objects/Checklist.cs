using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantrylyDesktopApp
{
    public class Checklist
    {
        public Guid Checklist_Id { get; set; }
        public string Checklist_Name { get; set; }
        public List<PantryItems> ChecklistItems { get; set; }
        public string CreatorEmail { get; set; }
        public Checklist(string name, string email) 
        {
            Checklist_Id = Guid.NewGuid();
            Checklist_Name = name;
            CreatorEmail = email;
        }
    }
}
