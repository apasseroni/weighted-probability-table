using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable.Examples
{
    /// <summary>
    /// This class provides a simple example for how to populate and select items from a weighted probability table using the SampleWithoutReplacement sample mode.
    /// Notice that each item is guaranteed to be selected once per cycle.
    /// </summary>
    public class SampleWithoutReplacementExample : MonoBehaviour
    {
        [SerializeField]
        private float _commonItemWeight = .5f;
        [SerializeField]
        private float _uncommonItemWeight = .25f;
        [SerializeField]
        private float _rareItemWeight = .15f;
        [SerializeField]
        private float _epicItemWeight = .08f;
        [SerializeField]
        private float _legendaryItemWeight = .02f;
        [SerializeField]
        private int _numCommonItems = 50;
        [SerializeField]
        private int _numUncommonItems = 25;
        [SerializeField]
        private int _numRareItems = 15;
        [SerializeField]
        private int _numEpicItems = 8;
        [SerializeField]
        private int _numLegendaryItems = 2;
        [SerializeField]
        private int _numberOfItemsToSelect = 100;

        private WeightedProbabilityTable<ExampleItem, ExampleSelectionContext> simpleTable;

        private void Start()
        {
            // Create a table using the SampleWithoutReplacement sample mode
            simpleTable = new WeightedProbabilityTable<ExampleItem, ExampleSelectionContext>(SampleMode.SampleWithoutReplacement);

            // Add items to the table
            AddItems(RarityEnum.Common, _commonItemWeight, _numCommonItems);
            AddItems(RarityEnum.Uncommon, _uncommonItemWeight, _numUncommonItems);
            AddItems(RarityEnum.Rare, _rareItemWeight, _numRareItems);
            AddItems(RarityEnum.Epic, _epicItemWeight, _numEpicItems);
            AddItems(RarityEnum.Legendary, _legendaryItemWeight, _numLegendaryItems);

            // Create a map to keep track of the number of times each item was selected
            Dictionary<ExampleItem, int> itemSelectionCount = new Dictionary<ExampleItem, int>();

            // Create a map to keep track of the number of times an item of each rarity was selected
            Dictionary<RarityEnum, int> raritySelectionCount = new Dictionary<RarityEnum, int>();

            // Select items from the table
            for (int i = 0; i < _numberOfItemsToSelect; i++)
            {
                // Select an item from the table
                ExampleItem selectedItem = simpleTable.Get();

                if (!itemSelectionCount.ContainsKey(selectedItem))
                    itemSelectionCount.Add(selectedItem, 1);
                else
                    itemSelectionCount[selectedItem]++;

                if (!raritySelectionCount.ContainsKey(selectedItem.Rarity))
                    raritySelectionCount.Add(selectedItem.Rarity, 1);
                else
                    raritySelectionCount[selectedItem.Rarity]++;
            }

            // Print the number of times an item of each rarity was selected
            foreach (RarityEnum rarity in Enum.GetValues(typeof(RarityEnum)))
            {
                if (raritySelectionCount.ContainsKey(rarity))
                {
                    float percent = (float)raritySelectionCount[rarity] / _numberOfItemsToSelect * 100;
                    Debug.Log("A " + rarity + " item was selected " + raritySelectionCount[rarity] + " times (" + percent + "%)");
                }
            }

            // Print the number of times each item was selected
            foreach (ExampleItem exampleItem in itemSelectionCount.Keys.OrderBy(i => i.Name))
            {
                if (itemSelectionCount.ContainsKey(exampleItem))
                {
                    float percent = (float)itemSelectionCount[exampleItem] / _numberOfItemsToSelect * 100;
                    Debug.Log(exampleItem.Name + " was selected " + itemSelectionCount[exampleItem] + " times (" + percent + "%)");
                }
            }
        }

        /// <summary>
        /// Adds the specified number of items of the given rarity to the table with the provided weight
        /// </summary>
        /// <param name="rarity"></param>
        /// <param name="weight"></param>
        private void AddItems(RarityEnum rarity, float weight, int count)
        {
            for (int i = 0; i < count; i++)
            {
                // Create the item
                ExampleItem exampleItem = new ExampleItem(Enum.GetName(typeof(RarityEnum), rarity) + "Item" + i, rarity);

                // Create the table entry weight
                WeightedProbabilityTableItemWeight<ExampleItem, ExampleSelectionContext> exampleItemWeight =
                    new WeightedProbabilityTableItemWeight<ExampleItem, ExampleSelectionContext>(exampleItem, weight);

                // Create the table entry selection info (no selection limits need to be specified for this example)
                WeightedProbabilityTableItemSelectionInfo exampleItemSelectionInfo =
                    new WeightedProbabilityTableItemSelectionInfo();

                // Create the table entry
                WeightedProbabilityTableItem<ExampleItem, ExampleSelectionContext> tableEntry =
                    new WeightedProbabilityTableItem<ExampleItem, ExampleSelectionContext>(exampleItem, exampleItemWeight, exampleItemSelectionInfo);

                // Add the table entry to the table
                simpleTable.Add(tableEntry);
            }
        }


        /// <summary>
        /// A simple class to represent an item with a name and rarity
        /// </summary>
        private class ExampleItem : ICloneable
        {
            public string Name { get; private set; }
            public RarityEnum Rarity { get; private set; }

            public ExampleItem(string name, RarityEnum rarity)
            {
                Name = name;
                Rarity = rarity;
            }

            public object Clone()
            {
                return new ExampleItem(Name, Rarity);
            }
        }


        /// <summary>
        /// A simple selection context for the example. The selection context is not used in this example.
        /// </summary>
        private class ExampleSelectionContext : WeightedProbabilityTableItemSelectionContext
        {
            // No selection context needed for this example
        }

        private enum RarityEnum
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }
    }
}