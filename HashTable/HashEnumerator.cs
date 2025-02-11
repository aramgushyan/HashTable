using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class HashEnumerator : IEnumerator<KeysAndValues>
    {
        private int position = 0;
        //private int positionList = 0;
        LinkedListNode<KeysAndValues> node;
        private LinkedList<KeysAndValues>[] _storage;
        private int _index=0;
        public HashEnumerator(LinkedList<KeysAndValues>[] storage) 
        {
            _storage = storage;
        }

        public KeysAndValues Current => node.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if  (_storage[position]!=null && _index == _storage[position].Count )
            {
                ++position;
                _index = 0;
                node = null;
            }

            while (position <= _storage.Length - 1 && _storage[position] == null)
            {
                ++position;

            }

            if (position > _storage.Length - 1)
            {
                return false;
            }

            if (_index < _storage[position].Count)
            {
                    
                if (_index == 0)
                    node = _storage[position].First;
                else
                    node = node.Next;
                ++_index;
                return true;
            }

            return true;
        }
        public void Reset()
        {
            position = -1;
            node = null;
        }
    }
}