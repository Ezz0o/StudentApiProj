using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentApiProj
{
    internal class Program
    {
        static Database1Entities db;
        static void Main(string[] args)
        {
            db = new Database1Entities();
            var student1 = new Student()
            {
                Name = "Ahmad",
                Email = "Ahmad@gmail.com"
            };
            var student2 = new Student()
            {
                Name = "Mo",
                Email = "Mo@gmail.com"
            };

            var students = new List<Student>();
            students.Add(student1);
            students.Add(student2);

            db.Students.AddRange(students);
            db.SaveChanges();

            List<Student> res = db.Students.ToList();

            foreach(Student stu in res)
            {
                Console.WriteLine(stu.Id);
                Console.WriteLine(stu.Name);
            }
        }
    }
}
