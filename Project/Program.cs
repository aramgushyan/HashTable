using System;
using System.Security.Cryptography;
using HashTable;
using LibClass;
namespace HashTableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            NewHashTable<Place, Region> hashTable = new NewHashTable<Place, Region>();
            Place firstPlace = new Place()
            {
                Name = "Forest",
                Population = 5
            };
            Place secondPlace = new Place()
            {
                Name = "Volcano",
                Population = 6
            };
            Region firstRegion = new Region()
            {
                Name = "Forest",
                Population = 5,
                Cities = 6
            };
            Region secondRegion = new Region()
            {
                Name = "Volcano",
                Population = 6,
                Cities = 5
            };
            Place thirdPlace = new Place()
            {
                Name = "Jungle",
                Population = 200
            };

            Region thirdRegion = new Region()
            {
                Name = "Jungle",
                Population = 200,
                Cities = 0
            };

            hashTable.Add(firstPlace, firstRegion);
            hashTable.Add(secondPlace, secondRegion);

            Console.WriteLine("После добавления");
            foreach (var item in hashTable)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("Используем свойства Keys и Values");
            Console.WriteLine("Keys:");
            foreach (var item in hashTable.Keys) 
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine("Values:");
            foreach (var item in hashTable.Values)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine("Удаляем  по ключу firstPlace");
            hashTable.Remove(firstPlace);

            Console.WriteLine("После Удаления  по ключу firstPlace");
            foreach (var item in hashTable)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine($"Содержит ли таблица ключ firstPlace?: {hashTable.ContainsKey(firstPlace)} ");
            Console.WriteLine($"Содержит ли таблица ключ secondPlace?: {hashTable.ContainsKey(secondPlace)} ");

            Console.WriteLine($"Содержит ли таблица firstRegion?: {hashTable.ContainsValue(firstRegion)} ");
            Console.WriteLine($"Содержит ли таблица secondRegion?: {hashTable.ContainsValue(secondRegion)} ");
            Console.WriteLine();

            Console.WriteLine($"Добавим новый элемент через метод интерфейса ICollection");
            hashTable.Add(new KeyValuePair<Place, Region>(thirdPlace, thirdRegion));
            Console.WriteLine("");

            Console.WriteLine("Индексатор:");
            foreach (var item in hashTable)
            {
                Console.WriteLine($"Значение={hashTable[item.Key]}");
            }
            Console.WriteLine();

            Console.WriteLine("Поменяем thirdRegion на secondRegion");
            hashTable[thirdPlace] = secondRegion;
            Console.WriteLine($"{hashTable[thirdPlace]}");
            Console.WriteLine();

            Console.WriteLine($"Удалим новый элемент через метод интерфейса ICollection");
            hashTable.Remove(new KeyValuePair<Place, Region>(thirdPlace, secondRegion));
            Console.WriteLine();

            KeyValuePair<Place, Region> forCheck = new KeyValuePair<Place, Region>(thirdPlace, secondRegion);

            Console.Write("Проверим  есть ли элемент  через метод интерфейса ICollection?: ");
            Console.Write($"{hashTable.Contains(forCheck)}");
            Console.WriteLine();

            Console.WriteLine("Создание Клона");
            NewHashTable<Place, Region> clone = (NewHashTable<Place, Region>)hashTable.Clone();

            Console.WriteLine("Добавим firstplace в клон");
            clone.Add(firstPlace, firstRegion);

            Console.WriteLine("Убедимся в том что клон изменился, а оригинал нет");
            Console.WriteLine();

            Console.WriteLine("Оригинал:");
            foreach (var item in hashTable)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("Клон:");
            foreach (var item in clone)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("Создание Копии");
            NewHashTable<Place, Region> copyClone = (NewHashTable<Place, Region>)hashTable.ShallowCopy();

            Console.WriteLine("Добавим firstplace в копию");
            copyClone.Add(firstPlace, firstRegion);

            Console.WriteLine("Убедимся в том что копия изменилась с оригиналом");
            Console.WriteLine();

            Console.WriteLine("Оригинал:");
            foreach (var item in hashTable)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("Копия:");
            foreach (var item in copyClone)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("Создание клона через конструктор");
            NewHashTable<Place, Region> anotherClone = new NewHashTable<Place, Region>(hashTable);
            Console.WriteLine();

            Console.WriteLine("Новая таблица:");
            foreach (var item in anotherClone)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("Copy To");
            KeyValuePair<Place, Region>[] array = new KeyValuePair<Place, Region>[copyClone.Count];
            copyClone.CopyTo(array, 0);
            Console.WriteLine();

            Console.WriteLine("Массив");
            foreach (var item in array)
            {
                Console.WriteLine($"ключ={item.Key}" + " " + $"значение={item.Value}");
            }


        }
    }
}
