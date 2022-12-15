namespace DatebaseCP.Models
{
    internal class FormOfEducation
    {
        public FormOfEducation()
        {
        }

        public FormOfEducation(string name)
        {
            Name = name;
        }

        public FormOfEducation(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
