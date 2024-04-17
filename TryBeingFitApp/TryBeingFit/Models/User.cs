using Models.Enums;

namespace Models
{
    public abstract class User
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        private string Password { get; set; }
        public AccountType AccountType { get; set; }

        public User(string firstName, string lastName, string username, string password, AccountType accountType)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            SetPassword(password);
            AccountType = accountType;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password can't be empty.");
            if (password.Length < 6) throw new ArgumentException("Password length isn't within the minimal range! (6 characters).");
            if (!password.Any(x => char.IsNumber(x))) throw new ArgumentException("Password must contain at least one number.)");
            Password = password;
        }
        public bool CheckPassword(string password)
        {
            return Password.Equals(password);
        }
        public string Account()
        {
            return $"{FirstName} {LastName} [user: {Username}] - {AccountType} User";
        }
    }
}
