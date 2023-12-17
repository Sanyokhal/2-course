using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    internal interface IStateAccount
    {
        void UseInternet(AccountContext account, int mb);
        void MakeCall(AccountContext account, int minutes, string number);
        void CheckPackage(AccountContext account);
    }
}
