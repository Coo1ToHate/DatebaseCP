using System;

namespace DatebaseCP.Models
{
    internal class Student : Employees
    {
        public int GroupId { get; set; }
        /// <summary>
        /// Номер зачетной книжки
        /// </summary>
        public string RecordBook { get; set; }

        public Student()
        {
        }

        public Student(string lastName, string firstName, string middleName, DateTime birthDate, int groupId, string recordBook) : base(lastName, firstName, middleName, birthDate)
        {
            GroupId = groupId;
            RecordBook = recordBook;
        }

        public Student(int id, string lastName, string firstName, string middleName, DateTime birthDate, int groupId, string recordBook) : base(id, lastName, firstName, middleName, birthDate)
        {
            GroupId = groupId;
            RecordBook = recordBook;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
