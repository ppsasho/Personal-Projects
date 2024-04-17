using Models;
namespace Hero_s_Journey
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user1 = new("Dalvir", "examplemail1@gmail.com", "user1Password");
            User user2 = new("Nikola", "examplemail2@gmail.com", "user2Password");
            List<User> users = new() { user1, user2 };

            while (!UI(users)) ;
        }
        #region UI
        static bool UI(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Hero's Journey!");
            for (int i = 0; i <= 4; i++) 
            {
                string email = CMD.GetInput("Enter your email: ");
                if(!(email.Contains('@') && email.Contains('.')))
                {
                    Console.WriteLine(@"Please check your email contains an ""@"" and an ""."" ");
                    continue;

                } else {
                    string password = CMD.GetInput("Enter your password: ");
                    if(!users.Any(x => x.Email == email && x.CheckPassword(password))) {
                        Console.WriteLine("Incorrect username or password!");
                        continue;

                    } else {
                        User user = users.First(x => x.Email == email);
                        return CMD.UserLogin(user);
                    }
                }
            }
            Console.WriteLine("You failed to login in 5 tries! Please try again later.");
            return true;
        }
        #endregion
    }
}
