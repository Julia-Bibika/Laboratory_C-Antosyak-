using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace labka4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DaiTable table = new DaiTable();
            Dai car = new Dai("Бартків Олександр Михайлович", "Audi", "BB2588BA", "red");

            table.Save(car);

            Console.WriteLine(car.id);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            car.pib = "Кучер Іван Андрійович";
            table.Save(car);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            table.Remove(car);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            Dai car1 = table.GetById(1);
            Console.WriteLine(car1);

            Console.WriteLine(table.GetNumberOfCars("BMW","A"));

            Singleton.CloseConnection();
        }
    }
}
