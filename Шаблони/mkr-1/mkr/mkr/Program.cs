using System;

// Для данної задачі доцільно використати Декоратор 
namespace mkr 
{
    public class Program
    {
        static void Main(string[] args)
        {
            ICharacter player = new Character("Саша", 100);

            player = new PunctureProtection(player, 20); // Захист від уколів на 20%
            player = new CutProtection(player, 15); // Захист від порізів на 15%
            player = new FixedProtection(player, 10); // Фіксований захист 10 НР

            player.showStats();
            player.dealDamage(30);

            player.showStats();
        }
    }

    public interface ICharacter // інтерфейс 
    {
        void dealDamage(int damage);
        void showStats();
    }

    public class Character: ICharacter //Базові дані героя
    {
        public string name { get; set; }
        public double HP { get; set; }

        public Character(string name, double HP) {
            this.name = name;
            this.HP = HP;
        }
        public void dealDamage(int damage)
        {
            this.HP-= damage;
            Console.WriteLine($"{this.name} отримав {damage} шкоди");
        }

        public void showStats()
        {
            Console.WriteLine($"Персонаж :{this.name} Життя: {this.HP}");
        }
    }
    public abstract class CharacterDecorator : ICharacter //декоратор
    {
        protected ICharacter character;

        public CharacterDecorator(ICharacter character)
        {
            this.character = character;
        }

        public virtual void dealDamage(int damage)
        {
            character.dealDamage(damage);
        }

        public virtual void showStats()
        {
            character.showStats();
        }
    }

    public class PunctureProtection : CharacterDecorator // Захист від уколів
    {
        private int protectionPercentage;

        public PunctureProtection(ICharacter character, int percentage)
            : base(character)
        {
            protectionPercentage = percentage;
        }

        public override void dealDamage(int damage)
        {
            int protectedDamage = damage - (damage * protectionPercentage / 100);
            base.dealDamage(protectedDamage);
        }

        public override void showStats()
        {
            base.showStats();
            Console.WriteLine($"Захист від уколів: {protectionPercentage}%");
        }
    }

    public class CutProtection : CharacterDecorator // захист від порізів
    {
        private int protectionPercentage;

        public CutProtection(ICharacter character, int percentage)
            : base(character)
        {
            protectionPercentage = percentage;
        }

        public override void dealDamage(int damage)
        {
            int protectedDamage = damage - (damage * protectionPercentage / 100);
            base.dealDamage(protectedDamage);
        }

        public override void showStats()
        {
            base.showStats();
            Console.WriteLine($"Захист від порізів: {protectionPercentage}%");
        }
    }


    public class FixedProtection : CharacterDecorator //Захист на фіксовану величину
    {
        private int protectionAmount;

        public FixedProtection(ICharacter character, int amount)
            : base(character)
        {
            protectionAmount = amount;
        }

        public override void dealDamage(int damage)
        {
            int protectedDamage = damage - protectionAmount;
            base.dealDamage(protectedDamage);
        }

        public override void showStats()
        {
            base.showStats();
            Console.WriteLine($"Фіксований захист: {protectionAmount}");
        }
    }


}