using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Service.OOPs.OOPs_Main
{
    public class Abstraction
    {
        //Way of showing only the essential info to the user.

        //Done by Abstract class, Interfaces.
        // Abstract class:

        public abstract class Abs
        {
            public int a = 1;

            public void Test()
            {
                Console.WriteLine("Test 123");
            }

            public abstract void CommonMethod();
        }

        public class childClass : Abs
        {
            public override void CommonMethod()
            {
                throw new NotImplementedException();
            }
        }

        //Interface : Complete Abstractions

        public interface Interface1
        {
            public int x { get; set; }

            public void Common();
        }

        public class ParentClass : Interface1
        {
            public int x { get; set; }

            public void Common()
            {
                Console.WriteLine("Test");
            }
        }

        // Enum

        enum TestEnum{
            a = 2, // By default the first value is 0 and so on..
            b = 3,
        }

        public class UseEnum
        {
            public UseEnum() 
            {
                Console.WriteLine((int) TestEnum.a);
            }
        }
    }
}
