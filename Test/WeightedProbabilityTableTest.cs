using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable.Test
{
    public class WeightedProbabilityTableTest
    {
        // Test that the Add method adds an item to the table
        [Test]
        public void Add_WeightedProbabilityTableItem_AddsItemToList()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            var mockItem = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(
                item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            // Act
            table.Add(mockItem.Object);

            // Assert
            Assert.Contains(mockItem.Object, table.Items);
        }

        // Test that the Add method throws an exception if the item already exists in the table
        [Test]
        public void Add_WeightedProbabilityTableItem_ItemAlreadyExists_ThrowsArgumentException()
        {
            // Arrange
            TestObject item1 = new TestObject(1);

            var mockItem = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            table.Add(mockItem.Object);

            // Act
            TestDelegate testDelegate = () => table.Add(mockItem.Object);

            // Assert
            Assert.Throws<System.ArgumentException>(testDelegate);
        }

        // Test that the Add method adds a collection of items to the table
        [Test]
        public void Add_IEnumerableOfWeightedProbabilityTableItem_AddsItemsToList()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            // Act
            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object, mockItem3.Object });

            // Assert
            Assert.Contains(mockItem1.Object, table.Items);
            Assert.Contains(mockItem2.Object, table.Items);
            Assert.Contains(mockItem3.Object, table.Items);
        }

        // Test that the Add method throws an exception if any of the items already exist in the table
        [Test]
        public void Add_IEnumerableOfWeightedProbabilityTableItem_SomeNewItemsToAdd_ThrowsArgumentException()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(mockItem1.Object);

            // Act
            TestDelegate testDelegate = () => table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object, mockItem3.Object });

            // Assert
            Assert.Throws<System.ArgumentException>(testDelegate);
        }

        // Test that the Add method throws an exception if all of the items already exist in the table
        [Test]
        public void Add_IEnumerableOfWeightedProbabilityTableItem_NoNewItemsToAdd_ThrowsArgumentException()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object, mockItem3.Object });

            // Act
            TestDelegate testDelegate = () => table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object, mockItem3.Object });

            // Assert
            Assert.Throws<System.ArgumentException>(testDelegate);
        }

        // Test that the Contains method returns true if the item exists in the table
        [Test]
        public void Contains_ItemExists_ReturnsTrue()
        {
            // Arrange
            TestObject item1 = new TestObject(1);

            var mockItem = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(mockItem.Object);

            // Act
            bool result = table.Contains(mockItem.Object.Item);

            // Assert
            Assert.IsTrue(result);
        }

        // Test that the Contains method returns false if the item does not exist in the table
        [Test]
        public void Contains_ItemDoesNotExist_ReturnsFalse()
        {
            // Arrange
            TestObject item1 = new TestObject(1);

            var mockItem = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            // Act
            bool result = table.Contains(mockItem.Object.Item);

            // Assert
            Assert.IsFalse(result);
        }

        // Test that the ContainsAll method returns true if all of the items exist in the table
        [Test]
        public void ContainsAll_AllItemsExist_ReturnsTrue()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object, mockItem3.Object });

            // Act
            bool result = table.ContainsAll(new TestObject[] { mockItem1.Object.Item, mockItem2.Object.Item, mockItem3.Object.Item });

            // Assert
            Assert.IsTrue(result);
        }

        // Test that the ContainsAll method returns false if any of the items do not exist in the table
        [Test]
        public void ContainsAll_NotAllItemsExist_ReturnsFalse()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object });

            // Act
            bool result = table.ContainsAll(new TestObject[] { mockItem1.Object.Item, mockItem2.Object.Item, mockItem3.Object.Item });

            // Assert
            Assert.IsFalse(result);
        }

        // Test that the ContainsAny method returns true if any of the items exist in the table
        [Test]
        public void ContainsAny_AnyItemsExist_ReturnsTrue()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object });

            // Act
            bool result = table.ContainsAny(new TestObject[] { mockItem1.Object.Item, mockItem2.Object.Item, mockItem3.Object.Item });

            // Assert
            Assert.IsTrue(result);
        }

        // Test that the ContainsAny method returns false if none of the items exist in the table
        [Test]
        public void ContainsAny_AnyItemsExist_ReturnsFalse()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            // Act
            bool result = table.ContainsAny(new TestObject[] { mockItem1.Object.Item, mockItem2.Object.Item, mockItem3.Object.Item });

            // Assert
            Assert.IsFalse(result);
        }

        // Test that the Remove method removes the item from the table
        [Test]
        public void Remove_ItemExists_RemovesItemFromList()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            
            var mockItem = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
               new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);
            table.Add(mockItem.Object);

            // Act
            table.Remove(mockItem.Object.Item);

            // Assert
            Assert.IsFalse(table.Items.Contains(mockItem.Object));
        }

        // Test that the Clear method removes all of the items from the table
        [Test]
        public void Clear_ListIsNotEmpty_RemovesAllItemsFromList()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2);
            TestObject item3 = new TestObject(3);

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem3 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item3,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item3, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object, mockItem3.Object });

            // Act
            table.Clear();

            // Assert
            Assert.AreEqual(table.Items.Count, 0);
            Assert.IsFalse(table.Contains(mockItem1.Object.Item));
            Assert.IsFalse(table.Contains(mockItem2.Object.Item));
            Assert.IsFalse(table.Contains(mockItem3.Object.Item));
        }

        // Test that the Get method return an item from the table if one exists
        [Test]
        public void Get_ItemExists_ReturnsItem()
        {
            // Arrange
            TestObject item1 = new TestObject(1);

            var mockItem = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            table.Add(mockItem.Object);

            // Act
            object result = table.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(item1, result);
        }

        // Test that the Get method returns null if the table is empty
        [Test]
        public void Get_ListIsEmpty_ReturnsNull()
        {
            // Arrange
            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            // Act
            object result = table.Get();

            // Assert
            Assert.IsNull(result);
        }

        // Test that the weight of an item is the same after getting it from the table when the sample mode is SampleWithReplacement
        [Test]
        public void Get_SampleModeIsSampleWithReplacement_WeightIsSame()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2); // Need to add two items so the table doesn't reset after getting the first item

            float itemWeight = 1f;

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, itemWeight, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, itemWeight, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object });

            // Act
            TestObject result = table.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(itemWeight, table.Items.First(i => i.Item.Equals(result)).Weight.CurrentWeight);
        }

        // Test that the weight of an item is zero after getting it from the table when the sample mode is SampleWithoutReplacement
        [Test]
        public void Get_SampleModeIsSampleWithoutReplacement_WeightIsZero()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2); // Need to add two items so the table doesn't reset after getting the first item

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, 1f, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);

            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object });

            // Act
            object result = table.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0f, table.Items.First(i => i.Item.Equals(result)).Weight.CurrentWeight);
        }

        // Test that the weight of an item decays by the correct amount after getting it from the table when the sample mode is SampleWithWeightDecay
        [Test]
        public void Get_SampleModeIsSampleWithWeightDecay_WeightIsDecayed()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2); // Need to add two items so the table doesn't reset after getting the first item

            float itemWeight = 1f;
            int itemCycleSelectionLimit = 2;

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, itemWeight, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, itemWeight, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithWeightDecay);

            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object });

            // Act
            object result = table.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(itemWeight / itemCycleSelectionLimit, table.Items.First(i => i.Item.Equals(result)).Weight.CurrentWeight);
        }

        // Test that the weight of an item is zero after getting it from the table when the sample mode is SampleWithWeightDecay and the item has reached its decay limit
        [Test]
        public void Get_ItemHasReachedDecayLimit_WeightIsZero()
        {
            // Arrange
            TestObject item1 = new TestObject(1);
            TestObject item2 = new TestObject(2); // Need to add two items so the table doesn't reset after getting the first item

            float itemWeight = 1f;
            int itemCycleSelectionLimit = 1;

            var mockItem1 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item1,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item1, itemWeight, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object);

            var mockItem2 = new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item2,
                new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item2, itemWeight, null).Object,
                new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object);

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithWeightDecay);

            table.Add(new WeightedProbabilityTableItem<TestObject, TestSelectionContext>[] { mockItem1.Object, mockItem2.Object });

            // Act
            object result = table.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0f, table.Items.First(i => i.Item.Equals(result)).Weight.CurrentWeight);
        }

        // Test that the probability that an item is selected is correct when the sample mode is SampleWithReplacement
        // NOTE: It is expected that this test fails about 5% of the time. This is due to the fact that the test is
        // probabilistic. If the test fails occasionally it is not necessarily an indication that the code is incorrect,
        // but if it fails consistently then that would be a cause for concern.
        [Test]
        public void Get_SampleModeIsSampleWithReplacement_ProbabilityIsCorrect()
        {
            int numItems = 10;
            int numSelections = 10000;

            List<TestObject> testObjectList = new List<TestObject>();
            Dictionary<TestObject, int> itemSelectionCount = new Dictionary<TestObject, int>();

            // Arrange
            for (int i = 0; i < numItems; i++)
            {
                TestObject item = new TestObject(i);
                testObjectList.Add(item);
                itemSelectionCount.Add(item, 0);
            }

            float itemWeight = 1f;
            float totalWeight = 0f;

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithReplacement);

            for (int i = 0; i < numItems; i++)
            {
                table.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(testObjectList[i],
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(testObjectList[i], itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object).Object);

                totalWeight += itemWeight;
            }

            // Act
            for (int i = 0; i < numSelections; i++)
            {
                TestObject result = table.Get();
                itemSelectionCount[result]++;
            }

            string debugString = "Item Selections:\n";

            float criticalValue = 16.919f; // This value was computed by ChatGPT based on alpha=0.05 and df=9
            float chiSquared = 0f;

            for (int i = 0; i < numItems; i++)
            {
                float expected = (itemWeight / totalWeight) * numSelections;
                float observed = itemSelectionCount[testObjectList[i]];

                chiSquared += Mathf.Pow(observed - expected, 2f) / expected;

                debugString += "Item #" + i + ": Expected=" + Mathf.RoundToInt(expected) + ", Observed: " + Mathf.RoundToInt(observed) + "\n";
            }

            Debug.Log((chiSquared < criticalValue ? "SUCCESS" : "FAILURE") + ": chiSquared=" + chiSquared + ", criticalValue=" + criticalValue + "\n\n" + debugString);

            // Assert
            Assert.IsTrue(chiSquared < criticalValue);
        }

        // Test that the probability that an item is selected is correct when the sample mode is SampleWithoutReplacement
        // NOTE: It is expected that this test fails about 5% of the time. This is due to the fact that the test is
        // probabilistic. If the test fails occasionally it is not necessarily an indication that the code is incorrect,
        // but if it fails consistently then that would be a cause for concern.
        [Test]
        public void Get_SampleModeIsSampleWithoutReplacement_ProbabilityIsCorrect()
        {
            int numItems = 10;
            int numSelections = 10000;

            List<TestObject> testObjectList = new List<TestObject>();
            Dictionary<TestObject, int> itemSelectionCount = new Dictionary<TestObject, int>();

            // Arrange
            for (int i = 0; i < numItems; i++)
            {
                TestObject item = new TestObject(i);
                testObjectList.Add(item);
                itemSelectionCount.Add(item, 0);
            }

            float itemWeight = 1f;
            float totalWeight = 0f;

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);

            for (int i = 0; i < numItems; i++)
            {
                table.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(testObjectList[i],
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(testObjectList[i], itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(false, -1, false, -1).Object).Object);

                totalWeight += itemWeight;
            }

            // Act
            for (int i = 0; i < numSelections; i++)
            {
                TestObject result = table.Get();
                itemSelectionCount[result]++;
            }

            string debugString = "Item Selections:\n";

            float criticalValue = 16.919f; // This value was computed by ChatGPT based on alpha=0.05 and df=9
            float chiSquared = 0f;

            for (int i = 0; i < numItems; i++)
            {
                float expected = (itemWeight / totalWeight) * numSelections;
                float observed = itemSelectionCount[testObjectList[i]];

                chiSquared += Mathf.Pow(observed - expected, 2f) / expected;

                debugString += "Item #" + i + ": Expected=" + Mathf.RoundToInt(expected) + ", Observed: " + Mathf.RoundToInt(observed) + "\n";
            }

            Debug.Log((chiSquared < criticalValue ? "SUCCESS" : "FAILURE") + ": chiSquared=" + chiSquared + ", criticalValue=" + criticalValue + "\n\n" + debugString);

            // Assert
            Assert.IsTrue(chiSquared < criticalValue);
        }
        
        [Test]
        public void Get_CopiedTableReturnsSameObjects()
        {
            // Arrange
            int numObjects = 100;

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);
                
                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            var tableCopy = table.Copy();

            // Act
            for (int i = 0; i < numObjects; i++)
            {
                object tableResult = table.Get();
                object tableCopyResult = tableCopy.Get();

                Debug.Log("tableResult=" + tableResult + ", tableCopyResult=" + tableCopyResult);

                // Assert
                Assert.IsNotNull(tableResult);
                Assert.IsNotNull(tableCopyResult);
                Assert.AreEqual(tableResult, tableCopyResult);
            }
        }

        [Test]
        public void GetIndex_ReturnsCorrectObject()
        {
            int numObjects = 100;
            int getIndex = UnityEngine.Random.Range(0, numObjects);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            var tableCopy = table.Copy();

            // Act
            TestObject getItem = null;

            for (int i = 0; i <= getIndex; i++)
                getItem = table.Get();

            TestObject getIndexItem = tableCopy.GetIndex(getIndex);

            Debug.Log("getItem[" + getIndex + "] = " + getItem + ", getIndexItem[" + getIndex + "]=" + getIndexItem);

            // Assert
            Assert.IsNotNull(getItem);
            Assert.IsNotNull(getIndexItem);
            Assert.AreEqual(getItem, getIndexItem);
        }

        [Test]
        public void GetIndex_CopiedTableReturnsSameObjects()
        {
            // Arrange
            int numObjects = 100;
            int numGets = 10;

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            var tableCopy = table.Copy();

            // Act
            for (int i = 0; i < numObjects / numGets; i++)
            {
                object tableResult = table.GetIndex(numGets);
                object tableCopyResult = tableCopy.GetIndex(numGets);

                Debug.Log("tableResult=" + tableResult + ", tableCopyResult=" + tableCopyResult);

                // Assert
                Assert.IsNotNull(tableResult);
                Assert.IsNotNull(tableCopyResult);
                Assert.AreEqual(tableResult, tableCopyResult);
            }
        }

        [Test]
        public void Peek_ReturnsSameAsGet()
        {
            // Arrange
            int numObjects = 100;

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var getTable = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            getTable.Add(mockItems.Select(m => m.Object).ToArray());

            var peekTable = getTable.Copy();

            // Act
            object getResult = getTable.Get();
            object peekResult = peekTable.Peek();

            Debug.Log(nameof(getResult) + "=" + getResult + ", " + nameof(peekResult) + "=" + peekResult);

            // Assert
            Assert.IsNotNull(getResult);
            Assert.IsNotNull(peekResult);
            Assert.AreEqual(getResult, peekResult);
        }

        [Test]
        public void PeekMultiple_ReturnsSameAsGetMultiple()
        {
            // Arrange
            int numObjects = 100;
            int getCount = UnityEngine.Random.Range(2, numObjects);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var getTable = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            getTable.Add(mockItems.Select(m => m.Object).ToArray());

            var peekTable = getTable.Copy();

            // Act
            TestObject[] getResult = getTable.Get(getCount);
            TestObject[] peekResult = peekTable.Peek(getCount);

            // Assert
            Assert.IsNotNull(getResult);
            Assert.IsNotNull(peekResult);
            Assert.AreEqual(getResult.Length, getCount);
            Assert.AreEqual(peekResult.Length, getCount);
            Assert.IsTrue(getResult.SequenceEqual(peekResult));
        }

        [Test]
        public void PeekIndex_ReturnsSameAsGetIndex()
        {
            // Arrange
            int numObjects = 100;
            int getIndex = UnityEngine.Random.Range(0, numObjects);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var getTable = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            getTable.Add(mockItems.Select(m => m.Object).ToArray());

            var peekTable = getTable.Copy();

            // Act
            object getIndexResult = getTable.GetIndex(getIndex);
            object peekIndexResult = peekTable.PeekIndex(getIndex);

            Debug.Log(nameof(getIndexResult) + "=" + getIndexResult + ", " + nameof(peekIndexResult) + "=" + peekIndexResult);

            // Assert
            Assert.IsNotNull(getIndexResult);
            Assert.IsNotNull(peekIndexResult);
            Assert.AreEqual(getIndexResult, peekIndexResult);
        }

        [Test]
        public void PeekIndexMultiple_ReturnsSameAsGetIndexMultiple()
        {
            // Arrange
            int numObjects = 100;
            int getIndex = UnityEngine.Random.Range(0, numObjects - 1);
            int getCount = UnityEngine.Random.Range(2, numObjects - getIndex);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var getTable = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            getTable.Add(mockItems.Select(m => m.Object).ToArray());

            var peekTable = getTable.Copy();

            // Act
            TestObject[] getResult = getTable.GetIndex(getIndex, getCount);
            TestObject[] peekResult = peekTable.PeekIndex(getIndex, getCount);

            // Assert
            Assert.IsNotNull(getResult);
            Assert.IsNotNull(peekResult);
            Assert.AreEqual(getResult.Length, getCount);
            Assert.AreEqual(peekResult.Length, getCount);
            Assert.IsTrue(getResult.SequenceEqual(peekResult));
        }

        [Test]
        public void Peek_DoesntChangeNextGet()
        {
            // Arrange
            int numObjects = 100;

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            // Act
            TestObject peekResult = table.Peek();
            TestObject getResult = table.Get();

            // Assert
            Assert.IsNotNull(peekResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(peekResult, getResult);
        }

        [Test]
        public void PeekMultiple_DoesntChangeNextGetMultiple()
        {
            // Arrange
            int numObjects = 100;
            int getCount = UnityEngine.Random.Range(2, numObjects);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            // Act
            TestObject[] peekResult = table.Peek(getCount);
            TestObject[] getResult = table.Get(getCount);

            // Assert
            Assert.IsNotNull(peekResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(peekResult.Length, getCount);
            Assert.AreEqual(getResult.Length, getCount);
            Assert.IsTrue(getResult.SequenceEqual(peekResult));
        }

        [Test]
        public void PeekIndex_DoesntChangeNextGetIndex()
        {
            // Arrange
            int numObjects = 100;
            int getIndex = UnityEngine.Random.Range(0, numObjects);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            // Act
            TestObject peekResult = table.PeekIndex(getIndex);
            TestObject getResult = table.GetIndex(getIndex);

            // Assert
            Assert.IsNotNull(peekResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(peekResult, getResult);
        }

        [Test]
        public void PeekIndexMultiple_DoesntChangeNextGetIndexMultiple()
        {
            // Arrange
            int numObjects = 100;
            int getIndex = UnityEngine.Random.Range(0, numObjects);
            int getCount = UnityEngine.Random.Range(2, numObjects - getIndex);

            List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>> mockItems = new List<Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>>();

            for (int i = 0; i < numObjects; i++)
            {
                TestObject item = new TestObject(i);

                float itemWeight = 1f;
                int itemCycleSelectionLimit = 1;

                mockItems.Add(new Mock<WeightedProbabilityTableItem<TestObject, TestSelectionContext>>(item,
                    new Mock<WeightedProbabilityTableItemWeight<TestObject, TestSelectionContext>>(item, itemWeight, null).Object,
                    new Mock<WeightedProbabilityTableItemSelectionInfo>(true, itemCycleSelectionLimit, false, -1).Object));
            }

            var table = new WeightedProbabilityTable<TestObject, TestSelectionContext>(SampleMode.SampleWithoutReplacement);
            table.Add(mockItems.Select(m => m.Object).ToArray());

            // Act
            TestObject[] peekResult = table.PeekIndex(getIndex, getCount);
            TestObject[] getResult = table.GetIndex(getIndex, getCount);

            // Assert
            Assert.IsNotNull(peekResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(peekResult.Length, getCount);
            Assert.AreEqual(getResult.Length, getCount);
            Assert.IsTrue(getResult.SequenceEqual(peekResult));
        }

        private class TestObject : ICloneable
        {
            int Value;

            public TestObject()
            {
                Value = 0;
            }

            public TestObject(int value)
            {
                Value = value;
            }

            public TestObject(TestObject other)
            {
                Value = other.Value;
            }

            public object Clone()
            {
                return new TestObject(this);
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj is not TestObject other) return false;

                return Value == other.Value;
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }

            public override string ToString()
            {
                return typeof(TestObject).Name + " { Value=" + Value + " }";
            }
        }

        private class TestSelectionContext : WeightedProbabilityTableItemSelectionContext
        {

        }
    }
}
