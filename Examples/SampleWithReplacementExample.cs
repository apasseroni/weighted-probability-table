using System;
using System.Collections.Generic;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable.Examples
{
    /// <summary>
    /// This class provides a simple example for how to populate and select items from a weighted probability table using the SampleWithReplacement sample mode.
    /// Notice that each item is selected approximately the number of times that is expected based on its weight, but there is some variance.
    /// </summary>
    public class SampleWithReplacementExample : MonoBehaviour
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
        private int _numberOfItemsToSelect = 100;

        private WeightedProbabilityTable<ExampleItem, ExampleSelectionContext> simpleTable;

        private void Start()
        {
            // Create a table using the SampleWithReplacement sample mode
            simpleTable = new WeightedProbabilityTable<ExampleItem, ExampleSelectionContext>(SampleMode.SampleWithReplacement);

            // Add items to the table
            AddItem(RarityEnum.Common, _commonItemWeight);
            AddItem(RarityEnum.Uncommon, _uncommonItemWeight);
            AddItem(RarityEnum.Rare, _rareItemWeight);
            AddItem(RarityEnum.Epic, _epicItemWeight);
            AddItem(RarityEnum.Legendary, _legendaryItemWeight);

            // Create a map to keep track of the number of times an item of each rarity was selected
            Dictionary<RarityEnum, int> raritySelectionCount = new Dictionary<RarityEnum, int>();

            // Select items from the table
            for (int i = 0; i < _numberOfItemsToSelect; i++)
            {
                // Select an item from the table
                ExampleItem selectedItem = simpleTable.Get();

                if (!raritySelectionCount.ContainsKey(selectedItem.Rarity))
                    raritySelectionCount.Add(selectedItem.Rarity, 1);
                else
                    raritySelectionCount[selectedItem.Rarity]++;
            }

            // Print the number of times the item of each rarity was selected
            foreach (RarityEnum rarity in Enum.GetValues(typeof(RarityEnum)))
            {
                if (raritySelectionCount.ContainsKey(rarity))
                {
                    float percent = (float)raritySelectionCount[rarity] / _numberOfItemsToSelect * 100;
                    Debug.Log("The " + rarity + " item was selected " + raritySelectionCount[rarity] + " times (" + percent + "%)");
                }
            }
        }

        /// <summary>
        /// Adds an item of the given rarity to the table with the provided weight
        /// </summary>
        /// <param name="rarity"></param>
        /// <param name="weight"></param>
        private void AddItem(RarityEnum rarity, float weight)
        {
            // Create the item
            ExampleItem exampleItem = new ExampleItem(Enum.GetName(typeof(RarityEnum), rarity) + "Item", rarity);

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