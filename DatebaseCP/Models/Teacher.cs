using System;
using System.Collections.ObjectModel;

namespace DatebaseCP.Models
{
    internal class Teacher : Employees
    {
        public Teacher(string lastName, string firstName, string middleName, DateTime birthDate, TeacherTitle title, TeacherDegree degree) : 
            base(lastName, firstName, middleName, birthDate)
        {
            Title = title;
            Degree = degree;
            Posts = new ObservableCollection<TeacherPost>();
        }

        public Teacher(int id,string lastName, string firstName, string middleName, DateTime birthDate, TeacherTitle title, TeacherDegree degree) :
            base(id, lastName, firstName, middleName, birthDate)
        {
            Title = title;
            Degree = degree;
            Posts = new ObservableCollection<TeacherPost>();
        }

        public TeacherTitle Title { get; set; }
        public ObservableCollection<TeacherPost> Posts { get; set; }
        public TeacherDegree Degree { get; set; }
    }
}
