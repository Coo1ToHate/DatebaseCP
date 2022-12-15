using System.Collections.ObjectModel;

namespace DatebaseCP.Models
{
    internal class Group
    {
        public Group()
        {
        }

        public Group(string name, Speciality speciality, FormOfEducation formOfEducation)
        {
            Students = new ObservableCollection<Student>();
            Name = name;
            Speciality = speciality;
            FormOfEducation = formOfEducation;
        }

        public Group(int id, string name, Speciality speciality, FormOfEducation formOfEducation) : this(name, speciality, formOfEducation)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Speciality Speciality { get; set; }

        public FormOfEducation FormOfEducation { get; set; }

        public ObservableCollection<Student> Students { get; set; }

        //public Student Groupleader { get; set; }
    }
}
