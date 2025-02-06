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
        private int position = -1;
        private int positionList = 0;

        int _pos = -1;
        int _posList = 0;
        int _count;

        private List<KeysAndValues>[] _storage;
        public HashEnumerator(List<KeysAndValues>[] storage, int count) 
        {
            _count = count;
            _storage = storage;
        }

        public KeysAndValues Current => _storage[position][positionList];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (position != -1)
            {
                if (positionList < _storage[position].Count - 1)
                {
                    ++positionList;
                    return true;
                }
                else
                {
                    do
                    {
                        if (position < _storage.Length - 1)
                        {
                            ++position;
                            positionList = 0;
                        }
                        else
                            return false;
                    } while (_storage[position] == null);
                    return true;
                }
            }
            else
            {
                ++position;
                if (_storage[position] == null)
                {
                    do
                    {
                        if (position < _storage.Length - 1)
                        {
                            ++position;
                            positionList = 0;
                        }
                        else
                            return false;
                    } while (_storage[position] == null);
                }
                    return true;
            }
        }

        //public bool MoveNext()
        //{
        //    _pos++;
        //    if (_posList >= _storage.Length)
        //        return false;
        //    while (_storage[_posList] == null)
        //    {
        //        _posList++;
        //        if (_posList >= _storage.Length)
        //            return false;
        //    }
        //    if (_pos < _storage[_posList].Count)
        //        return true;
        //    else
        //    {
        //        _posList++;
        //        _pos = 0;
        //        while (_storage[_posList] == null)
        //        {
        //            _posList++;
        //            if (_posList >= _storage.Length)
        //                return false;
        //        }
        //    }
        //    return true;
        //}

        public void Reset()
        {
            position = -1;
            positionList = -1;
        }
    }
}