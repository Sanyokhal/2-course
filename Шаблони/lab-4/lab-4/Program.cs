using System;

namespace lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AccountContext account = new AccountContext();
            while (true)
            {
                Console.WriteLine("Оберіть операцію:");
                Console.WriteLine("A. Використати інтернет");
                Console.WriteLine("B. Зателефонувати");
                Console.WriteLine("C. Переглянути баланс");
                Console.WriteLine("D. Підключити пакет послуг (145 грн - 10240МБ та 200хв)");
                Console.WriteLine("E. Поповнити баланс");
                Console.WriteLine("F. Вийти");
                Console.WriteLine("");
                string select = Console.ReadLine().ToLower();
                if(select == "a")
                {
                    Console.Write("Введіть кількість МБ : ");
                    if(Int32.TryParse(Console.ReadLine(), out int mb))
                    {
                        account.CurrentState.UseInternet(account,mb);
                    }
                    else
                    {
                        Console.WriteLine("Введено хибні дані");
                    }
                }else if(select == "b")
                {
                    Console.Write("Введіть кількість хвилин : ");
                    if (Int32.TryParse(Console.ReadLine(), out int min))
                    {
                        Console.Write("Введіть номер телефона : ");
                        string num = Console.ReadLine();
                        account.CurrentState.MakeCall(account,min, num);
                    }
                    else
                    {
                        Console.WriteLine("Введено хибні дані");
                    }
                }
                else if(select == "c")
                {
                    Console.WriteLine($"Залишок коштів на балансі = {account.GetBalance()}₴");
                }else if(select == "d")
                {
                    Console.WriteLine("Підключення пакету...");
                    account.CurrentState.CheckPackage(account);
                }
                else if (select == "e")
                {
                    Console.Write("Поповнити баланс на : ");
                    int uah = Int32.Parse(Console.ReadLine());
                    Console.WriteLine($"Баланс поповнено на {uah}₴. Залишок коштів на балансі {account.ResupplyBalance(uah)}");
                }
                else if(select == "f") {
                    Console.WriteLine("До скорої зусрічі");
                    return;

                }else
                {
                    Console.WriteLine("Хибний вибір");
                }
            }
        }
    }
}