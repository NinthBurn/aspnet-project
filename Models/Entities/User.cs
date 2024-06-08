namespace Lab10.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserLog> Logs { get; set; }
        public User() {
            Name = "";
            Email = "";
            Password = "";
        }
    }
}
