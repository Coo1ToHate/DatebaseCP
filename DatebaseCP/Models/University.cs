using System.Collections.ObjectModel;
using System.Data;

namespace DatebaseCP.Models
{
    internal class University
    {
        public University(string name)
        {
            Name = name;
            Groups = new ObservableCollection<Group>();
            TeachersTable = new DataTable();
        }

        public string Name { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public DataTable TeachersTable { get; set; }
    }
}
