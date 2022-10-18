using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp6
{
    public class Car
    {
        public string model { get; set; }
        public string number { get; set; }
        public string color { get; set; }
        public string producing_country { get; set; }
        public string date_last_inspection { get; set; }

        public Car(string model, string number, string color, string producing_country, string date_last_inspection)
        {
            this.model = model;
            this.number = number;
            this.color = color;
            this.producing_country = producing_country;
            this.date_last_inspection = date_last_inspection;
        }

        public Car()
        {

        }

        public override string ToString()
        {
            return $"Car: модель:'{model}',номер:{number},колір:{color},країна-виробник{producing_country},дата останнього техогляду:{date_last_inspection}";
        }
    }
    public class Cars
    {
        public List<Car> cars = new List<Car>();

        public Cars()
        {

        }

        public void Add(string model, string number, string color, string producing_country, string date_last_inspection)
        {
            cars.Add(new Car(model, number, color, producing_country, date_last_inspection));
        }
        public void CreatePO(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cars));
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (fs)
            {
                serializer.Serialize(fs, this);
            }
        }

        public void ReadPO(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cars));
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (fs)
            {
                Cars obj = (Cars)serializer.Deserialize(fs);
                this.cars = obj.cars;
            }
        }
        public void Search(string other_color, string other_producing_country)
        {
            this.cars = this.cars.Where(elem => elem.color == other_color && elem.producing_country == other_producing_country).ToList();
        }
        class Program
        {
            static void Main(string[] args)
            {
                /*Інформація про машини включає в себе: модель, номер, колір, країна-виробник, дата останнього техогляду. 
                 * a.Сформувати xml-файл, який би містив інформацію щонайменше про 10 машин. 
                 * b.Організувати пошук за кольором та країною виробником. 
                 * c.Вивести статистичну інформацію про відповідну кількість машин для кожної наявної моделі. */
                string fileName = @"D:\Проекти з Антосяком\Julia\labka2\labka2\XMLFile1.xml";

                Cars cars = new Cars();
                cars.Add("Audi","BB4177CH","gray","Україна", "22.03.2017");
                cars.Add("BMW", "BA2588BA", "white", "Канада", "25.01.2018");
                cars.Add("Tesla", "BC6040CP", "black", "США", "14.11.2018");
                cars.Add("Mazda", "AE8181EX", "black", "США", "31.07.2018");
                cars.Add("Tesla", "BT6026AX", "blue", "Ірландія", "22.01.2019");
                cars.Add("Toyota", "BB5243CH", "lightgrey", "Нідерланди", "29.03.2017");

                cars.CreatePO(fileName);

                Cars cars2 = new Cars();
                cars2.ReadPO(fileName);
                cars2.Search("black", "США");

                foreach (Car car in cars2.cars)
                {
                    Console.WriteLine(car.ToString());
                }
                Console.WriteLine();

                var task = cars.cars
                .GroupBy(group => $"{group.model}")
                .Select(item => new { item.Key, Value = item.Count() });

                foreach (var item in task)
                {
                    Console.WriteLine($"Model: {item.Key}, numbers cars: {item.Value}");
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}