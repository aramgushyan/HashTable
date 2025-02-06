using HashTable;

NewHashTable newHashTable = new NewHashTable();
newHashTable.Add(5, 6);
newHashTable.Add(6, 7);
newHashTable.Add(75, 8);
newHashTable.Add(76, 8);
foreach (var o in newHashTable) 
{
    Console.WriteLine(o.Value);
}