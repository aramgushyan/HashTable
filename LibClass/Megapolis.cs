using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClass
{
    public class Megapolis : City
    {
        private int streets;
        public int Streets
        {
            get
            {
                return streets;
            }
            set
            {
                if (value >= 0)
                    streets = value;
                else
                {
                    throw new ArgumentException("Количество улиц не может быть меньше 0 ");
                }
            }
        }
        public Megapolis()
        {

        }
        public Megapolis(int population, string name, int cities, int places, int streets) : base(population, name, cities, places)
        {
            Streets = streets;
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Количество улиц:{Streets}");
        }

        public new void ShowNoVirtual()
        {
            Console.WriteLine($"Название местности : {Name}" + "\n" + $"Население местности: {Population}");
            Console.WriteLine($"Количество городов:{Cities}");
            Console.WriteLine($"Количество районов:{Places}");
            Console.WriteLine($"Количество улиц:{Streets}");
        }


        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите количество улиц");
            Streets = int.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Random random = new Random();
            Streets = random.Next(1, 50);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Megapolis megapolis)
                return base.Equals(obj) && megapolis.Streets == Streets;
            return false;
        }
    }
}
