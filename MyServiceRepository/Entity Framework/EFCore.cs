using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyServiceRepository.Entity_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static MyServiceRepository.Entity_Framework.EFCore.StudentsDBContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServiceRepository.Entity_Framework
{
    public class EFCore
    {
        //Entity Framework core is an Object Relational Mapper
        // Avoids SQL Code.

        public class SchoolDBContext : DbContext // DbContext is primary class to interact with DB
        {
            DbSet<Students> students {  get; set; } //Entity

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Configure the context to use SQL Server
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\Local;Initial Catalog=AJDB;Integrated Security=True;");
            }
        }

        //Code First Approach:
        // 1. Define entities
        // 2. Annotations
        // 3. Fluent API , onModelCreating to configure entity properties
        // 4. Migration : Managing DB schema changes

        public class StudentsDBContext : DbContext // DbContext is primary class to interact with DB
        {
            public DbSet<Students> students { get; set; } //Entity
            public DbSet<Student> student { get; set; } //Entity
            public DbSet<Course> courses { get; set; } //Entity
            public DbSet<StudentLazy> studentLazies { get; set; } //Entity
            public DbSet<StudentTS> studentTs { get; set; } //Entity

            protected override void OnModelCreating(ModelBuilder modelBuilder) // Fluent API
            {
                modelBuilder.Entity<Students>()
                    .HasKey(p => p.Id);

                modelBuilder.Entity<Students>()
                    .Property(x => x.Id).IsRequired();

                modelBuilder.Entity<StudentTS>()
               .Property(s => s.RowVersion)
               .IsRowVersion();

                modelBuilder.Entity<Students>(entity =>
                {
                    entity.ToTable("Students"); // Specify the table name
                    entity.HasKey(e => e.Id); // Configure the primary key
                });

                //Data Seeding
                modelBuilder.Entity<Students>().HasData(
                    new Students { Id = 1, Name = "John Doe"},
                    new Students { Id = 2, Name = "Jane Smith" }
                );

                // Configure table splitting
                modelBuilder.Entity<Student>()
                    .HasOne(s => s.Address) // This means each Student entity is associated with one StudentAddress.
                    .WithOne() // This specifies that the StudentAddress entity has a single navigation property to the Student entity, and that there is no other property in StudentAddress to navigate back to Student. It indicates a one-to-one relationship where each Student has exactly one StudentAddress, and each StudentAddress is associated with exactly one Student.
                    .HasForeignKey<StudentAddress>(a => a.Id); // Use the same Id for both entities
            }

            public void DBContextToSaveData()
            {
                using (var context = new StudentsDBContext())
                {
                    // Example data to seed
                    var student = new Students
                    {
                        Id = 3,
                    };

                    context.students.Add(student); 
                    context.SaveChanges();
                }
            }

            //LINQ in DBContext
            public void useLinq()
            {
                using(var context = new StudentsDBContext())
                {
                    var students = context.students.Where(x => x.Id == 1).ToList();
                }
            }

            //Eager, Lazy and Explicit Loading
            public void loaderTypes()
            {
                //Eager Loading
                using (var context = new StudentsDBContext())
                {
                    var studentsWithCourses = context.student
                        .Include(s => s.Courses) // The Include method ensures that the related Courses data is loaded in the same query.
                        .ToList();

                    foreach (var student in studentsWithCourses)
                    {
                        Console.WriteLine($"Student: {student.Name}");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"    Course: {course.CourseName}");
                        }
                    }
                }

                //Lazy Loading (virtual keyword)
                using (var context = new StudentsDBContext())
                {
                    var studentsWithCourses = context.studentLazies.Find(1);

                    foreach (var course in studentsWithCourses.Courses) // Lazy Loading will only trigger when you access the Courses collection for the first time.
                    {
                        Console.WriteLine($"    Course: {course.CourseName}");
                    }
                }

                //Explicit Loading 
                using (var context = new StudentsDBContext())
                {
                    //Use Explicit Loading to load related courses after retrieving the student
                    var studentsWithCourses = context.studentLazies.Find(1);

                    // Explicitly load the related Courses
                    context.Entry(studentsWithCourses).Collection(x=> x.Courses).Load();
                }
            }

            // Summary
            // Eager Loading: Retrieves related data in the initial query and is suitable for when you know you will need the related data immediately.
            // Lazy Loading: Loads related data only when accessed, useful for scenarios where related data is not always needed.
            // Explicit Loading: Allows you to control when related data is loaded after the initial query, giving you flexibility in data retrieval.

            public void NoTracking()
            {
                using (var context = new StudentsDBContext())
                {
                    var students = context.students.Where(x => x.Id == 1).AsNoTracking().ToList();

                    // The AsNoTracking method in Entity Framework Core is used to optimize query performance by disabling change tracking for the entities returned from a query.
                    // This can be useful in scenarios where you only need to read data and do not intend to modify it.
                }
            }

            // Concurrency control in Entity Framework (EF) 
            // Types of Concurrency Control in EF
            // 1. Optimistic Concurrency Control
            // 2. Pessimistic Concurrency Control

            // Optimistic : Modify your entity to include a concurrency token. This is usually a byte[] property marked with the [ConcurrencyCheck] attribute or configured using Fluent API.
            public class StudentTS
            {
                public int Id { get; set; }

                public string Name { get; set; }

                [Timestamp] // This attribute is used for concurrency control
                public byte[] RowVersion { get; set; }
            }

            // EF uses concurrency tokens (such as a timestamp or a row version) to detect conflicts. When a row is updated, the token is checked to ensure that it has not changed since it was last read.

            public void OptimisticConcurrencyControl()
            {
                using (var context = new StudentDBContext())
                {
                    try
                    {
                        var student = context.Students.Find(1);
                        student.Name = "Updated Name";

                        context.SaveChanges(); // Throws DbUpdateConcurrencyException if a conflict is detected
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        // Handle concurrency conflict
                        var entry = ex.Entries.Single();
                        var clientValues = (Student)entry.Entity;
                        var databaseValues = (Student)entry.GetDatabaseValues().ToObject();

                        // Compare client and database values and resolve conflict
                        Console.WriteLine("Concurrency conflict detected.");
                    }
                }
            }

            // Pessimistic Concurrency Control
            // Assumes conflicts are frequent. Uses explicit locks to prevent other users from modifying data while it is being accessed. Not directly supported by EF but can be implemented using raw SQL.

            public void PessimisticConcurrencyControl()
            {
                using (var context = new StudentDBContext())
                {
                    var studentId = 1;

                    // Start a transaction
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        // Lock the row
                        var student = context.Students
                            .FromSqlRaw("SELECT * FROM Students WITH (UPDLOCK) WHERE Id = {0}", studentId)
                            .Single();

                        // Update the student record
                        student.Name = "Updated Name";
                        context.SaveChanges();

                        // Commit the transaction
                        transaction.Commit();
                    }
                }
            }

            public void ExecuteProcs()
            {
                // Compiled Query
                var compiledQuery = EF.CompileQuery((StudentDBContext dbContext, int id) =>
            dbContext.Students.Where(x => x.Id == id).ToList());

                using (var context = new StudentDBContext())
                {
                    var data = context.Students.FromSqlRaw("Exec ProcName @p1,@p2", [1, 2]).ToList();
                    var data2 = context.Students.FromSqlInterpolated($"Exec ProcName @p1 = {1},@p2 = {2}").ToList();

                    // Cross DB Calls :

                    context.Students.FromSqlRaw("select * from Database1.T1 JOIN Database2.T2 ON ...").ToList();

                    // Compiled Query
                    compiledQuery(context, 1);

                }
            }

            // Table splitting in Entity Framework Core allows you to map multiple entity types to a single database table.
            // This is useful when you want to separate entity properties into different classes but store them in the same table, often to optimize performance or to logically group related properties.

            
            // Multiple DB = Multiple DBContext
        
            
        }

        //Example
        public class StudentDBContext : DbContext
        {
            public DbSet<Student> Students { get; set; }
            public DbSet<Course> Courses { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\Local;Initial Catalog=StudentDB;Integrated Security=True;");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configure Students entity
                modelBuilder.Entity<Student>()
                    .ToTable("Students")
                    .HasKey(s => s.Id);

                // Configure Courses entity
                modelBuilder.Entity<Course>()
                    .ToTable("Courses")
                    .HasKey(c => c.Id);

                // Define relationship
                modelBuilder.Entity<Student>()
                    .HasMany(s => s.Courses)
                    .WithOne(c => c.Student)
                    .HasForeignKey(c => c.StudentId);

                // Seed data
                modelBuilder.Entity<Student>().HasData(
                    new Student { Id = 1, Name = "John Doe" },
                    new Student { Id = 2, Name = "Jane Smith" }
                );

                modelBuilder.Entity<Course>().HasData(
                    new Course { Id = 1, CourseName = "Math 101", StudentId = 1 },
                    new Course { Id = 2, CourseName = "History 201", StudentId = 1 },
                    new Course { Id = 3, CourseName = "Biology 101", StudentId = 2 }
                );
            }

            public static void Main()
            {
                using (var context = new StudentDBContext())
                {
                    var studentsWithCourses = context.Students
                        .Include(s => s.Courses) // Eager load Courses
                        .ToList();

                    foreach (var student in studentsWithCourses)
                    {
                        Console.WriteLine($"Student: {student.Name}");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"    Course: {course.CourseName}");
                        }
                    }

                    //Student: John Doe
                    //Course: Math 101
                    //Course: History 201
                    //Student: Jane Smith
                    //Course: Biology 101
                }
            }
        }
    }
}
