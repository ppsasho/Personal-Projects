namespace Models
{
    public class StandardUser : User
    {
        public StandardUser(string firstName, string lastName, string username, string password) : base(firstName, lastName, username, password, Enums.AccountType.Standard )
        {
        }
    }
}
