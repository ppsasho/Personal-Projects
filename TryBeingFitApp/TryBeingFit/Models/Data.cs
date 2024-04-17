namespace Models
{
    public static class Data
    {
        public static List<User> Users = new();
        public static List<Trainer> Trainers = new();
        public static List<VideoTraining> VideoTrainings = new();
        public static List<LiveTraining> LiveTrainings = new();
        static Data() 
        {
            LoadItems();
        }
        public static void AddLive(LiveTraining liveTraining)
        {
            if (LiveTrainings.Any())
            {
                int max = LiveTrainings.Max(x => x.Id);
                liveTraining.Id = max + 1;
            } else
            {
                liveTraining.Id = 1;
            }
            LiveTrainings.Add(liveTraining);
        }
        public static void LoadItems()
        {
            LiveTrainings = new List<LiveTraining> {};
            VideoTrainings = new List<VideoTraining>()
            {
                new("https://www.youtube.com/watch?v=abcdefg12345", "Upper Body workout\t", 1),
                new("https://www.youtube.com/watch?v=qwerty09876", "Lower Body workout\t", 2),
                new("https://www.youtube.com/watch?v=zyxwvuts7654", "Core Body workout\t", 3),
                new("https://www.youtube.com/watch?v=0987poiuytre", "Cardio workout\t\t", 4),
                new("https://www.youtube.com/watch?v=mnbvcxz43210", "Flexibility workout\t", 5)
            };
            Trainers = new List<Trainer>()
            {
                new("Brad", "Schoenfeld", "bschoen", "bschoen1"),
                new("Matt", "Roberts", "mroberts", "mroberts1"),
                new("Louise", "Parker", "lparker", "lparker1"),
                new("Shaun", "Stafford", "sstafford", "sstafford1")
            };
            Users = new List<User>()
            {
                new StandardUser("Sasho", "Popovski", "ppsasho", "ppsasho1"),
            };
        }
    }
}
