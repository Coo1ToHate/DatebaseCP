using System;
using System.Collections.Generic;

namespace DatebaseCP.Models
{
    internal class Teacher : Employees
    {
        public Teacher()
        {
        }

        public Teacher(string lastName, string firstName, string middleName, DateTime birthDate, int titleId, int degreeId) : 
            base(lastName, firstName, middleName, birthDate)
        {
            TitleId = titleId;
            DegreeId = degreeId;
        }

        public Teacher(int id,string lastName, string firstName, string middleName, DateTime birthDate, int titleId, int degreeId) :
            base(id, lastName, firstName, middleName, birthDate)
        {
            TitleId = titleId;
            DegreeId = degreeId;
        }

        public int TitleId { get; set; }
        public int DegreeId { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
