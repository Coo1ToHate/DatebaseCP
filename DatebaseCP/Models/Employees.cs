using System;

namespace DatebaseCP.Models
{
    internal abstract class Employees
    {
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        protected Employees(string lastName, string firstName, string middleName, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }

        protected Employees(int id, string lastName, string firstName, string middleName, DateTime birthDate)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }
    }
}
