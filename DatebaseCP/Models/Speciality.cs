namespace DatebaseCP.Models
{
    internal class Speciality
    {
        public Speciality()
        {
        }

        public Speciality(string name)
        {
            Name = name;
        }

        public Speciality(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
