
using System.Collections;
namespace HashTable
{
    public class NewHashTable<TKey, TValue> : IEnumerable<KeysAndValues<TKey, TValue>>, ICollection<KeysAndValues<TKey, TValue>>, ICloneable
    {

        private LinkedList<KeysAndValues<TKey, TValue>>[] _storage;
        private int _capacity;


        public LinkedList<KeysAndValues<TKey, TValue>>[] Storage
        {
            get
            {
                return _storage;
            }
        }

        public int Capacity 
        {
            get
            {
                return _capacity;
            }
            set
            {
                if (value < 0) 
                    throw new ArgumentOutOfRangeException("Не может быть меньше 0");
                _capacity = value;
            }
        }

        public int Count { get; set; } = 0;

        public bool IsReadOnly => false;


        public NewHashTable()
        {
            Capacity = 15;
            _storage = new LinkedList<KeysAndValues<TKey, TValue>>[Capacity];
        }

        public NewHashTable(int capacity)
        {
            Capacity = capacity;
            _storage = new LinkedList<KeysAndValues<TKey, TValue>>[Capacity];
            
        }

        public NewHashTable(NewHashTable<TKey, TValue> hashTable)
        {
            _storage = ((NewHashTable<TKey, TValue>)hashTable.Clone()).Storage;
            Capacity = hashTable.Capacity;
            Count = hashTable.Count;
        }

        public void Add(TKey key, TValue value)
        {
            if (IsReadOnly == false)
            {
                if (ContainsKey(key))
                {
                    throw new Exception("Такой ключ уже есть");
                }
                else
                {
                    CreateList(key, out int hashcodeIndex);
                    _storage[hashcodeIndex].AddLast(new KeysAndValues<TKey, TValue>()
                    {
                        Key = key,
                        Value = value
                    });
                    ++Count;
                    if (_storage[hashcodeIndex].Count >= _capacity)
                    {
                        UpdateRange();
                    }
                }
            }
            else 
            {
                throw new Exception("Коллекция только для чтения");
            }
        }

        public bool Remove(TKey key)
        {
            if (IsReadOnly == false)
            {
                int index = IndexStorage(key);
                if (ContainsKey(key))
                {
                    foreach (var item in _storage[index])
                    {
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
            throw new Exception("Коллекция только для чтения");
        }

        private int IndexStorage(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % _capacity;
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
            foreach (var item in this) 
            {
                if(item.Value.Equals(value))
                    return true;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    int index = IndexStorage(key);
                    return FindValueWithKey(key, _storage[index]);
                }
                throw new Exception("Такого ключа нет");
            }
            set
            {
                if (IsReadOnly == false)
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
                else
                {
                    throw new Exception("Коллекция только для чтения");
                }
            }
        }

        private void ChangeValue(TKey key, TValue value, LinkedList<KeysAndValues<TKey, TValue>> keysAndValues)
        {
                foreach (var item in keysAndValues)
                {
                    if (item.Key.Equals(key))
                    {
                        item.Value = value;
                    }
                }
        }

        private TValue FindValueWithKey(TKey key, LinkedList<KeysAndValues<TKey, TValue>> keysAndValues)
        {
            foreach (var item in keysAndValues)
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }
            throw new Exception("Такого ключа нет");
        }

        private void CreateList(TKey key, out int hashcodeIndex)
        {
            hashcodeIndex = IndexStorage(key);

            if (_storage[hashcodeIndex] == null)
            {
                _storage[hashcodeIndex] = new LinkedList<KeysAndValues<TKey, TValue>>();
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
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _storage = new LinkedList<KeysAndValues<TKey, TValue>>[_capacity];
        }

        public bool Contains(KeysAndValues<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeysAndValues<TKey, TValue>[] array, int arrayIndex)//?????????????
        {
            if (arrayIndex < 0 || arrayIndex + Count > array.Length)
                throw new Exception("Недостаточно места в массиве");

            foreach (var item in this) 
            {
                array[arrayIndex] = item;
                ++arrayIndex;
            }
        }

        public bool Remove(KeysAndValues<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public object Clone()
        {
            NewHashTable<TKey, TValue> clone = new NewHashTable<TKey, TValue>(_capacity);
            foreach (var item in this)
            {
                clone.Add(item.Key, item.Value);
            }
            clone.Capacity = _capacity;
            clone.Count = Count;
            return clone;
        }

        public object ShallowCopy()
        {
            {
                return this.MemberwiseClone();
            }
        }

        private void UpdateRange() 
        {
            _capacity *= 2;
            var newStorage = new LinkedList<KeysAndValues<TKey, TValue>>[_capacity];
            foreach (var item in this) 
            {
                int index = IndexStorage(item.Key);
                if (newStorage[index] == null) 
                {
                    newStorage[index] = new LinkedList<KeysAndValues<TKey, TValue>>();
                }
                newStorage[index].AddLast(item);
            }
            _storage = newStorage;
        }

        public override bool Equals(object? obj)
        {
            if (obj is NewHashTable<TKey, TValue> anotherTable)
            {
                if(anotherTable.Count!=Count)
                    return false;

                List<KeysAndValues<TKey, TValue>> listOfThisItems = new List<KeysAndValues<TKey, TValue>>();
                List<KeysAndValues<TKey, TValue>> listOfAnotherItems = new List<KeysAndValues<TKey, TValue>>();

                foreach (var item in this)
                {
                    listOfThisItems.Add(item);
                }

                foreach (var item in anotherTable)
                {
                    listOfAnotherItems.Add(item);
                }

                for (int i = 0; i < listOfAnotherItems.Count; ++i) 
                {
                    if (listOfThisItems[i].Equals(listOfAnotherItems[i]) == false)
                        return false;
                }
                return true;
            }
            return false;
        }

    }
}
