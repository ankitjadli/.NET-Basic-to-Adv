using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.Design_Patterns
{
    public class Creational
    {
        //Best practices for scalablity, maintainability etc
        // 1. Creational Patterns  - deal with objeect creation mechanism
        // 2. Structural Patterns  - deal with object composition and relationship
        // 3. Behavioral Patterns - deal with object intraction and relationship distribution

        // 1. Creational Patterns 
        // 1.1 Singleton

        public class Singleton1
        {
            private static Singleton1 _s1;

            private Singleton1() { }

            public static Singleton1 s1
            {
                get
                {
                    return _s1 == null ? new Singleton1() : _s1;
                }
            }

            public void Method()
            {

            }
        }

        public void Main() 
        {
            Singleton1 s1 = Singleton1.s1;
            s1.Method();
        }

        //1.2 Factory

        public class FactoryClass
        {
            public abstract class Abs
            {
                public abstract void Method();
            }

            public class Child1 : Abs
            {
                public override void Method()
                {

                }
            }

            public class Child2 : Abs
            {
                public override void Method()
                {

                }
            }

            public Abs GetTypes()
            {
                int a = 1;

                switch (a)
                {
                    case 1:
                        return new Child1();
                    default:
                        throw new ArgumentException();
                }
            }

            public void Main()
            {
                Abs abs = GetTypes();
                abs.Method();
            }
        }
    }
}
