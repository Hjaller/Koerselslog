using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koerselslog.Utils
{
    internal class Assignments
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string LicensePlate { get; set; }
        public string Assignment { get; set; }
        public string Date { get; set; }

        public Assignments(int id, string name, string licensePlate, string assignment, string date)
        {
            this.Id = id;    
            this.Name = name;
            this.LicensePlate = licensePlate;
            this.Assignment = assignment;    
            this.Date = date;

        }
    }
}
