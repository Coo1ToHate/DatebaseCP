namespace DatebaseCP.Models
{
    class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Lesson()
        {
        }

        public Lesson(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
