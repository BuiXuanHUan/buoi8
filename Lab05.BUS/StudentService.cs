using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05.BUS
{
    public class StudentService
    {
     
        public List<Student> GetAll()
        {
            Model1 context = new Model1();
            return context.Students.ToList();
        }
        public List<Student> GetAllHasNoMajor()
        {
            Model1 context = new Model1();
            return context.Students.Where(p => p.MajorID == null).ToList();

        }
        public List <Student> GetAllHasNoMinor(int facultyID) {
            Model1 context = new Model1();
            return context.Students.Where(p=>p.MajorID == null && p.FacultyID == facultyID).ToList().ToList();

        }
        public Student FinBtID(string studentId)
        {
            Model1 context = new Model1();
            return context.Students.FirstOrDefault(p => p.StudentID == studentId);
        }

        public void InsertUpdate (Student s)
        {
            Model1 context = new Model1 ();
            context.Students.AddOrUpdate(s);
            context.SaveChanges();
        }

        
        public void Delete ( string studentId)
        {
            using (var context = new Model1())
            {
                var student = context.Students.FirstOrDefault(s=>s.StudentID == studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
            }
        }

        public Student FinBtID(int studentId)
        {
            throw new NotImplementedException();
        }
    }
    

}
