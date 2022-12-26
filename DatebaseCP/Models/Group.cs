using System.Collections.ObjectModel;

namespace DatebaseCP.Models
{
    internal class Group
    {
        public Group()
        {
        }

        public Group(string name, int specialityId, int formOfEducationId)
        {
            Name = name;
            SpecialityID = specialityId;
            FormOfEducationID = formOfEducationId;
        }

        public Group(int id, string name, int specialityId, int formOfEducationId) : this(name, specialityId, formOfEducationId)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SpecialityID { get; set; }

        public int FormOfEducationID { get; set; }
    }
}
