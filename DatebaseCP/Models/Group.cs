namespace DatebaseCP.Models
{
    internal class Group
    {
        public Group()
        {
        }

        public Group(string name, int specialityId, int formOfEducationId, int curatorId)
        {
            Name = name;
            SpecialityId = specialityId;
            FormOfEducationId = formOfEducationId;
            CuratorId = curatorId;
        }

        public Group(int id, string name, int specialityId, int formOfEducationId, int curatorId) : this(name, specialityId, formOfEducationId, curatorId)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SpecialityId { get; set; }

        public int FormOfEducationId { get; set; }

        public int CuratorId { get; set; }
    }
}
