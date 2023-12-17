using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    internal class AccountContext
    {
        public IStateAccount currentState;
        public double balance;
        public bool packageConnected;
        public int remainingData;
        public int remainingMinutes;

        public double GetBalance() //Повертає баланс
        {
            return balance;
        }
        public double ResupplyBalance(int uah) //Поповнення балансу
        {
            balance += uah;
            return balance;
        }


        public AccountContext() //При початку стан = EnoughFunds
        {
            currentState = new EnoughFunds(); 
            balance = 200;
            packageConnected = false;
            remainingData = 0;
            remainingMinutes = 0;
        }
        public void UseInternet(int megabytes) // Використання інтернету
        {
            currentState.UseInternet(this, megabytes);
        }

        public void MakeCall(int minutes, string number) // Зробити дзвінок
        {
            currentState.MakeCall(this, minutes, number);
        }

        public void CheckPackage(int data, int minutes)// Перевірка пакету
        {
            currentState.CheckPackage(this);
        }
   

        public IStateAccount CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
    }
}
