using Models.Enums;

namespace Models
{
    public static class CMD
    {
        public static void DefaultOption()
        {
            Console.Clear();
            Console.WriteLine("Please make sure you choose one of the options.");
        }
        public static string GetVideoTrainings()
        {
            string result = string.Empty;
            foreach (var video in Data.VideoTrainings) result += $"(ID: {video.Id})\t[{video.Title}]\n";
            return result;
        }

        public static string GetLiveTrainings(User user)
        {
            if (!Data.LiveTrainings.Any())
            {
                Console.WriteLine($"There are no live trainings scheduled, please check again later.");
                switch (user.AccountType)
                {
                    case AccountType.Standard:
                        StandardLogIn(user, Welcome(user));
                        break;
                    case AccountType.Premium:
                        PremiumLogIn(user, Welcome(user));
                        break;
                    case AccountType.Trainer:
                        TrainerLogIn(user, Welcome(user));
                        break;
                }
            }
            string result = string.Empty;
            foreach (var training in Data.LiveTrainings) result += $"\n(ID: {training.Id})\t[{training.Title}] {training.GetRemainingTime()}\n";
            return result;
        }
        public static bool MoreLiveInfo(User user)
        {
            while (true)
            {
                int id = GetNumber("Enter the id of the live training that you want to participate in:");
                if (!Data.LiveTrainings.Any(x => x.Id.Equals(id)))
                {
                    Console.WriteLine("The id you entered doesn't exist in the list of live trainings");
                    continue;
                }

                LiveTraining live = Data.LiveTrainings.First(x => x.Id.Equals(id));
                Console.WriteLine(live.GetInfo());
                switch (GetInput("Do you want to:\n1) Get added to the participants list?\n2) Go back to the Live trainings menu"))
                {
                    case "1":
                        Data.LiveTrainings[Data.LiveTrainings.IndexOf(live)].Participants.Add(user);
                        Console.Clear();
                        Console.WriteLine("You have been added to the live training successfully!");
                        return LiveTrain(user);
                    case "2":
                        Console.Clear();
                        return LiveTrain(user);
                    default:
                        DefaultOption();
                        continue;
                }

            }
        }
        public static bool LiveTrain(User user)
        {
            while (true)
            {
                Console.WriteLine(GetLiveTrainings(user));
                switch (GetInput("Would you like to:\n1) Participate in a live training\n2) Go back to your profile"))
                {
                    case "1":
                        return MoreLiveInfo(user);

                    case "2":
                        switch (user.AccountType)
                        {
                            case AccountType.Standard:
                                Console.Clear();
                                return StandardLogIn(user, Welcome(user));

                            case AccountType.Premium:
                                Console.Clear();
                                return PremiumLogIn(user, Welcome(user));

                            case AccountType.Trainer:
                                Console.Clear();
                                return TrainerLogIn(user, Welcome(user));
                        }
                        break;

                    default:
                        DefaultOption();
                        continue;
                }
            }
        }

        public static decimal GetRating(string msg)
        {
            while (true)
            {
                if (decimal.TryParse(GetInput(msg), out decimal number))
                {
                    if (number < 1 || number > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Please make sure your rating's not greater than 5 or lesser than 1");
                        continue;
                    }
                    return number;
                }
                Console.Clear();
                Console.WriteLine("Please make sure you enter a valid number!");
                continue;
            }
        }

