using System;
namespace StudentApiProj.Controllers
{
    internal class StudnetMarkController
    {
        public string addRow(DbModels db, StudentMark sm)
        {
            string res = "";
            try
            {
                db.StudentMarks.Add(sm);
                db.SaveChanges();
                res = "Added student mark successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't add studnet mark duo to {e}";
            }
            return res;
        }


        public string editRow(DbModels db, int id, StudentMark newSm)
        {
            string res = "";
            try
            {
                var sm = db.StudentMarks.Find(id);
                if (sm == null)
                {
                    res = "Student mark not found";
                    return res;
                }
                sm = newSm;
                db.StudentMarks.AddOrUpdate(sm);
                db.SaveChanges();
                res = "Edited student mark successfully";
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
                var sm = db.StudentMarks.Find(id);
                db.StudentMarks.Remove(sm);
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
