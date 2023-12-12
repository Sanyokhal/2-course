using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        List<Komposlugy> komposlugy = new List<Komposlugy>
        {
            new Komposlugy { surname = "Іванов", address = "Адреса1", date = DateTime.Now, op_type = "Послуга1", sum = 100 },
            new Komposlugy { surname = "Петров", address = "Адреса2", date = DateTime.Now, op_type = "Послуга2", sum = 200 },
        };

        List<Oplata> oplata = new List<Oplata>
        {
            new Oplata { surname = "Іванов", operation = "Послуга1", sum = 50, payment_date = DateTime.Now },
            new Oplata { surname = "Петров", operation = "Послуга2", sum = 200, payment_date = DateTime.Now },
        };

        // a) Середні сплати за задану послугу у поточному місяці усіх жильців у заданому місті
        string city = "Київ";
        string operation = "Послуга1";
        var avarega_operation = from k in komposlugy
                            join o in oplata on k.surname equals o.surname
                            where k.address.Contains(city) && k.op_type == operation &&
                                  o.payment_date.Month == DateTime.Now.Month
                            group o by o.operation into groupa
                            select new
                            {
                                operation = groupa.Key,
                                average = groupa.Average(o => o.sum)
                            };
        foreach (var item in avarega_operation)
        {
            Console.WriteLine($"Середня сума для послуги '{item.operation}' у поточному місяці: {item.average}");
        }

        // b) Назва послуги, за яку загалом нараховано найбільшу суму за останній квартал
        var maxSum = (from k in komposlugy
                             where k.date >= DateTime.Now.AddMonths(-3)
                             group k by k.op_type into groupb
                             select new
                             {
                                 operation = groupb.Key,
                                 overallSum = groupb.Sum(k => k.sum)
                             }).OrderByDescending(x => x.overallSum).FirstOrDefault();
        Console.WriteLine($"Назва послуги з найбільшою загальною сумою за останній квартал: {maxSum?.operation}");

        // c) Список прізвищ боржників у порядку спадання їх сумарної заборгованості по усім послугам
        var payers = (from k in komposlugy
                        join o in oplata on k.surname equals o.surname
                        group new { k, o } by k.surname into groupc
                      select new
                        {
                            surname = groupc.Key,
                            payment_total = groupc.Sum(x => x.o.sum) - groupc.Sum(x => x.k.sum)
                        }).Where(x => x.payment_total < 0).OrderByDescending(x => x.payment_total).Select(x=>x.surname).ToList();
        Console.WriteLine("Список прізвищ боржників у порядку спадання їх заборгованості:");
        foreach (var payer in payers)
        {
            Console.WriteLine(payer);
        }
    }
}

class Komposlugy
{
    public string surname { get; set; }
    public string address { get; set; }
    public DateTime date { get; set; }
    public string op_type { get; set; }
    public decimal sum { get; set; }
}

class Oplata
{
    public string surname { get; set; }
    public string operation { get; set; }
    public decimal sum { get; set; }
    public DateTime payment_date { get; set; }
}
