﻿using Structures;
using Structures.Helper;
using System;
using System.Linq;
using Xunit;

namespace StructuresTests
{
    public class AvlTreeTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        [InlineData(100_000)]
        public void InsertionTest(int nodeCount)
        {
            var tree = StructureFactory.Instance.GetAvlTree<TwoDimObject>();
            ITableTests.InsertionTest(tree, nodeCount, (found, item) => found.Count == 1 && item.CompareTo(found.First()) == 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        [InlineData(100_000)]
        public void DeletionTest(int nodeCount)
        {
            var tree = StructureFactory.Instance.GetAvlTree<TwoDimObject>();
            ITableTests.DeletionTest(tree, nodeCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        public void RandomInsertDeleteTest(int nodeCount)
        {
            var tree = StructureFactory.Instance.GetAvlTree<TwoDimObject>();
            ITableTests.RandomInsertDeletTest(tree, nodeCount, (found, item) => found.Count == 1 && item.CompareTo(found.First()) == 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        [InlineData(100_000)]
        public void MinMaxTest(int nodeCount)
        {
            var data = Generator.GenerateRandomData(nodeCount);
            var tree = StructureFactory.Instance.GetAvlTree<TwoDimObject>();

            foreach (var item in data)
            {
                tree.Insert(item);
            }

            foreach (var item in data)
            {
                var found = tree.Find(item);
                Assert.True(found.Count == 1 && item.CompareTo(found.First()) == 0, "Data not inserted properly");
            }

            Assert.Equal(tree.Min.PrimaryKey, data.Min(x => x.PrimaryKey));
            Assert.Equal(tree.Max.PrimaryKey, data.Max(x => x.PrimaryKey));

            foreach (var item in data)
            {
                tree.Delete(item);
                var found = tree.Find(item);
                Assert.True(found.Count == 0, "Data not deleted properly");
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1_000)]
        [InlineData(10_000)]
        [InlineData(100_000)]
        public void CountTest(int nodeCount)
        {
            var data = Generator.GenerateRandomData(nodeCount).Shuffle();
            var tree = StructureFactory.Instance.GetAvlTree<TwoDimObject>();

            foreach (var item in data)
            {
                tree.Insert(item);
            }

            Assert.Equal(nodeCount, tree.Count);

            int n = nodeCount;
            var rand = new Random();
            int stop = rand.Next(0, n);

            foreach (var item in data)
            {
                tree.Delete(item);
                n--;
                if (n == stop)
                    break;
            }

            Assert.Equal(n, tree.Count);
        }

        [Fact]
        public void InsertDeleteTest_Fact()
        {
            for (int i = 3494; i < 3495; i++)
            {
                var rand = new Random(i);
                var data = Generator.GenerateRandomData(12).Shuffle(rand);
                var tree = StructureFactory.Instance.GetAvlTree<TwoDimObject>();

                foreach (var item in data)
                {
                    tree.Insert(item);
                }

                foreach (var item in data)
                {
                    var found = tree.Find(item);
                    Assert.True(found.Count == 1 && item.CompareTo(found.First()) == 0, $"Data not inserted properly, i: {i}");
                }

                foreach (var item in data)
                {
                    //try
                    //{
                    tree.Delete(item);
                    var found = tree.Find(item);
                    Assert.True(found.Count == 0, $"Data not deleted properly, i : {i}");
                    //}
                    //catch (Exception ex)
                    //{
                    //    Assert.True(false, $"exception: {ex}, i: {i}");
                    //}
                }
            }
        }
    }
}
