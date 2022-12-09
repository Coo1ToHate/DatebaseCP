namespace DatebaseCP.Models
{
    internal class TeacherDegree
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TeacherDegree(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
