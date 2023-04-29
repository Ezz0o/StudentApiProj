using System;
using System.Data.Entity.Migrations;

namespace StudentApiProj.Controllers
{
    internal class ExamController
    {
        public string addRow(DbModels db, Exam exam)
        {
            string res = "";
            try
            {
                db.Exams.Add(exam);
                db.SaveChanges();
                res = "Added exam successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't add exam duo to {e}";
            }
            return res;
        }


        public string editRow(DbModels db, int id , Exam newExam)
        {
            string res = "";
            try
            {
                var exam = db.Exams.Find(id);
                if (exam == null)
                {
                    res = "Exam not found";
                    return res;
                }
                exam = newExam;
                db.Exams.AddOrUpdate(exam);
                db.SaveChanges();
                res = "Edited Exam successfully";
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
                var exam = db.Exams.Find(id);
                db.Exams.Remove(exam);
                db.SaveChanges();
                res = "Deleted exam successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't delete exam due to {e}";
            }
            return res;

        }
    }
}
