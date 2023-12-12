using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Створення XML-файлу "Auto.xml"
        XDocument xmlDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement("Autos",
                new XElement("Auto",
                    new XElement("Surname", "Іванов"),
                    new XElement("Plate", "AB123CD"),
                    new XElement("Mark", "МаркаX"),
                    new XElement("Price", 25000),
                    new XElement("Address", "Адреса1")
                ),
                new XElement("Auto",
                    new XElement("Surname", "Петров"),
                    new XElement("Plate", "XY777ZZ"),
                    new XElement("Mark", "МаркаY"),
                    new XElement("Price", 30000),
                    new XElement("Address", "Адреса2")
                )
            )
        );

        xmlDocument.Save("Auto.xml");
        Console.WriteLine("Файл 'Auto.xml' створено та збережено.");

        // Виводимо в консоль
        XDocument loadedDocument = XDocument.Load("Auto.xml");
        Console.WriteLine("Вміст файлу 'Auto.xml':");
        Console.WriteLine(loadedDocument);

        string search_mark = "МаркаY";

        // Марка = value та номерний знак має 7
        var cars_with_7 = loadedDocument.Root.Elements("Auto")
            .Where(auto => auto.Element("Mark").Value == search_mark &&
                            auto.Element("Plate").Value.Contains("7"))
            .Count();

        Console.WriteLine($"Кількість власників машини марки {search_mark}, " +
                          $"у номері яких є принаймні одна цифра 7: {cars_with_7}");

        // Загальна вартість усіх машин де марка = value
        var overallPrice = loadedDocument.Root.Elements("Auto")
            .Where(auto => auto.Element("Mark").Value == search_mark)
            .Sum(auto => (int)auto.Element("Price"));

        Console.WriteLine($"Загальна вартість усіх машин марки {search_mark}: {overallPrice}");
    }
}
