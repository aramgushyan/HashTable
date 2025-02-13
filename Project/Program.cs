using HashTable;

NewHashTable <int,int> newHashTable = new NewHashTable<int, int>();
newHashTable.Add(5, 6);
newHashTable.Add(6, 7);
newHashTable.Add(75, 8);
newHashTable.Add(76, 8);
NewHashTable<int,int> another = (NewHashTable<int, int>)newHashTable.Clone();
//another.Remove(5);
//another.Add(80, 4);
//another.Add(9080, 4);
//foreach (var item in newHashTable)
//{
//    Console.WriteLine(item.Key);
//}
foreach (var item in another)
{
    Console.WriteLine(item.Key);
}

