namespace Models
{
    public class PremiumUser : User
    {
        public PremiumUser(string firstName, string lastName, string username, string password) : base(firstName, lastName, username, password, Enums.AccountType.Premium)
        {
        }
    }
}
