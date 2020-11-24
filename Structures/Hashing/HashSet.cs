﻿using Structures.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Structures.Hashing
{
    internal class HashSet<T> : IStructure<T>
    {
        private static double _expandFactor = 0.75;
        private static int _defaultCapacity = 1024;

        private int _count;
        private LinkedList<T>[] _hashTable;

        public HashSet() : this(_defaultCapacity)
        { }

        public HashSet(int initialCapacity) => _hashTable = new LinkedList<T>[initialCapacity];

        public ICollection<T> Find(T data)
        {
            var result = new LinkedList<T>();
            var list = _hashTable[GetIndex(data.GetHashCode())];

            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Equals(data))
                    {
                        result.AddLast(item);
                        break;
                    }
                }
            }

            return result;
        }

        public void Insert(T data)
        {
            if (Find(data).Count > 0)
                throw new ArgumentException("Cannot insert duplicate values");

            if ((_count + 1) / (double)_hashTable.Length > _expandFactor)
                Expand();

            int index = GetIndex(data.GetHashCode());

            if (_hashTable[index] == null)
                _hashTable[index] = new LinkedList<T>();

            _hashTable[index].AddLast(data);
            _count++;
        }

        public void Update(T oldData, T newData)
        {
            Delete(oldData);
            Insert(newData);
        }

        public void Delete(T data)
        {
            if (Find(data).Count == 0)
                throw new ArgumentException("Data not found");

            _hashTable[GetIndex(data.GetHashCode())].Remove(data);
            _count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_count == 0)
                yield break;

            for (int i = 0; i < _hashTable.Length; i++)
            {
                if (_hashTable[i] != null)
                {
                    foreach (var item in _hashTable[i])
                        yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetIndex(int hash) => hash % _hashTable.Length;

        private void Expand()
        {
            var newTable = new LinkedList<T>[_hashTable.Length * 2];

            foreach (var item in this)
            {
                var index = item.GetHashCode() % newTable.Length;

                if (newTable[index] == null)
                    newTable[index] = new LinkedList<T>();

                newTable[index].AddLast(item);
            }

            _hashTable = newTable;
        }
    }
}
