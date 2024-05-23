namespace DiplomaAPI.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
