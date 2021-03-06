﻿using Structures.Hashing;
using Structures.Interface;
using Structures.Tree;
using System;
using System.Collections.Generic;

namespace Structures
{
    /// <summary>
    /// Factory for providing instances of data structures in <see cref="Structures"/> namespace
    /// </summary>
    public class StructureFactory
    {
        private static object _lock = new object();
        private static volatile StructureFactory _instance;

        protected StructureFactory() { }

        /// <summary>
        /// Instance of the factory
        /// </summary>
        public static StructureFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new StructureFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets K-d tree
        /// </summary>
        /// <typeparam name="T">Type of elements in K-d tree</typeparam>
        /// <returns>Empty K-d tree</returns>
        public IBSTree<T> GetKdTree<T>() where T : IKdComparable
        {
            return new KdTree<T>();
        }

        /// <summary>
        /// Constructs balanced K-d tree from <paramref name="data"/>
        /// </summary>
        /// <typeparam name="T">Type of elements in K-d tree</typeparam>
        /// <param name="data">Data used in K-d tree construction</param>
        /// <returns>Balanced K-d tree constructed from <paramref name="data"/></returns>
        public IBSTree<T> GetKdTree<T>(IEnumerable<T> data) where T : IKdComparable
        {
            return new KdTree<T>(data);
        }

        /// <summary>
        /// Loads instance of extendible hashing structure from <paramref name="directory"/>
        /// </summary>
        /// <typeparam name="T">Type of elements stored at extendible hashing</typeparam>
        /// <param name="directory">Path to directory where the structure is stored</param>
        /// <returns>Extendible hashing loaded from <paramref name="directory"/></returns>
        public IHashFile<T> GetExtendibleHashing<T>(string directory, T prototype) where T : ISerializable
        {
            return new ExtendibleHashing<T>(directory, prototype);
        }

        /// <summary>
        /// Gets instance of extendible hashing structure
        /// </summary>
        /// <typeparam name="T">Type of elements stored at extendible hashing</typeparam>
        /// <param name="directory">Path to directory where the structure will be stored</param>
        /// <param name="clusterSize">File system's cluster size in bytes</param>
        /// <returns>New extendible hashing that will be stored at <paramref name="directory"/></returns>
        public IHashFile<T> GetExtendibleHashing<T>(string directory, int clusterSize, T prototype) where T : ISerializable
        {
            return new ExtendibleHashing<T>(directory, clusterSize, prototype);
        }

        /// <summary>
        /// Gets instance of hash set structure
        /// </summary>
        /// <typeparam name="T">Type of elements stored at hash set</typeparam>
        /// <returns>Hash set structure</returns>
        public ITable<T> GetHashSet<T>()
        {
            return new Hashing.HashSet<T>();
        }

        /// <summary>
        /// Gets hash set structure with specific initial capacity
        /// </summary>
        /// <typeparam name="T">Type of elements stored at hash set</typeparam>
        /// <param name="initialCapacity"></param>
        /// <returns>Hash set structure</returns>
        public ITable<T> GetHashSet<T>(int initialCapacity)
        {
            return new Hashing.HashSet<T>(initialCapacity);
        }

        /// <summary>
        /// Get instance of hash set pre-filled with <paramref name="data"/>
        /// </summary>
        /// <typeparam name="T">Type of elements stored at hash set</typeparam>
        /// <param name="data">Data to be filled into the hash set</param>
        /// <returns></returns>
        public ITable<T> GetHashSet<T>(IEnumerable<T> data)
        {
            return new Hashing.HashSet<T>(data);
        }

        /// <summary>
        /// Gets instance of AVL tree structure
        /// </summary>
        /// <typeparam name="T">Type of elements stored at AVL tree</typeparam>
        /// <returns>AVL tree structure</returns>
        public IBSTree<T> GetAvlTree<T>() where T : IComparable
        {
            return new AvlTree<T>();
        }
    }
}
