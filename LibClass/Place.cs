using System;

namespace LibClass
{
  
    public class Place : IComparable <Place>,ICloneable, IInit
    {
        static string[] arr = { "Лес", "Горы", "Пляж", "Озеро", "Долина", "Равнина", "Пустыня", "Вулкан" };
        static Random rnd = new Random();
        
        private int population;
        private string name;
        public ExampleClone example_Clone;
        
        public int Population
        {
            get
            {
                return population;
            }
            set
            {
                if (value >= 0)
                    population = value;
                else 
                {
                    throw new ArgumentException("Население места не может быть меньше 0 ");
                }
            }
        }
        public string Name 
        { 
            get
            {
                return name;
            }
            set 
            { 
                name = value;
            }
        }
        public Place(int population, string name)
        {
            Name = name;
            Population = population;
            this.example_Clone =new ExampleClone("clone");
        }

        public Place() 
        {
        }
        //public Place(int population, string name) 
        //{
        //    Name = name;
        //    Population = population;
        //    example_Clone = new ExampleClone("");
        //}
        public virtual void Show()
        {
            Console.WriteLine($"Название местности : {Name}" + "\n" + $"Население местности: {Population}");      
        }

        public new void ShowNoVirtual()
        {
            Console.WriteLine($"Название местности : {Name}" + "\n" + $"Население местности: {Population}");
        }

        public virtual void Init() 
        {
            Console.WriteLine("Введите название местности");
            Name = Console.ReadLine();
            Console.WriteLine("Введите численность населения местности");
            Population = int.Parse(Console.ReadLine());
        }

        public virtual void RandomInit()
        {
            Name = arr[rnd.Next(1,8)];
            Population = rnd.Next(1, 1200);
        }
        public  override bool Equals(object? obj) 
        {
            if(obj is Place place)
                return place.Population== Population && place.Name==Name;
            return false;
        }
        public override int GetHashCode()
        {
            return name.GetHashCode() + population.GetHashCode();
        }
        public int CompareTo(Place? place) 
        {
            if (place is null) throw new ArgumentException("Некорректное значение");
            return Population.CompareTo(place.Population);
        }
        public object Clone() 
        {
            return new Place(Population, "Клон" + " " + Name);
        }

        public Place ShallowCopy() 
        {
            return (Place)this.MemberwiseClone();
        }
        public override string ToString()
        {
            return Name+" " + Population;
        }
    }
}
