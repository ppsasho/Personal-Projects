namespace Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public Character(string name, int health, int strength, int agility, string race)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Agility = agility;
            Race = race;
        }
        public string Stats()
        {
            return $"{Health} HP, {Strength} STR, {Agility} AGI";
        }
        public string CharName()
        {
            return $"{Name} ( {Race} ) the {Class}";
        }
    }
}
