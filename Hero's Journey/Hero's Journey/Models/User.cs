namespace Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email {  get; set; }
        protected string Password { get; set; }
        public Character Character { get; set; }
        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }
        public bool CheckPassword(string password)
        {
            return Password.Equals(password);
        } 
    }
}
