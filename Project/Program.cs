using HashTable;

NewHashTable <int,int> newHashTable = new NewHashTable<int, int>();
newHashTable.Add(5, 6);
newHashTable.Add(6, 7);
newHashTable.Add(75, 8);
newHashTable.Add(76, 8);
foreach (var item in newHashTable) 
{
    Console.WriteLine(item.Key);
}