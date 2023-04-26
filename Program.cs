using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentApiProj
{
    internal class Program
    {
        static DbModels db;
        static void Main(string[] args)
        {
            db = new DbModels();
            var student1 = new Student()
            {
                First_Name = "Ahmad",
                Email = "Ahmad@gmail.com"
            };
            var student2 = new Student()
            {
                First_Name = "Mo",
                Email = "Mo@gmail.com"
            };

            //add to list
            var students = new List<Student>();
            students.Add(student1);
            students.Add(student2);

            //add to database from list
            db.Students.AddRange(students);
            //save changes to database
            db.SaveChanges();

            //turn database table into list
            List<Student> res = db.Students.ToList();

            //print database content
            foreach(Student stu in res)
            {
                Console.WriteLine(stu.Id);
                Console.WriteLine(stu.First_Name);
            }
        }
    }
}
