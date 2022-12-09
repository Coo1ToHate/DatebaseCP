namespace DatebaseCP.Models
{
    internal class TeacherPost
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TeacherPost(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
