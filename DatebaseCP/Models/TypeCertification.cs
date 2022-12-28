namespace DatebaseCP.Models
{
    class TypeCertification
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeCertification()
        {
        }

        public TypeCertification(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
