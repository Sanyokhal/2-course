using mkr_2.models;
using System;


//Необхідно розробити програму для симуляції роботи транспортної системи
//міста. Місто має різні види транспорту, такі як автобуси, таксі та
//електросамокати. Створіть базовий інтерфейс з функціями обчислення
//обчислення вартості проїзду і реалізуйте різні підкласи для кожного виду
//транспорту: ціна проїзду в автобусі фіксована, вартість в таксі розраховується як
//добуток відстані на вартість кілометру, а вартість електросамокату – рівна
//добутку часу поїздки на вартість хвили (вважати швидкість самокату сталою)
//Користувач обирає вид транспорту та відстань до місця призначення. Вивести
//ціну проїзду.

// Для цього завдання я б використав шаблон Стратегія
namespace mkr_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Виберіть метод транспортування");
                Console.WriteLine("1 - Автобус (12 грн)");
                Console.WriteLine("2 - Таксі (20₴/1км)");
                Console.WriteLine("3 - Самокат (5₴/1хв)");
                Console.WriteLine("4 - Вийти");
        
            double select = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введіть дистанцію поїздки в км : ");
            int distance = Convert.ToInt32(Console.ReadLine());
            if (select == 1)
            {
                var busContext = new TransportationContext(new BusStrategy());
                Console.WriteLine("Ціна проїзду на автобусі: " + busContext.CalculateCost(distance) + "₴");
            } else if (select == 2)
            {
                var taxiContext = new TransportationContext(new TaxiStrategy());
                Console.WriteLine("Ціна проїзду на таксі: " + taxiContext.CalculateCost(distance) + "₴");
            } else if (select == 3)
            {
                var scooterContext = new TransportationContext(new ScooterStrategy());
                Console.WriteLine("Ціна проїзду на електросамокаті: " + scooterContext.CalculateCost(distance) + "₴");
            }
            else if(select == 4)
            {
                return;
            }
            else
            {
                Console.WriteLine("Хибний ввід");
            }
            }
        }
    }
}