        public static int GetNumber(string msg)
        {
            while (true)
            {
                if (int.TryParse(GetInput(msg), out int number)) return number;
                Console.Clear();
                Console.WriteLine("Please make sure to enter a number!");
                continue;
            }
        }
        public static DateTime GetDateTime(string msg)
        {
            while (true)
            {
                if (DateTime.TryParse(GetInput(msg), out DateTime dateTime))
                    if (dateTime > DateTime.Now) return dateTime;
                Console.Clear();
                Console.WriteLine("Please make sure to follow the format shown.\nAnd make sure the dates entered including time are somewhere in the future");
                continue;
            }
        }
        public static bool CreateLiveTraining(User trainer)
        {
            while (true)
            {
                string title = GetInput("Enter the title for your training:");
                string link = GetInput("Enter the link for your live training:");
                DateTime date = GetDateTime("Enter the date for your live training(dd/MM/yyyy HH:mm:ss):");

                LiveTraining live = new(link, title, date, trainer);
                Data.AddLive(live);
                switch (GetInput("Would you like to:\n1) Create another live training\n2) Go back to your profile"))
                {
                    case "1":
                        Console.Clear();
                        continue;
                    case "2":
                        Console.Clear();
                        return TrainerLogIn(trainer, Welcome(trainer));
                    default:
                        DefaultOption();
                        continue;
                }
            }
        }
        public static bool RescheduleTraining(User trainer)
        {
            while (true)
            {
                var trainerLiveTrainings = Data.LiveTrainings.Where(x => x.Trainer.Username.Equals(trainer.Username));
                if (!trainerLiveTrainings.Any())
                {
                    switch (GetInput("There are no live training made by you, would you like to:\n1) Create a live training\n2) Go back to your profile"))
                    {
                        case "1":
                            return CreateLiveTraining(trainer);
                        case "2":
                            return TrainerLogIn(trainer, Welcome(trainer));
                        default:
                            DefaultOption();
                            continue;
                    }
                }
                foreach (var item in trainerLiveTrainings) Console.WriteLine($"(ID {item.Id}) - {item.GetInfo()}");
                int id = GetNumber("Enter the Id of the training you want to reschedule:");
                if (!trainerLiveTrainings.Any(x => x.Id.Equals(id)))
                {
                    Console.Clear();
                    Console.WriteLine("Please check that you entered a valid Id!");
                    continue;
                }
                LiveTraining live = trainerLiveTrainings.First(x => x.Id.Equals(id));
                Data.LiveTrainings[Data.LiveTrainings.IndexOf(live)].ReSchedule(GetDateTime("Enter the new date for your training.\nPlease follow this format (dd/MM/yyyy HH:mm:ss):"));
                Console.WriteLine("Training successfully rescheduled");
                switch (GetInput("Would you like to:\n1) Reschula a live training again\n2) Go back to your profile"))
                {
                    case "1":
                        Console.Clear();
                        continue;

                    case "2":
                    default:
                        Console.Clear();
                        return TrainerLogIn(trainer, Welcome(trainer));
                }
            }
        }
        public static bool UI()
        {
            Console.WriteLine("Welcome to Try Being Fit!");
            switch (CMD.GetInput("Are you a:\n1) User\n2) Trainer"))
            {
                case "1":
                    Console.Clear();
                    switch (CMD.GetInput("Do you want to:\n1) Log in\n2) Register"))
                    {
                        case "1":
                            Console.Clear();
                            return CMD.SignIn("user");

                        case "2":
                            Console.Clear();
                            return CMD.Register();

                        default:
                            Console.Clear();
                            return CMD.DefaultMessage();
                    }
                case "2":
                    Console.Clear();
                    return CMD.SignIn("trainer");
                default:

                    return CMD.DefaultMessage();
            }
        }
        public static User UpgradeUser(User user)
        {
            user.AccountType = AccountType.Premium;
            Console.WriteLine("You were successfully upgraded to premium!");
            return user;
        }

        public static bool DefaultMessage()
        {
            DefaultOption();
            return false;
        }

        public static string GetInput(string msg)
        {
            while (true)
            {
                Console.Write($"{msg}\n\t");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) return input;
                Console.WriteLine("Please don't leave empty inputs!");
            }
        }
        public static string Welcome(User user)
        {
            return $"Welcome {user.FirstName}!";
        }

        public static bool StandardLogIn(User user, string welcome)
        {
            while (true)
            {
                Console.WriteLine(welcome);
                switch (GetInput("Would you like to:\n1) Train\n2) Upgrade to premium\n3) Account\n4) Log out"))
                {
                    case "1":
                        Console.Clear();
                        return VideoTrain(user);
                    case "2":
                        Console.Clear();
                        return PremiumLogIn(UpgradeUser(user), Welcome(user));
                    case "3":
                        Console.WriteLine(user.Account());
                        switch (GetInput("Would you like to:\n1) Go back\n2) Log out"))
                        {
                            case "1":
                                Console.Clear();
                                continue;
                            case "2":
                                Console.Clear();
                                return false;
                            default:
                                DefaultOption();
                                continue;
                        }
                    case "4":
                        Console.Clear();
                        return false;
                    default:
                        DefaultOption();
                        continue;
                }
            }
        }

