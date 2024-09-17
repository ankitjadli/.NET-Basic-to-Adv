using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.OOPs.OOPs_Main
{
    public class Inheritance
    {
        public class BaseClass
        {
            protected int a = 1;

            public BaseClass() { }

            public BaseClass(int a)
            {
                this.a = a;
            }

            public void BaseClassMethod()
            {
                Console.WriteLine("Test");
            }
        }

        public class Child : BaseClass
        {
            public Child() : base() { } // calls constuctor of base class as well

            public Child(int a) : base(a) { } // calls parametrized constuctor of base class as well

            public void ChildClassMethod()
            {
                a = 1; // After inheritance of Base class i can access this 
                BaseClassMethod(); // Base class methos can be hit from here
            }
        }

        sealed class Parent2
        {
            public void Parent2Method()
            {
                Console.WriteLine("Test");
            }
        }

        //public class Child2 : Parent2 Not possible as Parnt2 is marked as sealed.
        
        // Sealed Method:

        public class SealedParent
        {
            public virtual void Method()
            {
                Console.WriteLine("");
            }
        }

        public class SealedChild1 : SealedParent
        {
            public override void Method()
            {
                const int a = 1;
            }
        }

        public class SealedChildB : SealedParent
        {
            sealed public override void Method()
            {
                Console.WriteLine("Test");
            }
        }

        public class SealedChild2 : SealedChildB
        {
            // This will cause a compile-time error because Method() is sealed in SealedChildB.
            //public override void Method()
            //{
            //    const int a = 1;
            //    Console.WriteLine($"Child2 Method, a = {a}");
            //}
        }
    }
}
