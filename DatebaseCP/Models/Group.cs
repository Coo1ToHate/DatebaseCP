using System.Collections.ObjectModel;

namespace DatebaseCP.Models
{
    internal class Group
    {
        public Group(string name)
        {
            Students = new ObservableCollection<Student>();
            Name = name;
        }

        public Group(int id, string name) : this(name)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Student> Students { get; set; }

        public Student Groupleader { get; set; }
    }
}
