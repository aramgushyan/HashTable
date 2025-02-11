
using System;
using System.Collections;
namespace HashTable
{
    public class NewHashTable <TKey,TValue>:IEnumerable<KeysAndValues>, ICollection<KeysAndValues>
    {
        
       private LinkedList<KeysAndValues>[] _storage = new LinkedList<KeysAndValues>[15];

        public int Count { get; set; } = 0;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new Exception("Такой ключ уже есть");
            }
            else 
            {
                CreateList(key,out int hashcodeIndex);
                _storage[hashcodeIndex].AddLast(new KeysAndValues() 
                {
                    Key = key,
                    Value = value
                });
            }            
        }

        public bool Remove(TKey key)
        {
            int index = IndexStorage(key);
            if (ContainsKey(key))
            {
                foreach (var item in _storage[index]) { 
                    if (item.Key.Equals(key))
                    {
                        _storage[index].Remove(_storage[index].Find(item));
                        return true;
                    }
                }
            }
                throw new Exception("Такого ключа нет");
        }

        public int IndexStorage(TKey key) 
        {
            return key.GetHashCode()%_storage.Length;
        }

        public bool ContainsKey(TKey key) 
        {
            int index = IndexStorage(key);

            if (_storage[index] != null)
            {
                foreach (var item in _storage[index])
                {
                    if (item.Key.Equals(key))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ContainsValue(object value)
        {
            for (int i = 0; i < _storage.Length; i++)
            {
                if (_storage[i]!=null)
                {
                    foreach (var item in _storage[i])
                    {
                        if (item.Value.Equals(value))
                        {
                            return true;
                        }
                    } 
                }
            }
            return false;
        }

        public object this[TKey key] 
        {
            get 
            {
                if (ContainsKey(key)) 
                {
                    int index = IndexStorage(key);
                    return FindValueWithKey(key,_storage[index]);
                }
                throw new Exception("Такого ключа нет");
            }
            set 
            {
                if (ContainsKey(key))
                {
                    int index = IndexStorage(key);
                    ChangeValue(key, (TValue)value, _storage[index]);
                }
                else
                {
                    throw new Exception("Такого ключа нет");
                }
            }
        }

        private void ChangeValue(TKey key, TValue value,LinkedList<KeysAndValues> keysAndValues)
        {
            foreach (var item in keysAndValues)
            {
                if (item.Key.Equals(key))
                {
                    item.Value = value;
                }
            }
        }

        private object FindValueWithKey(TKey key,LinkedList<KeysAndValues> keysAndValues) 
        {
            foreach (var item in keysAndValues)
            {
                if (item.Key.Equals(key)) 
                {
                    return item.Value;
                }
            }
            return false;
        }

        private void CreateList(TKey key,out int hashcodeIndex) 
        {
            hashcodeIndex = IndexStorage(key);
            if (_storage[hashcodeIndex] == null) 
            {
                _storage[hashcodeIndex] = new LinkedList<KeysAndValues>();
                Count++;
            }
        }

        public IEnumerator<KeysAndValues> GetEnumerator()
        {
            return new HashEnumerator(_storage);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeysAndValues item)
        {
            Add((TKey)item.Key, (TValue)item.Value);
        }

        public void Clear()
        {
            _storage = new LinkedList<KeysAndValues>[15];
        }

        public bool Contains(KeysAndValues item)
        {
            return ContainsKey((TKey)item.Key);
        }

        public void CopyTo(KeysAndValues[] array, int arrayIndex)//?????????????
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeysAndValues item)
        {
            return Remove((TKey)item.Key);
        }

    }
}
