namespace Models
{
    public static class CMD
    {
        public static string GetInput(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string input = Console.ReadLine();
                if (input != "") return input;
                Console.WriteLine("Input is invalid, please try again.(don't leave empty fields!)");
                continue;
            }
        }
        public static int RandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 11);
        }
        public static bool CheckHealth(Character character)
        {
            if (character.Health > 0) return true;
            return false;
        }
        public static bool GameEnd(string msg, User user)
        {
            Console.WriteLine(msg);
            switch (GetInput("Do you want to start a new game? (Y N)").ToUpper())
            {
                case "Y":
                    return UserLogin(user);
                case "N":
                default: return false;
            }
        }
        public static void Fight(string msg, User user, int health)
        {
            while (true)
            {
                Console.WriteLine(msg);
                switch (GetInput("Do you want to\n1) Fight\n2) Run Away\n"))
                {
                    case "1":
                        if (RandomNumber() < user.Character.Strength)
                        {
                            Console.Clear();
                            Console.WriteLine("You won the fight!");
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("You lost the fight!");
                        user.Character.Health -= health;
                        Console.WriteLine($"{user.Character.Name} - {user.Character.Health} HP");
                        break;

                    case "2":
                        if (RandomNumber() < user.Character.Agility)
                        {
                            Console.Clear();
                            Console.WriteLine("You escaped!");
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("You couldn't escape!");
                        user.Character.Health -= health;
                        Console.WriteLine($"{user.Character.Name} - {user.Character.Health} HP");
                        break;
                    default:
                        Console.WriteLine("Please check you enter either 1 or 2");
                        continue;
                }
                break;
            }
        }


        public static bool Journey(User user)
        {
            string lose = "YOU LOST";

            Fight("Bandits attack you out of nowhere.They seem very dangerous...", user, 20);
            if (!CheckHealth(user.Character)) return GameEnd(lose, user);
            Fight("You bump in to one of the guards of the nearby village. They attack you without warning...", user, 30);
            if (!CheckHealth(user.Character)) return GameEnd(lose, user);
            Fight("A Land Shark appears. It starts chasing you down to eat you...", user, 50);
            if (!CheckHealth(user.Character)) return GameEnd(lose, user);
            Fight("You accidentally step on a rat. His friends are not happy. They attack...", user, 10);
            if (!CheckHealth(user.Character)) return GameEnd(lose, user);
            Fight("You find a huge rock. It comes alive somehow and tries to smash you...", user, 30);
            if (!CheckHealth(user.Character)) return GameEnd(lose, user);

            return GameEnd("YOU WON", user);
        }
        public static bool UserLogin(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {user.Username}!");
                string charName = GetInput("Enter a name for your character: ");
                if (!(charName.Length > 1 || charName.Length < 20))
                {
                    Console.WriteLine("Please check the entered name isn't longer than 20 characters \n or shorter than 2.");
                    continue;

                } while (true)
                {
                    Console.WriteLine("1) Dwarf\n*\tHas 100 Health\n*\tHas 6 Strength\n*\tHas 2 Agility");
                    Console.WriteLine("2) Elf\n*\tHas 60 Health\n*\tHas 4 Strength\n*\tHas 6 Agility");
                    Console.WriteLine("3) Human\n*\tHas 80 Health\n*\tHas 5 Strength\n*\tHas 4 Agility");
                    switch (GetInput("Choose from the 3 races above:"))
                    {
                        case "1":
                            user.Character = new Dwarf(charName, 100, 6, 2);
                            Console.WriteLine(user.Character.Stats());
                            break;

                        case "2":
                            user.Character = new Elf(charName, 60, 4, 6);
                            Console.WriteLine(user.Character.Stats());
                            break;

                        case "3":
                            user.Character = new Human(charName, 80, 5, 4);
                            Console.WriteLine(user.Character.Stats());
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Please check you entered one of the numbers.");
                            continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.WriteLine("1) Warrior\n*\t+20 Health\n*\t-1 Agility");
                    Console.WriteLine("2) Rogue\n*\t-20 Health\n*\t+1 Agility");
                    Console.WriteLine("3) Mage\n*\t+20 Health\n*\t-1 Strength");
                    switch (GetInput("Choose from the 3 classes above:"))
                    {
                        case "1":
                            user.Character.Class = "Warrior";
                            user.Character.Health += 20;
                            user.Character.Agility -= 1;
                            break;

                        case "2":
                            user.Character.Class = "Rogue";
                            user.Character.Health -= 20;
                            user.Character.Agility += 1;
                            break;

                        case "3":
                            user.Character.Class = "Mage";
                            user.Character.Health += 20;
                            user.Character.Strength -= 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Please check you entered one of the numbers.");
                            continue;
                    }
                    break;
                }
                Console.Clear();
                Console.WriteLine("Character has been created successfully!");
                Console.WriteLine(user.Character.CharName());
                Console.WriteLine(user.Character.Stats());
                Console.WriteLine("Good luck on your journey adventurer!");

                return Journey(user);
            }
        }
    }
}
