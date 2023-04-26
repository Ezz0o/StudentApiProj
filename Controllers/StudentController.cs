using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApiProj
{
    class StudentController
    {

        public string addRow(DbModels db, Student row)
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

        public string editRow(DbModels db, int id, string newFName = "", string newLName = "", string newEmail = "",
                               string newPhone = "", string newRegDate = null, int newDept = -1)
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
                if(newFName != "")
                {
                    student.First_Name = newFName;
                }
                if (newLName != "")
                {
                    student.Last_Name = newLName;
                }
                if (newPhone != "")
                {
                    student.Phone = newPhone;
                }
                if (newEmail != "")
                {
                    student.Email = newEmail;
                }
                if (newRegDate != "")
                {
                    student.RegisterDate = DateTime.Parse(newRegDate);
                }
                if (newDept != -1)
                {
                    student.DepartmentId = newDept;
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

        public string deleteRow(DbModels db, int id)
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
