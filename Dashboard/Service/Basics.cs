using System; // used to import classes included in this namespace.

namespace Dashboard.Service // Provide namespace to find classes inside it in other classes when we refer it with using keyword.
{
    class Progam // container of data and methods.
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This will print the text followed by a new line");
            Console.Write("This will just print the sting with no new line");
        }

        // Single line comment

        // Multi line comments
        /*public void BackDown()
        {
            string? input = Console.ReadLine();
        }*/

        public void MultiArguments(string a, string b)
        {
            Console.Write($"{a} {b}");
        }


        // DataType and Variables

        public void DataTypes()
        {
            // const int contvariale; Throws error as needed to be declared by default
            const int constvariable = 1;

            Console.WriteLine("Const variable : " + constvariable);


            //DataTypes
            int a = 1; // 4Bytes;   Limit : -2^31 to 2^31 - 1 -> int32
            long b = 2; // 8Bytes;   Limit : -2^63 to 2^63 - 1 -> int64
            float f = 3; // 4 Bytes;    Good to store 6-7 decimal places
            double d = 4; // 8 Bytes;   Good to strore 15 decimal places
            bool t = true; // 1 Bit;
            char c = 'A'; // 2 Bytes
            string s = "asds"; // 2 Bytes each.


            //Type Casting 
            // 2 Types : Implicit, Explicit

            // Implicit char -> int -> long -> float -> double

            a = c; // Automatically
            b = a;
            f = b;
            d = f;

            // Explicit double -> float -> long -> int -> char

            f = (float)d; // Maually

            //Conversion method

            int int1 = Convert.ToInt32("34");
            double d2 = Convert.ToDouble("12");
            long l1 = Convert.ToInt64("12");

            string? s1 = Console.ReadLine(); // Returns the input as a string

            int bitwiseop = 5;
            bitwiseop >>= 2; // Bitwise move right

            Math.Max(1, 2);
            Math.Sqrt(123);

            string s5 = "This is amazing";
            var len = s.Length;
            var sUpper = s.ToUpper();

            //String interpolation ->
            string inter1 = "1", inter2 = "2";
            string inter3 = $"3 {inter1} is the new 4 {inter2}";
            int interi = inter3.IndexOf(inter1);
            string inter4 = "this is a test \' \' \\ \"";

            Console.WriteLine(inter4 + interi);

            //Switch Statement

            int switchinnt = 1;

            switch (switchinnt)
            {
                case 0:
                    Console.WriteLine("0 hai");
                    break;
                case 1:
                    Console.WriteLine("1 hai");
                    break;
                default:
                    break;
            }

            //While Loop

            while (switchinnt > 1)
            {
                switchinnt--;
            }

            do
            {
                switchinnt++;
            } while (switchinnt > 1);

            int[] array1 = new int[1];
            int[] array2 = { 1, 2, 3 };
            int[] array3 = new int[3] { 1, 2, 3 };

            Array.Sort(array3);
            array1.Min();
            array1.Average();
            array1.Sum();

            int[,] testArray = new int[1, 3] { { 1, 2, 3 } };
            int[,] testArray2 = { { 1, 2 } };

            testArray.GetLength(0); // Length of first array of arrays.

            //Named Arguments : Way of calling a method
            MultiArguments(b: "test", a: "test2"); // incorrect order but replaced by argument name
        }

        //Method Overloading
        // Same method name with different arguments

        public void MethodOver()
        {

        }

        public void MethodOver(int a)
        {

        }

        public void MethodOver(int b, int a)
        {

        }
    }
}