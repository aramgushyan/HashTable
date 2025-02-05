using LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClass
{
    public class City : Region
    {
        private int places;
        public int Places
        {
            get
            {
                return places;
            }
            set
            {
                if (value >= 0)
                    places = value;
                else
                {
                    throw new ArgumentException("Количество мест не может быть меньше 0 ");
                }
            }
        }
        public City()
        {
        }
        public City(int population, string name, int cities, int places) : base(population, name, cities)
        {
            Places = places;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Количество районов:{Places}");
        }

        public new void ShowNoVirtual()
        {
            Console.WriteLine($"Название местности : {Name}" + "\n" + $"Население местности: {Population}");
            Console.WriteLine($"Количество городов:{Cities}");
            Console.WriteLine($"Количество районов:{Places}");
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите количество мест");
            Places = int.Parse(Console.ReadLine());
        }
        public override void RandomInit()
        {
            base.RandomInit();
            Random random = new Random();
            Places = random.Next(1, 50);
        }
        public override bool Equals(object? obj)
        {
            if (obj is City city)
                return base.Equals(obj) && city.Places == Places;
            return false;
        }
    }
}
