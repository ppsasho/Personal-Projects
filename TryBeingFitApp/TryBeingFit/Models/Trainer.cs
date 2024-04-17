namespace Models
{
    public class Trainer : User
    {
        public Trainer(string firstName, string lastName, string username, string password) : base(firstName, lastName, username, password, Enums.AccountType.Trainer)
        {
        }
    }
}
