﻿using HashTable;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace Tests
{
    [TestClass]
    public sealed class Test
    {
        private NewHashTable<int, int> hashTable = new NewHashTable<int, int>(2);
        [TestMethod]

        public void TestAdd()
        {
            hashTable.Add(5, 6);
            Assert.AreEqual(true, hashTable.ContainsKey(5));
            Assert.AreEqual(true, hashTable.ContainsValue(6));
            Assert.AreEqual(true, hashTable.Contains(new KeyValuePair<int, int>(5,6)));
        }
        [TestMethod]
        public void TestFalseContains()
        {
            Assert.AreEqual(false, hashTable.ContainsKey(6));
            Assert.AreEqual(false, hashTable.ContainsValue(5));
            Assert.AreEqual(false, hashTable.Contains(new KeyValuePair<int, int>(6, 5)));
        }

        [TestMethod]
        public void TestRemove()
        {
            hashTable.Add(5, 6);
            hashTable.Remove(5);
            Assert.AreEqual(false, hashTable.ContainsKey(5));
            Assert.AreEqual(false, hashTable.ContainsValue(6));
            Assert.AreEqual(false, hashTable.Contains(new KeyValuePair<int, int>(5, 6)));
        }

        [TestMethod]
        public void TestExceptionAdd()
        {
            Assert.ThrowsException<Exception>(TestDoubleAdd);
        }

        void TestDoubleAdd()
        {
            hashTable.Add(5, 8);
            hashTable.Add(5, 7);
        }

        [TestMethod]
        public void IndexGet()
        {
            hashTable.Add(5, 7);
            Assert.AreEqual(7, hashTable[5]);
        }
        [TestMethod]
        public void IndexSet()
        {
            hashTable.Add(5, 7);
            Assert.AreEqual(8, hashTable[5] = 8);
        }

        [TestMethod]
        public void TestExceptionGet()
        {
            Assert.ThrowsException<Exception>(TestGet);
        }
        void TestGet()
        {
            hashTable.Add(5, 7);
            Console.WriteLine(hashTable[6]);
        }

        [TestMethod]
        public void TestExceptionSet()
        {
            Assert.ThrowsException<Exception>(TestSet);
        }
        void TestSet()
        {
            hashTable.Add(5, 7);
            hashTable[6] = 8;
        }

        [TestMethod]
        public void TestAnotherAdd()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            hashTable.Add(new KeyValuePair<int, int>(3, 6));
            Assert.AreEqual(true, hashTable.ContainsKey(5));
            Assert.AreEqual(true, hashTable.ContainsValue(6));
            Assert.AreEqual(true, hashTable.Contains(new KeyValuePair<int, int>(5, 6)));
        }

        [TestMethod]
        public void TestAnotherRemove()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            hashTable.Remove(new KeyValuePair<int, int>(5, 6));
            Assert.AreEqual(false, hashTable.ContainsKey(5));
        }

        [TestMethod]
        public void TestClear()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            hashTable.Clear();
            Assert.AreEqual(false, hashTable.ContainsKey(5));
        }

        [TestMethod]
        public void TestCapacity()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            Assert.AreEqual(2, hashTable.Capacity);
        }

        [TestMethod]
        public void TestCount()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            Assert.AreEqual(1, hashTable.Count);
        }

        [TestMethod]
        public void TestClone()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            NewHashTable<int, int> anotherHashTable = (NewHashTable<int, int>)hashTable.Clone();
            Assert.AreEqual(hashTable, anotherHashTable);
        }

        [TestMethod]
        public void TestConstruct()
        {
            NewHashTable<int, int> anotherHashTable = new NewHashTable<int, int>();
            Assert.AreEqual(15, anotherHashTable.Capacity);
        }

        [TestMethod]
        public void TestSecondConstruct()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            NewHashTable<int, int> anotherHashTable = new NewHashTable<int, int>(hashTable);
            Assert.AreEqual(hashTable, anotherHashTable);
        }

        [TestMethod]
        public void TestCopyTo()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[hashTable.Count];
            hashTable.CopyTo(array, 0);
            Assert.AreEqual(hashTable.Count, array.Length);
            Assert.AreEqual(5, array[0].Key);
            Assert.AreEqual(6, array[0].Value);
        }

        [TestMethod]
        public void TestExceptionCopy()
        {
            Assert.ThrowsException<Exception>(TestCopy);
        }
        void TestCopy()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[hashTable.Count];
            hashTable.CopyTo(array, -1);
        }

        [TestMethod]
        public void TestShallowCopy()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            NewHashTable<int, int> anotherHashTable = (NewHashTable<int, int>)hashTable.ShallowCopy();
            Assert.AreEqual(hashTable, anotherHashTable);
        }

        [TestMethod]
        public void TestRange()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            NewHashTable<int, int> anotherHashTable = new NewHashTable<int, int>(1);
            anotherHashTable.Add(7, 7);
            Assert.AreEqual(2, anotherHashTable.Capacity);

        }

        [TestMethod]
        public void TestKey()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            var list = hashTable.Keys;
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestValue()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            var list = hashTable.Values;
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestTryGetValue()
        {
            hashTable.Add(new KeyValuePair<int, int>(5, 6));
            var item = hashTable.TryGetValue(5, out var value);
            Assert.AreEqual(true,item);
        }

    }
}
