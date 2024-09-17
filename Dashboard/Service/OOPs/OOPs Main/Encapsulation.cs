using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.OOPs.OOPs_Main
{
    public class Encapsulation
    {
        // Make sure that sensitive data is hidden from the user.

        private string _name; //stores the value but not accessible by other classes directly.

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }

    public class Program 
    {
        public static void Main(string[] args)
        {
            Encapsulation encapsulation = new Encapsulation();
            encapsulation.Name = "test";
        }
    }
}
