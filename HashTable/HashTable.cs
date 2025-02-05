
using System.Collections;
namespace HashTable
{
    public class NewHashTable   
    {
        
        List<KeysAndValues>[] storage = new List<KeysAndValues>[15]; 

        public void Add(object key, object value)
        {
            if (ContainsKey(key))
            {
                throw new Exception("Такой ключ уже есть");
            }
            else 
            {
                CreateList(key,out int hashcodeIndex);
                storage[hashcodeIndex].Add(new KeysAndValues() 
                {
                    Key = key,
                    Value = value
                });
            }            
        }

        public void Remove(object key)
        {
            int index = IndexStorage(key);
            if (ContainsKey(key))
            {
                for (int i = 0; i < storage[index].Count; i++)
                {
                    if (storage[index][i].Key.Equals(key))
                        storage[index].RemoveAt(i);
                }
            }
            else 
            {
                throw new Exception("Такого ключа нет");
            }
        }

        public int IndexStorage(object key) 
        {
            return key.GetHashCode()%storage.Length;
        }

        public bool ContainsKey(object key) 
        {
            int index = IndexStorage(key);
            if (storage[index] != null)
            {
                for (int j = 0; j < storage[index].Count; j++)
                {
                    if (storage[index][j].Key.Equals(key))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ContainsValue(object value)
        {
            for (int i = 0; i < storage.Length; i++)
            {
                if (storage[i]!=null){
                    for (int j = 0; j < storage[i].Count; j++)
                    {
                        if (storage[i][j].Value.Equals(value))
                        {
                            return true;
                        }
                    } }
            }
            return false;
        }

        public object this[object key] 
        {
            get 
            {
                if (ContainsKey(key)) 
                {
                    int index = IndexStorage(key);
                    return FindValueWithKey(key,storage[index]);
                }
                throw new Exception("Такого ключа нет");
            }
            set 
            {
                if (ContainsKey(key))
                {
                    int index = IndexStorage(key);
                    ChangeValue(key, value, storage[index]);
                }
                else
                {
                    throw new Exception("Такого ключа нет");
                }
            }
        }

        private void ChangeValue(object key, object value,List<KeysAndValues> keysAndValues)
        {
            for (int i = 0; i < keysAndValues.Count; i++)
            {
                if (keysAndValues[i].Key.Equals(key))
                {
                    keysAndValues[i].Value = value;
                }
            }
        }

        public object FindValueWithKey(object key,List<KeysAndValues> keysAndValues) 
        {
            for (int i = 0; i < keysAndValues.Count; i++) 
            {
                if (keysAndValues[i].Key.Equals(key)) 
                {
                    return keysAndValues[i].Value;
                }
            }
            return false;
        }
        private void CreateList(object key,out int hashcodeIndex) 
        {
            hashcodeIndex = IndexStorage(key);
            if (storage[hashcodeIndex] == null) 
            {
                storage[hashcodeIndex] = new List<KeysAndValues>();
            }
        }
    }
}
