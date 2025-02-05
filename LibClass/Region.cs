using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibClass
{
    public class Region : Place
    {
        private int cities;
        public int Cities
        {
            get
            {
                return cities;
            }
            set
            {
                if (value >= 0)
                    cities = value;
                else
                {
                    throw new ArgumentException("Количество городов не может быть меньше 0 ");
                }
            }
        }
        public Region()
        {
        }
        public Region(int population, string name, int cities) : base(population, name)
        {
            Cities = cities;
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Количество городов:{Cities}");
        }

        public new void ShowNoVirtual()
        {
            Console.WriteLine($"Название местности : {Name}" + "\n" + $"Население местности: {Population}");
            Console.WriteLine($"Количество городов:{Cities}");
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите количество городов");
            Cities = int.Parse(Console.ReadLine());
        }
        public override void RandomInit()
        {
            base.RandomInit();
            Random random = new Random();
            Cities = random.Next(1, 1200);
        }
        public Place BaseRegion 
        {
            get 
            {
                return new Place(Population,Name);
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj is Region region)
                return region.Cities == Cities && base.Equals(obj);
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()+ cities.GetHashCode();
        }

        public override string ToString()
        {
            return Name + " "  + Population + " " + Cities;
        }

    }
}
