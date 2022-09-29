using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp5
{
    public class Factory
    {
        protected int id;
        public int Id { get { return id; } set { id = value; } }
        protected string surname;
        public string Surname { get { return surname; } set{surname = value;} }
        protected string name;
        public string Name { get { return name; } set { name = value; } }
        protected string patronymic;
        public string Patronymic { get { return patronymic; } set {patronymic = value;} }
        protected int workshop_number;
        public int Workshop_number { get { return workshop_number; } set { workshop_number = (value >= 0 ? value : 0); } }
        protected string position;
        public string Position { get {return position;} set {position = value;} }
        protected int work_exp;
        public int Work_exp { get { return work_exp; } set { work_exp = (value >= 0 ? value : 0); } }
        protected float salary;
        public float Salary { get {return salary;} set { salary = (value >= 0 ? value : 0); } }

        public Factory(int id,string surname,string name,string patronymic, int workshop_number, string position,int work_exp,float salary)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.workshop_number = workshop_number;
            this.position = position;
            this.work_exp = work_exp;
            this.salary = salary;
        }
        public override string ToString()
        {
            return $" Surname: {surname} Name: {name}  Patronymic: {patronymic} Workshop_number: {workshop_number}  Position: {position}  Work_exp: {work_exp} Salary: {salary}";  
        }
        
    }
    public interface IListFactories
    {
        void Add(Factory factory);
        void Delete(int id);
        void EditExperience(int id, int work_exp);
        void EditSalary(int id, float salary);
        void Show();
    }
    public class ListFactories : IListFactories
    {
        protected List<Factory> factories;
        public List<Factory> Factories { get { return factories; } set { factories = value; } }

        public ListFactories(List<Factory> factories)
        {
            this.factories = factories;
        }

        public void Add(Factory factory)
        {
            factories.Add(factory);
        }
        public void Delete(int id)
        {
            try
            {
                factories = factories.Where(item => item.Id != id).ToList();
            }
            catch (Exception exexception)
            {
                Console.WriteLine(exexception.Message);
                Console.WriteLine("Smth wrong, check the id.");
            }
        }
        public void EditExperience(int id, int work_exp)
        {
            try
            {
                factories.First(item => item.Id == id).Work_exp = work_exp;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Smth wrong, check the id.");
            }
        }
        public void EditSalary(int id, float salary)
        {
            try
            {
                factories.First(item => item.Id == id).Salary = salary;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Smth wrong, check the id.");
            }
        }
        public void Show()
        {
            foreach (Factory factory in factories)
            {
                Console.WriteLine(factory);
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Розробити консольний додаток, який би давав можливість працювати зі списком.
            //Додаток повинен надавати користувачеві можливість додавання, перегляду,
            //редагування та видалення даних списку. 
            //“Завод” (прізвище, ім'я, по-батькові; номер цеху; посада; стаж роботи; заробітна плата).
            //Вивести номер цеху, посаду, стаж роботи та заробітну плату за заданим прізвищем.
            //Для кожного цеху вивести інформацію про кількість робітників, які працюють у відповідному цеху.

            ListFactories workers = new ListFactories(
                new List<Factory>
                {
                    new Factory(1, "Мовчан","Георгій","Андрійович",6,"Начальник цеху",4,30000),
                    new Factory(2,"Мельник","Ірина","Петрівна",10,"Головний фахівець",8,25000),
                    new Factory(3,"Ковтун", "Сергій","Олександрович",10,"Головний інспектор",5,22000),
                    new Factory(4,"Нечитайло","Микита", "Іванович",20,"Інженер-радіолог",10,20000),
                    new Factory(5,"Бабенко","Станіслав","Віталійович",6,"Інженер-програміст",4,25000 ),
                });
            workers.Show();
            workers.Add(new Factory(6, "Шевченко","Ігор","Алексійович" ,20,"Аналітик", 6, 1600));
            workers.Show();
            workers.Delete(4);
            workers.Delete(42);
            workers.Show();
            workers.EditExperience(3, 13);
            workers.Show();
            workers.EditSalary(3, 13000);
            workers.Show();
            workers.EditExperience(42, 42);
            workers.EditSalary(42, 42);
            Console.Write("Прізвище працівника: ");
                var name = Convert.ToString(Console.ReadLine());
                var task1 = workers.Factories.Where(item => item.Surname == name);
            Console.WriteLine(name);
            foreach (var item in task1)
            {
                Console.WriteLine($"Workshop_number: {item.Workshop_number}, position: {item.Position}, " +
                    $"Work_exp: {item.Work_exp}, salary: {item.Salary}");
            }
            var task2 = workers.Factories.GroupBy(group => group.Workshop_number).Select(item => new { item.Key, Value = item.Count() });

            foreach (var item in task2)
            {
                Console.WriteLine($"Department: {item.Key}, number of employees: {item.Value}");
            }
            Console.ReadLine();
            Console.WriteLine();
        }
    }
}
