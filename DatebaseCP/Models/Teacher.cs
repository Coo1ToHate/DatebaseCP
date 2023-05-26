using System;
using System.Collections.Generic;

namespace DatebaseCP.Models
{
    internal class Teacher : Employees
    {
        public Teacher()
        {
        }

        public Teacher(string lastName, string firstName, string middleName, DateTime birthDate, int titleId) : 
            base(lastName, firstName, middleName, birthDate)
        {
            TitleId = titleId;
        }

        public Teacher(int id,string lastName, string firstName, string middleName, DateTime birthDate, int titleId) :
            base(id, lastName, firstName, middleName, birthDate)
        {
            TitleId = titleId;
        }

        public int TitleId { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Degree> Degrees { get; set; } = new List<Degree>();
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
