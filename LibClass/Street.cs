using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClass
{
    public class Street : Megapolis
    {
        private int houses;
        public int Houses
        {
            get
            {
                return houses;
            }
            set
            {
                if (value >= 0)
                    houses = value;
                else
                {
                    throw new ArgumentException("Количество домов не может быть меньше 0 ");
                }
            }
        }
        public Street()
        {
        }
        public Street(int population, string name, int cities, int places, int streets, int houses) : base(population, name, cities, places, streets)
        {
            Houses = houses;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Количество домов {Houses}");
        }

        public new  void ShowNoVirtual()
        {
            Console.WriteLine($"Название местности : {Name}" + "\n" + $"Население местности: {Population}");
            Console.WriteLine($"Количество городов:{Cities}");
            Console.WriteLine($"Количество районов:{Places}");
            Console.WriteLine($"Количество улиц:{Streets}");
            Console.WriteLine($"Количество домов {Houses}");
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine($"Введите количество домов");
            Houses = int.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Random rnd = new Random();
            Houses = rnd.Next(1, 100);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Street street)
                return base.Equals(obj) && street.Houses == Houses;
            return false;
        }
    }
}
