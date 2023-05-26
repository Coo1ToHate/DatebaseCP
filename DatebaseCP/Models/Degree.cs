namespace DatebaseCP.Models
{
    internal class Degree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public Degree(bool isSelected = false)
        {
            IsSelected = isSelected;
        }

        public Degree(int id, string name, bool isSelected = false): this(isSelected)
        {
            Id = id;
            Name = name;
        }
    }
}
