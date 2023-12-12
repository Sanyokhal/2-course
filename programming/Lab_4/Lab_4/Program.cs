using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        // Створення JSON-файлу "Auto.json"
        var autoData = new[]
        {
            new
            {
                surname = "Іванов",
                plate = "AB123CD",
                mark = "МаркаX",
                price = 25000,
                address = "Адреса1"
            },
            new
            {
                surname = "Петров",
                plate = "XY777ZZ",
                mark = "МаркаY",
                price = 30000,
                address = "Адреса2"
            },
        };

        string jsonData = JsonConvert.SerializeObject(autoData, Formatting.Indented);
        File.WriteAllText("Auto.json", jsonData);
        Console.WriteLine("Файл 'Auto.json' створено та збережено.");

        string loadedJson = File.ReadAllText("Auto.json");
        Console.WriteLine("Вміст файлу 'Auto.json':");
        Console.WriteLine(loadedJson);

        string searchedMark = "МаркаY";

        //Кількість власників машини марки Х, у номері яких є принаймні одна цифра 7
        var autoArray = JArray.Parse(loadedJson);
        var кількістьМашинЗСімкою = autoArray.Count(auto =>
            (string)auto["mark"] == searchedMark &&
            ((string)auto["plate"]).Contains("7")
        );

        Console.WriteLine($"Кількість власників машини марки {searchedMark}, " +
                          $"у номері яких є принаймні одна цифра 7: {кількістьМашинЗСімкою}");

        //Загальна вартість усіх машин марки Х
        var overallPrice = autoArray
            .Where(auto => (string)auto["mark"] == searchedMark)
            .Sum(auto => (int)auto["price"]);

        Console.WriteLine($"Загальна вартість усіх машин марки {searchedMark}: {overallPrice}");
    }
}
