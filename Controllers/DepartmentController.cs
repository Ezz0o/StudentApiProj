using System;
using System.Data.Entity.Migrations;

namespace StudentApiProj
{
    internal class DepartmentController
    {
        public string addRow(DbModels db, Department department)
        {
            string res = "";
            try
            {
                db.Departments.Add(department);
                db.SaveChanges();
                res = "Added department successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't add department duo to {e}";
            }
            return res;
        }


        public string editRow(DbModels db, int id, Department newDepartment)
        {
            string res = "";
            try
            {
                var department = db.Departments.Find(id);
                if (department == null)
                {
                    res = "department not found";
                    return res;
                }
                department = newDepartment;
                db.Departments.AddOrUpdate(department);
                db.SaveChanges();
                res = "Edited department successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't edit due to {e}";
            }
            return res;
        }
        public string deleteRow(DbModels db, int id)
        {
            string res = "";
            try
            {
                var department = db.Departments.Find(id);
                db.Departments.Remove(department);
                db.SaveChanges();
                res = "Deleted student mark successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't delete studnet mark due to {e}";
            }
            return res;
        }
    }
}