        public static bool PremiumLogIn(User user, string welcome)
        {
            while (true)
            {
                Console.WriteLine(welcome);
                switch (GetInput("Would you like to:\n1) Train\n2) Account\n3) Log out"))
                {
                    case "1":
                        while (true)
                        {
                            switch (GetInput("Would you like to go to:\n1) Video trainings\n2) Live trainings"))
                            {
                                case "1":
                                    return VideoTrain(user);
                                case "2":
                                    return LiveTrain(user);
                                default:
                                    DefaultOption();
                                    continue;
                            }
                        }
                    case "2":
                        Console.WriteLine(user.Account());
                        switch (GetInput("Would you like to:\n1) Go back\n2) Log out"))
                        {
                            case "1":
                                Console.Clear();
                                continue;
                            case "2":
                                Console.Clear();
                                return false;
                            default:
                                DefaultOption();
                                continue;
                        }
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Thanks for using Try Being Fit!");
                        return false;
                    default:
                        DefaultOption();
                        continue;
                }
            }
        }
        public static bool StartLiveTraining(User trainer)
        {
            var trainerTrainings = Data.LiveTrainings.Where(x => x.Trainer.Username.Equals(trainer.Username)).ToList();
            while (true)
            {
                if (trainerTrainings.Count < 1)
                {
                    switch (GetInput("There's no live trainings scheduled by you, would you like to:\n1) Create a live training\n2) Go back to your profile"))
                    {
                        case "1":
                            Console.Clear();
                            return CreateLiveTraining(trainer);

                        case "2":
                            Console.Clear();
                            return CreateLiveTraining(trainer);

                        default:
                            DefaultOption();
                            continue;
                    }
                }
                Console.WriteLine(GetLiveTrainings(trainer));
                int id = GetNumber("Enter the id of the live training that you would like to start");
                if (!Data.LiveTrainings.Any(x => x.Id.Equals(id)))
                {
                    Console.Clear();
                    Console.WriteLine("The Id entered doesn't exist in the current list of live trainings, please try again.");
                    continue;
                }
                LiveTraining live = Data.LiveTrainings.First(x => x.Id.Equals(id));
                Console.WriteLine("Live training started.");
                Console.WriteLine(live.GetInfo());
                Data.LiveTrainings.Remove(live);
                switch (GetInput("Would you like to:\n1) Go back to your profile\n2) Start another live training"))
                {
                    case "1":
                        Console.Clear();
                        return TrainerLogIn(trainer, Welcome(trainer));
                    case "2":
                    default:
                        Console.Clear();
                        continue;
                }
            }
        }
        public static bool TrainerLogIn(User trainer, string welcome)
        {
            while (true)
            {
                Console.WriteLine(welcome);
                switch (GetInput("Would you like to:\n1) Create a live training\n2) Reschedule a live training\n3) Account\n4) Train\n5) Start a live training\n6) Log out"))
                {
                    case "1":
                        return CreateLiveTraining(trainer);
                    case "2":
                        return RescheduleTraining(trainer);
                    case "3":
                        Console.WriteLine(trainer.Account());
                        switch (GetInput("Would you like to:\n1) Go back\n2) Log out"))
                        {
                            case "1":
                                Console.Clear();
                                continue;
                            case "2":
                                Console.Clear();
                                return false;
                            default:
                                DefaultOption();
                                continue;
                        }
                    case "4":
                        switch (GetInput("Would you like to train using:\n1) Video trainings\n2) Live trainings "))
                        {
                            case "1":
                                return VideoTrain(trainer);
                            case "2":
                                return LiveTrain(trainer);
                            default:
                                DefaultOption();
                                continue;
                        }
                    case "5":
                        return StartLiveTraining(trainer);
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Thanks for using Try being fit!");
                        return UI();
                    default:
                        DefaultOption();
                        continue;
                }
            }
        }

