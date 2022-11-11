using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labka4
{
    internal class Dai
    {
        public int id { get; set; }
        public string pib { get; set; }
        public string brend { get; set; }
        public string number_car { get; set; }
        public string color { get; set; }

        public Dai(int id, string pib, string brend, string number_car, string color)
        {
            this.id = id;
            this.pib = pib;
            this.brend = brend;
            this.number_car = number_car;
            this.color = color;
        }

        public Dai(string pib, string brend, string number_car, string color)
        {
            this.id = 0;
            this.pib = pib;
            this.brend = brend;
            this.number_car = number_car;
            this.color = color;
        }
        public Dai() { }

        public override string ToString()
        {
            return $"id:{id}, П.І.Б:{pib} ,Марка:{brend} ,Номер машини:{number_car},Колір {color} ";
        }
    }
}
