﻿using System.Collections;

namespace HashTable
{
    public class HashEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private Node<KeysAndValues<TKey, TValue>> _item;
        private Node<KeysAndValues<TKey, TValue>>[] _storage;
        private Node<KeysAndValues<TKey, TValue>> _current;
        private int _index = 0;
        public HashEnumerator(Node<KeysAndValues<TKey, TValue>>[] storage)
        {
            _storage = storage;
        }

        object IEnumerator.Current => Current;

        public KeyValuePair<TKey, TValue> Current => new KeyValuePair<TKey, TValue>(_item.Item.Key, _item.Item.Value);

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while (_index != _storage.Length)
            {
                if (_storage[_index] == null && _index != _storage.Length)
                {
                    ++_index;
                    _item = null;
                }
                if (_index != _storage.Length)
                {
                    if (_item == null)
                    {
                        _current = _storage[_index];
                    }
                    while (_current != null)
                    {
                        _item = _current;
                        _current= _current.Next;
                        return true;
                    }
                    ++_index;
                    _item = null;
                }
            }
            return false;
        }
        public void Reset()
        {
            _index = 0;
        }
    }
}