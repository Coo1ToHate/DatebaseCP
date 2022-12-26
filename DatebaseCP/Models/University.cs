using System.Collections.ObjectModel;

namespace DatebaseCP.Models
{
    internal class University
    {
        public University(string name)
        {
            Name = name;
            Specialitys = new ObservableCollection<Speciality>();
            FormOfEducations = new ObservableCollection<FormOfEducation>();
            Groups = new ObservableCollection<Group>();
            Teachers = new ObservableCollection<Teacher>();
            TeachersTitle = new ObservableCollection<TeacherTitle>();
            Posts = new ObservableCollection<Post>();
            TeachersDegree = new ObservableCollection<TeacherDegree>();
        }

        public string Name { get; set; }
        public ObservableCollection<Speciality> Specialitys { get; set; }
        public ObservableCollection<FormOfEducation> FormOfEducations { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<TeacherTitle> TeachersTitle { get; set; }
        public ObservableCollection<Post> Posts { get; set; }
        public ObservableCollection<TeacherDegree> TeachersDegree { get; set; }

    }
}
