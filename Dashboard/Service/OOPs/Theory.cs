using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.OOPs
{
    public class Theory
    {
        // OOPs : Object oriented programming. 

        //Faster, Resuable, Clean

        public class Classname
        {
            string test;

            public Classname()  // Constructor, called when object is created
            {
                test = string.Empty;
            }

            //Destructor
            ~Classname()
            {
                Console.WriteLine("Destructor called by GC");
            }

            public Classname(int a) // Parametrized constructor
            {
                test = a.ToString();
            }

            public void MainM()
            {
                Classname classname = new Classname();
            }

            //Access modifier

            public int a = 1; //Accessible for all classes;
            private int b = 2; //Accessible only to this class.
            protected int c = 3; //Accessible to this class and class that inherits.
            internal int d = 4; //Accessible to class in same assemly.
        }

        public class ClassNameI : Classname
        {
            public ClassNameI()
            {
                a = 1; //public
                c = 2; //protected
                //b = 3; Throws error as private. 
                d = 4;  //internal
            }
        }
    }
}
