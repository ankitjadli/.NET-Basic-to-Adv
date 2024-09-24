using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dashboard.Service
{
    public class AdvanceTopics2
    {
        //Generic Class

        public class Generic<T>
        {
            public T a { get; set; }

            public string Method(T Type)
            {
                return Type.ToString();
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                Generic<int> a = new Generic<int>();
                a.Method(1);
            }

            public IEnumerable Method(int a) // will return 2 ints
            {
                yield return 1;
                yield return 2;
            }
        }

        //Delegate : Pointer to a method

        public class DelegateTest
        {
            public void Test(int i)
            {
                Console.WriteLine("");
            }

            public delegate void TestDele(int i); // Delegate declaration

            public DelegateTest()
            {
                TestDele testDele = new TestDele(Test);// Points to Test Method.
                testDele(2);
            }

            //Regex
            public void Regexx()
            {
                Regex r = new Regex("");
                r.IsMatch("Test String");
            }
        }

        // Events

        public class Publisher
        {
            public delegate void NotifyEvent(object sender, EventArgs e);

            public event NotifyEvent Notify;

            public void RaiseEvent()
            {
                Notify?.Invoke(this, EventArgs.Empty);
            }
        }

        public class Subscriber
        {
            public void onNotify(object sender, EventArgs e)
            {
                Console.WriteLine("Notofocations");
            }
        }

        public class Program2
        {
            public static void Main(string[] args)
            {
                Publisher publisher = new Publisher();
                Subscriber subscriber = new Subscriber();
                publisher.Notify += subscriber.onNotify;
                publisher.RaiseEvent();
            }
        }

    }

    //Extension Methods
    public static class Extensions
    {
        public static void isPal(this string s)
        {
            Console.WriteLine($"Pal: {s}");
        }

        public static void UseExtension()
        {
            string s = "0";
            s.isPal(); // string Extension method.
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int DID { get; set; }
    }

    public class Department
    {
        public string Name { get; set; }

        public int ID { get; set; }
    }

    // LinQ (Langage Inntegrated Quey

    public class LinqDemo
    {
        int[] a = new int[4] { 1, 2, 3, 4 };

        public void Main(string[] args)
        {
            var filtred = from x in this.a
                          where x == 1
                          select x;

            var b = a.Select(x => x);
            var c = a.Where(x => x == 1);

            var people = new List<Person>
            {
                new Person { DID = 1 , Name = ""}
            };

            var deps = new List<Department>
            {
                new Department{ID = 1, Name = ""}
            };

            var joined = people.Join(deps, x => x.DID, y => y.ID, (x, y) => new { a = x.Name, b = y.Name });

            var g = deps.GroupBy(x => x.ID);
            deps.OrderBy(c => c.ID);
            deps.Count();
            deps.Sum(x => x.ID);
            deps.Average(x => x.ID);
            deps.Min(x => x.ID);
            deps.Any(x => x.ID == 1);
            deps.All(x => x.ID == 1); // all elements should statisfy condition

            //Group By and Join
            DataTable customersTable = new DataTable("Customers");
            customersTable.Columns.Add("CustomerID", typeof(int));
            customersTable.Columns.Add("CustomerName", typeof(string));
            customersTable.Columns.Add("Country", typeof(string));

            customersTable.Rows.Add(1, "Alice", "USA");
            customersTable.Rows.Add(2, "Bob", "Canada");
            customersTable.Rows.Add(3, "Charlie", "USA");
            customersTable.Rows.Add(4, "Ankit", "India");
            customersTable.Rows.Add(5, "Nishant", "USA");

            // Create Orders DataTable
            DataTable ordersTable = new DataTable("Orders");
            ordersTable.Columns.Add("OrderID", typeof(int));
            ordersTable.Columns.Add("CustomerID", typeof(int));
            ordersTable.Columns.Add("OrderAmount", typeof(decimal));
            ordersTable.Columns.Add("OrderDate", typeof(DateTime));

            ordersTable.Rows.Add(101, 1, 250.00m, new DateTime(2023, 9, 20));
            ordersTable.Rows.Add(102, 2, 300.50m, new DateTime(2023, 9, 21));
            ordersTable.Rows.Add(103, 1, 150.75m, new DateTime(2023, 9, 22));
            ordersTable.Rows.Add(104, 3, 450.00m, new DateTime(2023, 9, 23));
            ordersTable.Rows.Add(105, 2, 100.00m, new DateTime(2023, 9, 24));

            var test = customersTable.AsEnumerable().GroupBy(x => x["Country"].ToString()).Select(y => new { KeyName = y.Key, Values = y.ToList() }).ToList();

            var test2 = customersTable.AsEnumerable().Join(ordersTable.AsEnumerable(), c => c["CustomerID"].ToString(), o => o["CustomerID"].ToString(),
                (c, o) => new {
                    CustomerName = c["CustomerName"],
                    OrderAmount = o["OrderAmount"]
                }).ToList();

        }
    }

    //Asynchronus Programming
    public class AsyncClass
    {
        public async Task<string> Method()
        {
            return string.Empty;
        }

        public async Task Main()
        {
            await Method();
        }

        public void TestTask()
        {
            Console.WriteLine("");
        }

        public int TestTask2(int a)
        {
            return a;
        }

        public void TasksMethod()
        {
            Task t = new Task(TestTask);
            Task t2 = new Task(TestTask);
            t.Start();
            t.ContinueWith(_ => t2.Start());
            t.Wait();

            int a = 2;

            Task<int> t3 = new Task<int>(() => TestTask2(a));

            a = t3.Result;

            Task[] tasks = new Task[1]
            {
                new Task(()=> TestTask())
            };

            Task.WaitAll(tasks);

            List<int> list = new List<int>();

            Parallel.ForEach(list, number =>
            {

            });
        }
    }

    //CustomException and Garbage Collector

    public class CustomExp : Exception
    {
        public CustomExp() { }

        public CustomExp(string s) : base(s)
        {
            Console.WriteLine($"{s}");
            GC.Collect(); // Garbage Collector
        }

        public void Main()
        {
            CustomExp customExp = new CustomExp();
            WeakReference<CustomExp> weakReference = new WeakReference<CustomExp>(customExp);

            customExp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // weakReference will be null
            tests tests = new tests();
            tests.tesst();
        }

        struct tests //copy by value
        {
            public int a { get; set; }

            public void tesst()
            {
                Console.WriteLine("tesst");
            }
        }
    }

    public class Threads
    {
        private readonly object lockobj = new object();

        public void DoWork()
        {

        }

        public void Main()
        {
            Thread task1 = new Thread(new ThreadStart(DoWork));
            task1.Start();
            Thread.Sleep(100);
            //task1.Join(); //wait for thread to finish

            lock (lockobj)
            {
                Console.WriteLine("Test");
            }
        }

        public void MonnitorTest()
        {
            Monitor.Enter(lockobj); //Custom Enter and exit commands

            Console.WriteLine("");

            Monitor.Exit(lockobj);
        }

        public int TaskTest(int c)
        {
            return c;
        }

        public void Interlocked()
        {
            int n = 1;
            System.Threading.Interlocked.Increment(ref n);

            //Normal Task Start
            Task task = new Task(() => MonnitorTest());
            task.Start();

            //Task With Return
            Task<int> ttest = new Task<int>(() => TaskTest(2));
            int res = ttest.Result;
        }
    }

    public class SystemIO
    {
        // uses System.IO

        public void Main()
        {
            //File, FileInfo, Directory, DirectoryInfo, Path

            File.Create("");
            File.ReadAllLines("");

            using(FileStream fs = File.Create("")) // using block is good for garbage collection
            {
                byte[] info = UTF32Encoding.UTF8.GetBytes("Test123"); // text to byte[]
                fs.Write(info);
            }

            Directory.CreateDirectory("");
            Directory.GetFiles("");

            Path.GetExtension("");
            Path.GetFileName("");

            FileInfo fs2 = new FileInfo("");
            //fs.Name;
            //fs.Length;

            DirectoryInfo df = new DirectoryInfo("");
            //df.Name;
            //df.LastAccessTime
        }
    }

    public class Refelction
    {
        public class Ref1
        {
            public int a {  get; set; }

            public void Test()
            {
                Console.WriteLine("");
            }

            public int Test(int a)
            {
               return a;
            }
        }

        public void main()
        {
            Type t = typeof(Ref1); // use typeof 

            //t.Name;
            foreach(PropertyInfo propertyInfo in t.GetProperties())
            {
                //propertyInfo.Name; 
            }

            foreach (MethodInfo methodInfo in t.GetMethods())
            {
                //methodInfo.Name; 
            }

            object obj = Activator.CreateInstance(t); // Initiate reflected object
            t.GetMethod("Test").Invoke(obj, null); // Find and trigger the method.

            t.GetMethod("Test").Invoke(obj, new object[] { 1 }); // Find and trigger the method.

            t.GetProperty("a").GetValue(obj);

            //Load Assembly
            Assembly assembly = Assembly.Load("");
            foreach (var x in assembly.GetTypes())
            {
                Console.WriteLine($"{x.FullName}");
            }
        }
    }
}
