using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koerselslog
{
    internal class User 
    {
        public string Name { get; set; }    
        public string LicensePlate { get; set; }    
        public User (string name, string licensePlate)
        {
            Name = name;    
            LicensePlate = licensePlate;

        }
    }
}
