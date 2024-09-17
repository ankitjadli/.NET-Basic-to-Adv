using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.OOPs.OOPs_Main
{
    public class Polymorphism
    {
        public class BaseClass1
        {
            public virtual void BaseM()
            {
                Console.WriteLine("Base");
            }
        }

        public class Child1 : BaseClass1
        {
            public override void BaseM()
            {
                Console.WriteLine("Child1");
            }
        }

        public class Child2 : BaseClass1
        {
            public void BaseM()
            {
                Console.WriteLine("Child2");
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                BaseClass1 base1 = new Child1();
                base1.BaseM(); //Child1 as method is overriden

                BaseClass1 base2 = new Child2();
                base2.BaseM(); //Base as method not overridden
            }
        }
    }
}
