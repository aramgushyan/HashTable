using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClass
{
    public class WorkWithIInit : IInit
    {
        private string name;
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

        public virtual void Init()
        {
            Console.WriteLine("Введите имя");
            Name = Console.ReadLine();
        }

        public virtual void RandomInit()
        {
            string[] arr = { "Тест", "Практика", "Экзамен" };
            Random rnd = new Random();
            Name = arr[rnd.Next(1, 3)];
        }
        public virtual void Show()
        {
            Console.WriteLine($"Название: {Name}");
        }

    }
}
