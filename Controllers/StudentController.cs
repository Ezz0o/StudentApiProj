using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApiProj
{
    class StudentController
    {
        
        public string addRow(Database1Entities db, Student row)
        {
            string res;
            try
            {
                db.Students.Add(row);
                db.SaveChanges();
                res = "Added row succesfukly";
            }
            catch(Exception e)
            {
                res = "Failed to add row: " + e.ToString();
            }
            return res;
        }

        public string editRow(Database1Entities db, int id, string newName = "", string newEmail = "")
        {

            string res;
            try
            {
                var student = db.Students.Find(id);
                if (student == null) 
                {
                    res = "Student not found, nothing to delete";
                    return res;
                }
                if(newName != "")
                {
                    student.Name = newName;
                }
                if(newEmail != "")
                {
                    student.Email = newEmail;
                }
                db.SaveChanges();
                res = "Edited row succesfukly";
            }
            catch (Exception e)
            {
                res = "Failed to edit row: " + e.ToString();
            }
            return res;
        }

        public string deleteRow(Database1Entities db, int id)
        {
            string res;

            try
            {
                var student = db.Students.Find(id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    res = "Deleted row successfully";
                }
                else
                    res = "Student not found, nothing to delete";

            }
            catch (Exception e)
            {
                res = "Failed to delete row: " + e.ToString();
            }

            return res;
        }
    }
}
