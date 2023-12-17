using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    internal class Limited : IStateAccount
    {
        public void UseInternet(AccountContext account, int mb) // СТАН "Обмежений"
        {
            if (account.packageConnected && account.remainingData >= mb)
            {
                account.remainingData -= mb;
                Console.WriteLine($"Використано {mb} МБ. Залишокк в пакеті: {account.remainingData} МБ");
            }
            else
            {
                Console.WriteLine("Лімітоване підключення до інтернету, будь-ласка підключіть пакет");
            }
        }
        public void MakeCall(AccountContext account, int min, string number)
        {
            if (account.packageConnected && account.remainingMinutes >= min)
            {
                account.remainingMinutes -= min;
                Console.WriteLine($"Телефона розмова з {number} на {min} хвилин. В пакеті залишилось: {account.remainingMinutes} хв");
            }
            else
            {
                Console.WriteLine("Лімітовані дзвінки, будь-ласка підключіть пакет");
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
