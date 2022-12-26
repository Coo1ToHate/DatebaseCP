using System;

namespace DatebaseCP.Models
{
    internal class Student : Employees
    {
        public int GroupId { get; set; }

        public Student()
        {
        }

        public Student(string lastName, string firstName, string middleName, DateTime birthDate, int groupId) : base(lastName, firstName, middleName, birthDate)
        {
            GroupId = groupId;
        }

        public Student(int id, string lastName, string firstName, string middleName, DateTime birthDate, int groupId) : base(id, lastName, firstName, middleName, birthDate)
        {
            GroupId = groupId;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
