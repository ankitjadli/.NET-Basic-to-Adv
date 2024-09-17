using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dashboard.Service.Design_Patterns.Structural.Decorator;

namespace Dashboard.Service.Design_Patterns
{
    public class Structural
    {
        // Adapter Pattern

        public class Pattern
        {
            public interface IC1
            {
                public void Main();
            }

            public class C2
            {
                public void Main()
                {
                    Console.WriteLine("Test");
                }
            }

            public class C1 : IC1
            {
                private C2 _c2;

                public void Main()
                {
                    Console.WriteLine("Test");
                }

                public C1(C2 c2)
                {
                    _c2 = c2;
                }

                public void CallC2()
                {
                    _c2.Main();
                }
            }

            public void Main()
            {
                C1 c1 = new C1(new C2());
                c1.Main();
            }
        }

        //Decorator

        public class Decorator
        {
            public abstract class ADec //Componnent
            {
                public abstract void Main();
            }

            public class MainComponent : ADec //MainComponent
            {
                public override void Main()
                {
                    Console.WriteLine("");
                }
            }

            public class SubC : ADec //Decorator
            {
                private ADec _dec;

                public SubC(ADec dec)
                {
                    _dec = dec;
                }

                public override void Main() => _dec.Main();
            }

            public class CoSub : SubC // SubComponnent
            {
                public CoSub(ADec dec) : base(dec)
                {
                }

                public override void Main() => base.Main();
            }

            public void Main()
            {
                ADec dec = new MainComponent();
                CoSub coSub = new CoSub(dec);
                coSub.Main();
            }
        }
    }
}
