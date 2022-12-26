namespace DatebaseCP.Models
{
    internal class TeacherTitle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TeacherTitle()
        {
        }

        public TeacherTitle(int id, string title)
        {
            Id = id;
            Name = title;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
