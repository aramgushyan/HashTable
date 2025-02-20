
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
namespace HashTable
{
    public class NewHashTable<TKey, TValue>:IDictionary<TKey, TValue>, ICloneable
    {

        private KeysAndValues<TKey, TValue>[] _storage;
        private int _capacity;


        public KeysAndValues<TKey, TValue>[] Storage
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
        public int Count 
        {
            get 
            {
                int _count = 0;
                foreach (var item in this)
                {
                    ++_count;
                }
                return _count;
            }
        }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                foreach (var item in this) 
                {
                    keys.Add(item.Key);
                }
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                foreach (var item in this)
                {
                    values.Add(item.Value);
                }
                return values;
            }
        }


        public NewHashTable()
        {
            Capacity = 15;
            _storage = new KeysAndValues<TKey, TValue>[Capacity];
        }

        public NewHashTable(int capacity)
        {
            Capacity = capacity;
            _storage = new KeysAndValues<TKey, TValue>[Capacity];

        }

        public NewHashTable(NewHashTable<TKey, TValue> hashTable)
        {
            _storage = ((NewHashTable<TKey, TValue>)hashTable.Clone()).Storage;
            Capacity = hashTable.Capacity;
        }

        public void Add(TKey key, TValue value)
        {
            if (IsReadOnly == false)
            {

                if (!ContainsKey(key))
                {
                    int hashcodeIndex = IndexStorage(key);

                    if (_storage[hashcodeIndex] == null)
                    {
                        _storage[hashcodeIndex] = new KeysAndValues<TKey, TValue>(key, value);
                    }
                    else
                    {
                        var current = _storage[hashcodeIndex];

                        while (current.Next != null)
                        {
                            current = current.Next;
                        }

                        current.Next = new KeysAndValues<TKey, TValue>(key, value);
                    }
                    if (LenghtNodes(hashcodeIndex) >= _capacity)
                    {
                        UpdateRange();
                    }
                }
                else
                {
                    throw new Exception("Такой ключ есть");
                }
            }
            else
            {
                throw new Exception("Коллекция только для чтения");
            }
        }
        private int LenghtNodes(int index) 
        {
            var current = _storage[index];
            int count = 0;
            while (current != null) 
            {
                ++count;
                current = current.Next;
            }  
            return count;
        }
        public bool Remove(TKey key)
        {
            if (IsReadOnly == false)
            {
                int index = IndexStorage(key);
                var current = _storage[index];
                KeysAndValues<TKey, TValue> past = null;

                while (current != null) 
                {
                    if (current.Key.Equals(key)) 
                    {
                        if (past == null)
                            _storage[index] = current.Next;
                        else
                            past.Next = current.Next;
                        return true;
                    }
                    past=current;
                    current=current.Next;
                }
                return false;
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
                var current = _storage[index];
                while (current !=null) 
                {
                    if (current.Key.Equals(key)) 
                    {
                        return true;
                    }
                    current = current.Next; 
                }
            }
            return false;
        }

        public bool ContainsValue(object value)
        {
            foreach (var _item in this)
            {
                if (_item.Value.Equals(value))
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
                    return FindValueWithKey(key);
                }
                throw new Exception("Такого ключа нет");
            }
            set
            {
                if (IsReadOnly == false)
                {
                    if (ContainsKey(key))
                    {
                        ChangeValue(key,value);
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

        private void ChangeValue(TKey key, TValue value)
        {
            var current = _storage[IndexStorage(key)];
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    current.Value = value;
                }
                current = current.Next;
            }
        }

        private TValue FindValueWithKey(TKey key)
        {

            var current = _storage[IndexStorage(key)];
            while (current != null) 
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            throw new Exception("Такого ключа нет");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _storage = new KeysAndValues<TKey, TValue>[_capacity];
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

       public  IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new HashEnumerator<TKey, TValue>(_storage);
        }

        public void CopyTo(KeysAndValues<TKey, TValue>[] array, int arrayIndex)//?????????????
        {
            if (arrayIndex < 0 || arrayIndex + Count > array.Length)
                throw new Exception("Недостаточно места в массиве");

            foreach (var _item in this)
            {
                array[arrayIndex] = new KeysAndValues<TKey, TValue>(_item.Key, _item.Value);
                ++arrayIndex;
            }
        }

        public object Clone()
        {
            NewHashTable<TKey, TValue> clone = new NewHashTable<TKey, TValue>(_capacity);
            foreach (var _item in this)
            {
                clone.Add(_item.Key, _item.Value);
            }
            clone.Capacity = _capacity;
            return clone;
        }

        public object ShallowCopy()
        {
             return this.MemberwiseClone();
        }

        private void UpdateRange()
        {
            _capacity *= 2;
            var newStorage = new KeysAndValues<TKey, TValue>[_capacity];
            foreach (var _item in this)
            {
                int index = IndexStorage(_item.Key);
                if (newStorage[index] == null)
                {
                    newStorage[index] = new KeysAndValues<TKey, TValue>(_item.Key, _item.Value);
                }
                else
                {
                    var current = newStorage[index];
                    while (current != null) 
                    {
                        current=current.Next;
                    }
                    current.Next = new KeysAndValues<TKey, TValue>(_item.Key, _item.Value);
                }   
            }
            _storage = newStorage;
        }

        public override bool Equals(object? obj)
        {
            if (obj is NewHashTable<TKey, TValue> anotherTable)
            {
                if (anotherTable.Count != Count)
                    return false;

                List<KeyValuePair<TKey, TValue>> listOfThisItems = new List<KeyValuePair<TKey, TValue>>();
                List<KeyValuePair<TKey, TValue>> listOfAnotherItems = new List<KeyValuePair<TKey, TValue>>();

                foreach (var _item in this)
                {
                    listOfThisItems.Add(_item);
                }

                foreach (var _item in anotherTable)
                {
                    listOfAnotherItems.Add(_item);
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