        public static bool SignIn(string accountType)
        {
            while (true)
            {
                string username = GetInput("Enter your username:");
                if (username.Length < 6)
                {
                    Console.WriteLine("The username you entered is shorter than the minimal requirement(6 chrs)");
                    continue;
                }
                if (accountType == "trainer")
                {
                    if (!Data.Trainers.Any(x => x.Username == username))
                    {
                        Console.WriteLine("No trainers with that username exist!");
                        continue;
                    }
                }
                else if (!Data.Users.Any(x => x.Username == username.Trim()))
                {
                    Console.WriteLine("This username doesn't exist!");
                    continue;
                }

                while (true)
                {
                    string password = GetInput("Enter your password:");
                    if (accountType == "trainer")
                    {
                        if (Data.Trainers.Any(x => x.Username == username && x.CheckPassword(password)))
                        {
                            Trainer trainer = Data.Trainers.First(x => x.Username.Equals(username));
                            Console.Clear();
                            return TrainerLogIn(trainer, Welcome(trainer));
                        }
                    }
                    else if (Data.Users.Any(x => x.Username == username && x.CheckPassword(password)))
                    {
                        User user = Data.Users.First(x => x.Username.Equals(username));
                        switch (user.AccountType)
                        {
                            case AccountType.Standard:
                                return StandardLogIn(user, Welcome(user));
                            case AccountType.Premium:
                                return PremiumLogIn(user, Welcome(user));
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The password you entered is incorrect.");
                        continue;
                    }
                }

            }
        }

        public static bool Register()
        {
            while (true)
            {
                string firstName = GetInput("Enter your first name:");
                string lastName = GetInput("Enter your last name:");
                if (firstName.Length < 2 || lastName.Length < 2)
                {
                    Console.Clear();
                    Console.WriteLine("Your names must not be shorter than 2 chrs");
                    continue;
                }
                while (true)
                {
                    string username = GetInput("Enter your username:");
                    if (username.Length < 6)
                    {
                        Console.WriteLine("Your username must contain at least 6 chrs.");
                        continue;
                    }
                    else if (Data.Users.Any(x => x.Username == username))
                    {
                        Console.WriteLine("That username already exists!");
                        continue;
                    }
                    User user = new StandardUser(firstName, lastName, username, GetInput("Enter your new password (Make sure theres at least 6 characters including a number)"));
                    Data.Users.Add(user);
                    Console.WriteLine("User successfully created!");
                    switch (GetInput("Would you like to login with your new account? (Y N)").ToUpper())
                    {
                        case "Y":
                            Console.Clear();
                            return StandardLogIn(user, Welcome(user));
                        case "N":
                            Console.Clear();
                            return false;
                    }
                }

            }
        }
        public static bool MoreVideoInfo(VideoTraining video, User user)
        {
            while (true)
            {
                Console.WriteLine(video.GetInfo());
                switch (GetInput("\nWould you like to:\n1) Give the video a rating\n2) Go Back to all the videos"))
                {
                    case "1":
                        decimal rating = GetRating("Enter a rating for the video (1 to 5)");
                        int index = Data.VideoTrainings.IndexOf(video);
                        Data.VideoTrainings[index].ChangeRating(rating);
                        Console.Clear();
                        Console.WriteLine($"Thanks for taking the time to give ({video.Title}) a rating!");
                        return VideoTrain(user);
                    case "2":
                        Console.Clear();
                        return VideoTrain(user);
                    default:
                        DefaultOption();
                        continue;
                }
            }
        }
        public static bool VideoTrain(User user)
        {
            while (true)
            {
                Console.WriteLine("Videos:\n");
                Console.WriteLine(GetVideoTrainings());
                switch (GetInput("Would you like to: \n1) Train using one of the videos\n2) Go back to your profile"))
                {
                    case "1":
                        int id = GetNumber("Enter the id of the video that you want to see.");
                        if (!Data.VideoTrainings.Any(x => x.Id == id))
                        {
                            Console.Clear();
                            Console.WriteLine("Please make sure you enter one of the video id's shown below!");
                            continue;
                        }
                        else
                        {
                            VideoTraining video = Data.VideoTrainings.First(x => x.Id.Equals(id));
                            Console.Clear();
                            return MoreVideoInfo(video, user);
                        }
                    case "2":
                        switch (user.AccountType)
                        {
                            case AccountType.Standard:
                                return StandardLogIn(user, Welcome(user));
                            case AccountType.Premium:
                                return PremiumLogIn(user, Welcome(user));
                            case AccountType.Trainer:
                                return TrainerLogIn(user, Welcome(user));
                        }
                        break;
                    default:
                        DefaultOption();
                        continue;
                }
            }
        }
    }
}
