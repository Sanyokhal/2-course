using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab_4
{
    internal class EnoughFunds : IStateAccount
    {
        public void UseInternet(AccountContext account, int mb) // СТАН "ДОСТАТНЬО КОШТІВ"
        {
            if (account.packageConnected && account.remainingData >= mb)
            {
                account.remainingData -= mb;
                Console.WriteLine($"Використано {mb} МБ. Залишокк в пакеті: {account.remainingData} МБ");
            }
            else
            {
                int packages_needed = Convert.ToInt32(Math.Ceiling((mb - account.remainingData)/200.0));
                int money_needed = packages_needed * 7;
                if (account.balance >= money_needed) //Якщо пакет відключений
                {
                    account.balance -= money_needed;
                    Console.WriteLine($"Підключено {packages_needed} пакетів по 200МБ/7₴");
                    account.remainingData = (packages_needed * 200) - (mb - account.remainingData);
                    Console.WriteLine($"Залишок МБ після використання інтернету = {account.remainingData}");
                }
                else
                {
                    Console.WriteLine("Недостатня кількість коштів, для виконання даної операції");
                }
            }
        }
        public void MakeCall(AccountContext account, int min, string number)
        {
            double callCost = (min - account.remainingMinutes) * 0.60;
            if (account.packageConnected && account.remainingMinutes >= min)
            {
                account.remainingMinutes -= min;
                Console.WriteLine($"Телефона розмова з {number} на {min} хвилин. В пакеті залишилось: {account.remainingMinutes} хв");
            }
            else
            {
                if (account.balance >= callCost)
                {
                    account.balance -= callCost;
                    account.remainingMinutes = 0;
                    Console.WriteLine($"Оплачено {callCost}₴ за дзвінок {number}   *(Тариф 1хв/0.6₴)");
                }
                else
                {
                    Console.WriteLine("Недостатня кількість коштів, для виконання даної операції");
                }
            }
        }
        public void CheckPackage(AccountContext account)
        {
            if (account.balance >= 145)
            {
                account.remainingData = 10240;
                account.remainingMinutes = 200;
                account.packageConnected = true;
                account.balance -= 145; 
                Console.WriteLine($"Пакет підключно, тепер доступно {account.remainingMinutes}ХВ та {account.remainingData}Мб. Залишок на балансі {account.balance}₴. Тариф 145 грн - 10240МБ та 200хв");
            }
            else
            {
                Console.WriteLine("Недостатня кількість коштів, для підключення пакету послуг");
            }
        }
    }
}
