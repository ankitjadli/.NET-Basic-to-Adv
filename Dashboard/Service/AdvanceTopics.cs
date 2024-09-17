using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service
{
    public class AdvanceTopics
    {
        //this keyword :

        public class Test
        {
            public int a;

            public void TestM(int a)
            {
                a = a; // same variable warning
                this.a = a; // correct way
            }

            public Test(int a)
            {
                this.a = a;
            }

            public Test(int a, int b) : this(a) // Calls the constructor with 1 argument as well
            {
                // Sequence is this constructor then this(a)
            }

            public void TestB(Test t)
            {
                t.a = 1;
            }

            public void TestC()
            {
                TestB(this); // this as object
            }
        }

        //Indexer

        public class Indxers
        {
            public int[] a = new int[3];

            public int this[int i]
            {
                get
                {
                    return a[i];
                }
                set
                {
                    a[i] = value;
                }
            }

            public void Main()
            {
                Indxers indxers = new Indxers();
                indxers[0] = 1;
            }
        }

        public class StaticExample
        {
            public static void TestMethod() // Can be Directly called.  
            {
                System.Console.WriteLine(":");
            }

            public void Caller()
            {
                StaticExample.TestMethod();
            }
        }

        public static class StaticExample2 // Static class cant be inherited.
        {
            public static int a = 1; // All variables need to be static

            public static void TestMethod() // Can be Directly called.  
            {
                System.Console.WriteLine(":");
            }

            public static void Caller() // All methods need to be static
            {
                StaticExample.TestMethod();
                ///StaticExample2 staticExample = new StaticExample2();  cant make objct of static class
            }
        }

        //Partial Classes and Methods

        public class PartialClasses
        {
            // We can write the same class in diff cs files using partial keyword

            partial class PartialClass
            {
                partial void PartialMethod(); // Implementation not mandatory

                public void Method1()
                {
                    Console.WriteLine("M1");
                }
            }

            partial class PartialClass
            {
                public int a = 1;

                public void Method2()
                {
                    Console.WriteLine("M2");
                }

                partial void PartialMethod()
                {
                    Console.WriteLine("PartialMethod");
                }
            }

            public void Main()
            {
                PartialClass partial = new PartialClass();

                partial.Method1();
                partial.Method2();
                partial.a = 11;
            }
        }

        //C# Collection
        public class Collections
        {
            public static void Main()
            {
                // System.Collection                    ArrayList, HashTable
                // System.Collection.Concurrent         ConcurrentDictionary<T,T> Thread safe
                // System.Collection.Generic            List<T>, Stack<T>, Queue<T>

                List<int> list = new List<int>();
                ConcurrentDictionary<int,int> keyValuePairs = new ConcurrentDictionary<int,int>();
                ArrayList arrayList = new ArrayList();
            }
        }

        //C# Lambda Expressions

        public class Lamba
        {
            public void Method()
            {
                int a = 1;

                // Anonymous method
                Func<int, int> fn = (int b) => a + 1; //Expression Lambda

                int b = fn(1);

                Func<int, string> fn2 = (int c) => //Statement Lambda
                {
                    return c.ToString();
                };

                string d = fn2(3);

                int square(int x)
                {
                    return x;
                }

                Func<int, int> square2 = square;

                //Anonymous Types
                var subject = new {  a = 1 , c = ""};
            }
        }
    }
}
