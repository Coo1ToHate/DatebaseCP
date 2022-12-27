namespace DatebaseCP.Models
{
    internal class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public Post(bool isSelected = false)
        {
            IsSelected = isSelected;
        }

        public Post(int id, string name, bool isSelected = false) : this(isSelected)
        {
            Id = id;
            Name = name;
        }
    }
}
