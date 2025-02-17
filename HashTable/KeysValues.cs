using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class KeysAndValues<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is KeysAndValues<TKey, TValue> keyAndValue)
            {
                return keyAndValue.Value.Equals(Value) && keyAndValue.Key.Equals(Key);
            }
            throw new Exception("Другой тип");
        }
    }
}
