using System;

namespace DatebaseCP.Models
{
    internal class Student : Employees
    {
        public Student()
        {
        }

        public Student(string lastName, string firstName, string middleName, DateTime birthDate) : base(lastName, firstName, middleName, birthDate)
        {
        }

        public Student(int id, string lastName, string firstName, string middleName, DateTime birthDate) : base(id, lastName, firstName, middleName, birthDate)
        {
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
