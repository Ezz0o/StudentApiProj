using System;
using System.Data.Entity.Migrations;

namespace StudentApiProj.Controllers
{
    internal class SubjectController
    {
        public string addRow(DbModels db , Subject subject) {
            string res="";
            try
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                res = "Added subject successfully";
            }
            catch(Exception e) {
                res = $"Couldn't add duo to {e}";
            }
            return res;
        }


        public string editRow(DbModels db, int id, Subject newSubject)
        {
            string res = "";
            try {
                var subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    res = "subject not found";
                    return res;
                }
                subject = newSubject;
                db.Subjects.AddOrUpdate(subject);
                db.SaveChanges();
                res = "Edited subject successfully";
            }
            catch (Exception e) {
                res = $"Couldn't edit due to {e}";
            }
            return res;
        }
        public string deleteRow(DbModels db, int id)
        {
            string res="";
            try
            {
                var subject = db.Subjects.Find(id);
                db.Subjects.Remove(subject);
                db.SaveChanges();
                res = "Deleted subject successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't delete subject due to {e}";
            }
            return res;

        }
    }
}
