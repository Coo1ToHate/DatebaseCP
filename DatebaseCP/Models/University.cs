using System.Collections.ObjectModel;

namespace DatebaseCP.Models
{
    internal class University
    {
        public University(string name)
        {
            Name = name;
            Groups = new ObservableCollection<Group>();
        }

        public string Name { get; set; }
        //public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
    }
}
