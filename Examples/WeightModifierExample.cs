using System;
using System.Collections.Generic;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable.Examples
{
    /// <summary>
    /// This class provides a simple example for how to populate and select items from a weighted probability table using a weight modifier to control which items can be selected.
    /// Notice that only the Epic and Legendary items are ever selected.
    /// </summary>
    public class WeightModifierExample : MonoBehaviour
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
        private RarityEnum _canSelectRarities = RarityEnum.Epic | RarityEnum.Legendary;
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

            ExampleSelectionContext selectionContext = new ExampleSelectionContext(_canSelectRarities);

            // Select items from the table
            for (int i = 0; i < _numberOfItemsToSelect; i++)
            {
                // Select an item from the table
                ExampleItem selectedItem = simpleTable.Get(selectionContext);

                if (!raritySelectionCount.ContainsKey(selectedItem.Rarity))
                    raritySelectionCount.Add(selectedItem.Rarity, 1);
                else
                    raritySelectionCount[selectedItem.Rarity]++;
            }

            // Print the number of times the item of each rarity was selected
            foreach (RarityEnum rarity in Enum.GetValues(typeof(RarityEnum)))
            {
                if (rarity == RarityEnum.None)
                    continue;

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

            // Create a list of weight modifiers containing one of our example weight modifiers
            List<WeightedProbabilityTableItemWeightModifier<ExampleItem, ExampleSelectionContext>> modifiers =
                new List<WeightedProbabilityTableItemWeightModifier<ExampleItem, ExampleSelectionContext>>() { new ExampleWeightModifier() };

            // Create the table entry weight
            WeightedProbabilityTableItemWeight<ExampleItem, ExampleSelectionContext> exampleItemWeight =
                new WeightedProbabilityTableItemWeight<ExampleItem, ExampleSelectionContext>(exampleItem, weight, modifiers);

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
        /// A selection context that contains the rarities that can be selected from the table
        /// </summary>
        private class ExampleSelectionContext : WeightedProbabilityTableItemSelectionContext
        {
            public RarityEnum CanSelectRarities { get; private set; }

            public ExampleSelectionContext(RarityEnum canSelectRarities)
            {
                CanSelectRarities = canSelectRarities;
            }
        }

        /// <summary>
        /// A weight modifier that limits the selection of items based on the rarities that can be selected
        /// </summary>
        private class ExampleWeightModifier : WeightedProbabilityTableItemWeightModifier<ExampleItem, ExampleSelectionContext>
        {
            public override float Modify(ExampleItem item, float weight, ExampleSelectionContext context)
            {
                if (context.CanSelectRarities.HasFlag(item.Rarity))
                    return weight;

                return 0f;
            }

            public override WeightedProbabilityTableItemWeightModifier<ExampleItem, ExampleSelectionContext> Copy()
            {
                return new ExampleWeightModifier();
            }
        }

        [Flags]
        private enum RarityEnum
        {
            None = 0,
            Common = 1 << 0,
            Uncommon = 1 << 1,
            Rare = 1 << 2,
            Epic = 1 << 3,
            Legendary = 1 << 4
        }
    }
}