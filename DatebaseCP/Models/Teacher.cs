using System;

namespace DatebaseCP.Models
{
    internal class Teacher : Employees
    {
        public Teacher(string lastName, string firstName, string middleName, DateTime birthDate) : 
            base(lastName, firstName, middleName, birthDate)
        {
        }

        public Teacher(int id,string lastName, string firstName, string middleName, DateTime birthDate) :
            base(id, lastName, firstName, middleName, birthDate)
        {
        }
    }
}
