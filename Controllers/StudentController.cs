using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentApiProj
{
    class StudentController
    {
        //TEST ALL FUNCTIONS IN HERE
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

        public string editRow(DbModels db, int id, Student updatedBody)
        {

            string res;
            try
            {
                var student = db.Students.Find(id);
                if(student == null)
                {
                    res = "student not found";
                    return res;
                }
                student = updatedBody;
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
        public string studentsByDept(DbModels db, int DeptId, int term = 0)
        {
            string res = "First Name\t Last Name";

            List<Student> query = db.Students.Where(
                student => student.DepartmentId == DeptId).ToList();
            
            if(term != 0)
            {
                query = db.Students.Where(
                student => student.DepartmentId == DeptId &&
                (student.Department == student.Department.Subjects.Where(subj => subj.Term == term))
                ).ToList();
            }

            if (query.Count <= 0)
            {
                res = "No students found in this department";
            }
            else
            {
                foreach(Student student in query)
                {
                    res += $"\n{student.First_Name}\t {student.Last_Name}";
                }
            }
            return res;
        }

        public string studentsAttendedExam(DbModels db, int ExamId)
        {
            string res = "First Name\t Last Name\t Mark";

            //get student marks for exam
            var studentMarks = db.StudentMarks.Where(marks => marks.ExamId == ExamId).ToList();
            
            //empty result handle
            if (studentMarks.Count <= 0)
            {
                res = "No one has attended this exam yet";
            }
            else
            {
                //print each student and their respective grade in the exam
                foreach(StudentMark s in studentMarks)
                {
                    res += $"{s.Student.First_Name}\t {s.Student.Last_Name}\t {s.Mark}";
                }
            }

            return res;
        }


        public string studentsAbsentInExam(DbModels db, int ExamId)
        {
            string res = "First Name\t Last Name";

            //query student marks for students that attended the exam
            var studentMarks = db.StudentMarks.Where(marks => marks.ExamId== ExamId).ToList();
            //put the students from the resulting query in a list
            List<Student> attended = new List<Student>();
            foreach(StudentMark s in studentMarks) { 
                attended.Add(s.Student);
            }
            
            //query for those who did not attend
            var didNotAttend = db.Students.Except(attended).ToList();

            if(didNotAttend.Count <= 0) 
            {
                res = "No student was absent during this exam";
            }
            else
            {
                foreach (Student student in didNotAttend)
                {
                    res += $"\n{student.First_Name}\t {student.Last_Name}";
                }
            }
            return res;
        }

        public string subjectsForStudent(DbModels db, int studentId, int DeptId, int year, int term, bool calcAvg)
        {
            string res = "Subject\t Year\t Term\t Dept\t Mark";
            double avg = 0.0;
            var student = db.Students.Where(stu => 
            (stu.Id == studentId)
            ).First();

            if (student == null)
            {
                res = "No student with this id found";
            }

            var subjects = db.Subjects.Where(sub => 
            (sub.Department.Students == sub.Department.Students.Where(std => std.Id == studentId)) &&
            (sub.Year == year) &&
            (sub.Term == term) &&
            (sub.DepartmentId == DeptId)
            ).ToList();

            if(subjects.Count <= 0)
            {
                res = "No subjects for the details specified";
            }
            else
            {
                foreach (Subject sub in subjects)
                {
                    var mark = db.StudentMarks.Where(stdMark => stdMark.Exam.Subject == sub).First();
                    res += $"\n{sub.Name}\t {sub.Year}\t {sub.Term}\t {sub.Department}\t {mark}";
                    avg += mark.Mark;
                }
            }
            if(calcAvg == true)
            {
                res += $"\n avg of all marks: {avg}";
            }
            return res;
        }

        public string lecturesPerSubject(DbModels db, int year = -1, int term = -1)
        {
            string res = "";

            var subjects = db.Subjects.ToList();

            if (subjects == null)
            {
                res = "subjects not found";
            }
            else
            {
                //query to group lectures by subject
                var lectures = (from lecture in db.SubjectLectures group lecture by lecture.Subject into subjectLectureGroup select subjectLectureGroup);
                if(term != -1)
                {
                    //don't know wtf I just wrote, but I hope it works
                    lectures = lectures.Where(subjs => subjs == subjs.Where(subj => subj.Subject.Term == term));
                }
                if (year != -1)
                {
                    lectures = lectures.Where(subjs => subjs == subjs.Where(subj => subj.Subject.Year == year));
                }
                if (lectures == null)
                {
                    res = "no lectures found in subjects";
                }
                else
                {
                    foreach(var subject in lectures)
                    {
                        res += $"Subject name:\t {subject.Key}";
                        foreach(var lecture in subject)
                        {
                            res += $"\nLectures for subject:\t {lecture.Title}";
                        }
                    }
                }
            }

            return res;
        }

    }
}
