using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    internal class Blocked : IStateAccount
    {
        public void UseInternet(AccountContext account, int mb) // СТАН "Заблоковано"
        {
            Console.WriteLine("Доступ до використання інтернету - заблокований");
        }
        public void MakeCall(AccountContext account, int min, string number)
        {
            Console.WriteLine("Доступ до використання дзвінків - заблокований");
        }
        public void CheckPackage(AccountContext account)
        {
            Console.WriteLine("Доступ до підключення пакету - заблокований");
        }
    }
}
