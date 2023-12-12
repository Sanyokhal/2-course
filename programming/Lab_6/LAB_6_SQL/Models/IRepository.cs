using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6_SQL.Models
{
    internal interface IRepository<T>
    {
        List<T> GetAll();
    }
}
