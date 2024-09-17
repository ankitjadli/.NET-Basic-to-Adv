using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.Design_Patterns
{
    public class BhavioralPattern
    {
        // 1. Observer Pattern
        /// <summary>
        ///   One to many dependency, when one object cange state, all its dependents are notified
        /// </summary>
        public class Observer
        {
            public interface IOb
            {
                public void Test();
            }

            public class Ob : IOb
            {
                private string _name;

                public Ob(string name)
                {
                    _name = name;
                }

                public void Test()
                {
                    Console.WriteLine(_name);
                }
            }

            public class Notifier
            {
                public List<IOb> obs = new List<IOb>();

                public void Attach(IOb ob)
                {
                    obs.Add(ob);
                }

                public void RemoveIt(IOb ob) => obs.Remove(ob);

                public void Notify()
                {
                    foreach (IOb ob in obs)
                    {
                        ob.Test();
                    }
                }
            }

            public void Main()
            {
                IOb ob1 = new Ob("test1");
                IOb ob2 = new Ob("test2");

                Notifier notifier = new Notifier();
                notifier.Attach(ob1);
                notifier.Attach(ob2);

                notifier.Notify();
            }
        }

        //Stratagy Pattern
        public class Stratagy
        {
            public interface IClassA
            {
                public void Test();
            }

            public class ClassA : IClassA
            {
                public void Test() => Console.WriteLine("");
            }

            public class StrategyClass
            {
                private IClassA _classA;

                public StrategyClass(IClassA classA)
                {
                    _classA = classA;
                }

                public void Test() => _classA.Test();
            }

            public void Main()
            {
                IClassA classA = new ClassA();
                StrategyClass strategyClass = new StrategyClass(classA);
                strategyClass.Test();
            }
        }
    }
}
