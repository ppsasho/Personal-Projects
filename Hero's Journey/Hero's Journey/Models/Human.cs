namespace Models
{
    public class Human : Character
    {
        public Human(string name, int health, int strength, int agility, string race = "Human") : base(name, health, strength, agility, race)
        {

        }
    }
}
