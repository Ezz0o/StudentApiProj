using System;
using System.Data.Entity.Migrations;

namespace StudentApiProj
{
    internal class SubjectLectureController
    {
        public string addRow(DbModels db, SubjectLecture sl)
        {
            string res = "";
            try
            {
                db.SubjectLectures.Add(sl);
                db.SaveChanges();
                res = "Added subject lecture successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't add subject lecture duo to {e}";
            }
            return res;
        }


        public string editRow(DbModels db, int id, SubjectLecture newSubLec)
        {
            string res = "";
            try
            {
                var sl = db.SubjectLectures.Find(id);
                if (sl == null)
                {
                    res = "Student mark not found";
                    return res;
                }
                sl = newSubLec;
                db.SubjectLectures.AddOrUpdate(sl);
                db.SaveChanges();
                res = "Edited subject lecture successfully";
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
                var sl = db.SubjectLectures.Find(id);
                db.SubjectLectures.Remove(sl);
                db.SaveChanges();
                res = "Deleted subject lecture successfully";
            }
            catch (Exception e)
            {
                res = $"Couldn't delete subject lecture due to {e}";
            }
            return res;
        }
    }
}
