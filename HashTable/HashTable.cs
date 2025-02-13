
using System;
using System.Collections;
namespace HashTable
{
    public class NewHashTable <TKey,TValue>:IEnumerable<KeysAndValues <TKey,TValue>>, ICollection<KeysAndValues <TKey,TValue>>,ICloneable
    {

        private LinkedList<KeysAndValues<TKey, TValue>>[] _storage;


        private int _capacity;
        public int Count { get; set; } = 0;

        public bool IsReadOnly => false;


        public NewHashTable()
        {
            _capacity = 15;
            _storage = new LinkedList<KeysAndValues<TKey, TValue>>[_capacity];
        }

        public NewHashTable(int capacity)
        {
            _storage = new LinkedList<KeysAndValues<TKey, TValue>>[capacity];
            _capacity = capacity;
        }

        public NewHashTable(NewHashTable<TKey,TValue> hashTable) 
        {

        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new Exception("Такой ключ уже есть");
            }
            else 
            {
                CreateList(key,out int hashcodeIndex);
                _storage[hashcodeIndex].AddLast(new KeysAndValues<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                });
                ++Count;
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
                        --Count;
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

        private void ChangeValue(TKey key, TValue value,LinkedList<KeysAndValues<TKey, TValue>> keysAndValues)
        {
            foreach (var item in keysAndValues)
            {
                if (item.Key.Equals(key))
                {
                    item.Value = value;
                }
            }
        }

        private object FindValueWithKey(TKey key,LinkedList<KeysAndValues<TKey, TValue>> keysAndValues) 
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
                _storage[hashcodeIndex] = new LinkedList<KeysAndValues<TKey, TValue>>();
                Count++;
            }
        }

        public IEnumerator<KeysAndValues<TKey, TValue>> GetEnumerator()
        {
            return new HashEnumerator<TKey, TValue>(_storage);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeysAndValues<TKey, TValue> item)
        {
            Add(item.Key,item.Value);
        }

        public void Clear()
        {
            _storage = new LinkedList<KeysAndValues<TKey, TValue>>[15];
        }

        public bool Contains(KeysAndValues<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeysAndValues<TKey, TValue>[] array, int arrayIndex)//?????????????
        {
            array = new KeysAndValues<TKey, TValue>[_capacity];
            foreach (var item in this) 
            {
                array[arrayIndex++] = item;
            }
        }

        public bool Remove(KeysAndValues<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public object Clone()
        {
            NewHashTable<TKey,TValue> clone = new NewHashTable<TKey,TValue>(_capacity);
            foreach (var item in this) 
            {
                clone.Add(item);
            }
            return clone;
        }

        public object ShallowCopy() 
        {
            return this;
        }

    }
}
