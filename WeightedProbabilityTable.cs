using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable
{
    [Serializable]
    public class WeightedProbabilityTable<TItem, TContext>
        where TItem : class, ICloneable
        where TContext : WeightedProbabilityTableItemSelectionContext
    {
        public List<WeightedProbabilityTableItem<TItem, TContext>> Items  => _items;

        public delegate bool FilterCriteria(TItem item);

        [SerializeField]
        private List<WeightedProbabilityTableItem<TItem, TContext>> _items;
        [SerializeField]
        private SampleMode _sampleMode;
        [SerializeField]
        private int _randSeed;
        [SerializeField]
        private UnityEngine.Random.State _getRandState;
        [SerializeField]
        private UnityEngine.Random.State _peekRandState;

        public WeightedProbabilityTable(SampleMode sampleMode)
        {
            _items = new List<WeightedProbabilityTableItem<TItem, TContext>>();
            _sampleMode = sampleMode;
            _randSeed = (int)Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
            UnityEngine.Random.State prevState = UnityEngine.Random.state;
            UnityEngine.Random.InitState(_randSeed);
            _getRandState = UnityEngine.Random.state;
            _peekRandState = _getRandState;
            UnityEngine.Random.state = prevState;
        }

        public WeightedProbabilityTable(WeightedProbabilityTable<TItem, TContext> other)
        {
            _items = new List<WeightedProbabilityTableItem<TItem, TContext>>();

            foreach (WeightedProbabilityTableItem<TItem, TContext> item in other._items)
                _items.Add(item.Copy());

            _sampleMode = other._sampleMode;
            _randSeed = other._randSeed;
            _getRandState = other._getRandState;
            _peekRandState = _getRandState;
        }

        public void Add(WeightedProbabilityTableItem<TItem, TContext> item)
        {
            if (Contains(item.Item))
                throw new ArgumentException("Item already exists in table");

            _items.Add(item);
        }

        public void Add(IEnumerable<WeightedProbabilityTableItem<TItem, TContext>> items)
        {
            if (ContainsAny(items.Select(i => i.Item)))
                throw new ArgumentException("No new items to add; Some or all of the items provided already exist in the table");

            _items.AddRange(items);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(TItem item)
        {
            return _items.Any(i => i.Item.Equals(item));
        }

        public bool ContainsAll(IEnumerable<TItem> items)
        {
            return items.All(i => Contains(i));
        }

        public bool ContainsAny(IEnumerable<TItem> items)
        {
            return items.Any(i => Contains(i));
        }

        public void Copy(WeightedProbabilityTable<TItem, TContext> other)
        {
            _sampleMode = other._sampleMode;
            _items.Clear();

            foreach (WeightedProbabilityTableItem<TItem, TContext> item in other._items)
                _items.Add(new WeightedProbabilityTableItem<TItem, TContext>(item));
        }

        public WeightedProbabilityTable<TItem, TContext> Copy()
        {
            return new WeightedProbabilityTable<TItem, TContext>(this);
        }

        public void Remove(TItem item)
        {
            if (!Contains(item))
                return;

            WeightedProbabilityTableItem<TItem, TContext> itemToRemove = _items.FirstOrDefault(i => i.Item.Equals(item));
            _items.Remove(itemToRemove);
        }

        public void Remove(IEnumerable<TItem> items)
        {
            foreach (TItem item in items)
                Remove(item);
        }

        public TItem Get(TContext context = null, FilterCriteria filterCriteria = null)
        {
            TItem[] items = Get(1, context, filterCriteria);

            if (items.Length > 0) return items[0];
            return null;
        }

        public TItem[] Get(int count, TContext context = null, FilterCriteria filterCriteria = null)
        {
            return GetIndex(0, count, context, filterCriteria);
        }

        public TItem GetIndex(int index, TContext context = null, FilterCriteria filterCriteria = null)
        {
            TItem[] items = GetIndex(index, 1, context, filterCriteria);

            if (items.Length > 0) return items[0];
            return null;
        }

        public TItem[] GetIndex(int index, int count, TContext context = null, FilterCriteria filterCriteria = null)
        {
            // Skip the first "index" items
            List<WeightedProbabilityTableItem<TItem, TContext>> skippedItems = new List<WeightedProbabilityTableItem<TItem, TContext>>();

            for (int i = 0; i < index; i++)
            {
                WeightedProbabilityTableItem<TItem, TContext> skippedItem = GetTableItem(context, filterCriteria, ref _getRandState);

                if (skippedItem == null)
                    break;

                skippedItems.Add(skippedItem);
            }

            // Get the next "count" items
            List<WeightedProbabilityTableItem<TItem, TContext>> selectedItems = new List<WeightedProbabilityTableItem<TItem, TContext>>();

            for (int i = 0; i < count; i++)
            {
                WeightedProbabilityTableItem<TItem, TContext> selectedItem = GetTableItem(context, filterCriteria, ref _getRandState);

                if (selectedItem == null)
                    break;

                selectedItems.Add(selectedItem);
            }

            // Replace the skipped items
            for (int i = 0; i < skippedItems.Count; i++)
                ReplaceTableItem(skippedItems[i]);

            return selectedItems.Select(i => i.Item).ToArray();
        }

        public TItem Peek(TContext context = null, FilterCriteria filterCriteria = null)
        {
            TItem[] items = Peek(1, context, filterCriteria);

            if (items.Length > 0) return items[0];
            return null;
        }

        public TItem[] Peek(int count, TContext context = null, FilterCriteria filterCriteria = null)
        {
            return PeekIndex(0, count, context, filterCriteria);
        }

        public TItem PeekIndex(int index, TContext context = null, FilterCriteria filterCriteria = null)
        {
            TItem[] items = PeekIndex(index, 1, context, filterCriteria);

            if (items.Length > 0) return items[0];
            return null;
        }

        public TItem[] PeekIndex(int index, int count, TContext context = null, FilterCriteria filterCriteria = null)
        {
            SyncPeekRandState();

            // Can't get more items than are remaining in the table
            //count = Mathf.Min(count, GetRemainingItemCount());

            // If we're peeking past the end of the table, the table will become exhausted and all item weights will be reset.
            // In order to be able to restore the state of the table after the peek, we need to cache the table item states
            List<WeightedProbabilityTableItem<TItem, TContext>> cachedItems = null;

            bool tableExhausted = index + count >= GetRemainingItemCount();

            if (tableExhausted)
            {
                cachedItems = new List<WeightedProbabilityTableItem<TItem, TContext>>();

                foreach (WeightedProbabilityTableItem<TItem, TContext> item in _items)
                    cachedItems.Add(item.Copy());
            }

            // Skip the first "index" items
            List<WeightedProbabilityTableItem<TItem, TContext>> skippedItems = new List<WeightedProbabilityTableItem<TItem, TContext>>();

            for (int i = 0; i < index; i++)
            {
                WeightedProbabilityTableItem<TItem, TContext> skippedItem = GetTableItem(context, filterCriteria, ref _peekRandState);

                if (skippedItem == null)
                    break;

                skippedItems.Add(skippedItem);
            }

            // Get the next "count" items
            List<WeightedProbabilityTableItem<TItem, TContext>> selectedItems = new List<WeightedProbabilityTableItem<TItem, TContext>>();

            for (int i = 0; i < count && GetRemainingItemCount() > 0; i++)
            {
                WeightedProbabilityTableItem<TItem, TContext> selectedItem = GetTableItem(context, filterCriteria, ref _peekRandState);

                if (selectedItem == null)
                    break;

                selectedItems.Add(selectedItem);
            }

            if (tableExhausted)
            {
                _items.Clear();
                _items.AddRange(cachedItems);
            }
            else
            {
                // Replace the skipped items
                for (int i = 0; i < skippedItems.Count; i++)
                    ReplaceTableItem(skippedItems[i]);

                // Replace the selected items
                for (int i = 0; i < selectedItems.Count; i++)
                    ReplaceTableItem(selectedItems[i]);
            }

            SyncPeekRandState();

            return selectedItems.Select(i => i.Item).ToArray();
        }

        private WeightedProbabilityTableItem<TItem, TContext> GetTableItem(TContext context, FilterCriteria filterCriteria, ref UnityEngine.Random.State randState)
        {
            WeightedProbabilityTableItem<TItem, TContext> selectedItem = null;

            Dictionary<WeightedProbabilityTableItem<TItem, TContext>, float> weightedItems = new Dictionary<WeightedProbabilityTableItem<TItem, TContext>, float>();
            float totalWeight = GetWeightedItems(context, filterCriteria, ref weightedItems);

            // If there are no items with weight > 0 then try resetting the table
            if (totalWeight <= 0f)
            {
                Reset();
                totalWeight = GetWeightedItems(context, filterCriteria, ref weightedItems);

                // If there are still no items with weight > 0 then return null
                if (totalWeight <= 0f)
                {
                    Debug.LogWarning("Failed to get item from table; totalWeight=" + totalWeight);
                    return null;
                }
            }

            // Select an item based on the random value r
            float r = GetRandomValue(ref randState) * totalWeight;

            foreach (KeyValuePair<WeightedProbabilityTableItem<TItem, TContext>, float> weightedItem in weightedItems)
            {
                r -= weightedItem.Value;

                if (r <= 0f)
                {
                    selectedItem = weightedItem.Key;

                    // Increment the selection counts
                    selectedItem.SelectionInfo.CycleSelectionCount++;
                    selectedItem.SelectionInfo.TotalSelectionCount++;

                    RecalculateWeight(selectedItem);

                    break;
                }
            }

            // Reset the table if all items have been selected the maximum number of times
            if (IsExhausted())
                Reset();

            if (selectedItem == null)
                Debug.LogWarning("Failed to get item from table; weightedItems.Count=" + weightedItems.Count + ", totalWeight=" + totalWeight);

            return selectedItem;
        }

        private float GetWeightedItems(TContext context, FilterCriteria filterCriteria, ref Dictionary<WeightedProbabilityTableItem<TItem, TContext>, float> weightedItems)
        {
            float totalWeight = 0f;

            // Get the effective weights and total weight for all items in the table
            foreach (WeightedProbabilityTableItem<TItem, TContext> item in _items)
            {
                // Filter out items that don't meet the criteria
                if (filterCriteria != null && !filterCriteria.Invoke(item.Item))
                    continue;

                // Filter out items with weight <= 0
                if (item.Weight.CurrentWeight <= 0f)
                    continue;

                float effectiveWeight = item.Weight.GetEffectiveWeight(context);

                // Filter out items with effective weight <= 0
                if (effectiveWeight <= 0f)
                    continue;

                weightedItems.Add(item, effectiveWeight);
                totalWeight += effectiveWeight;
            }

            return totalWeight;
        }

        private void ReplaceTableItem(WeightedProbabilityTableItem<TItem, TContext> replacedItem)
        {
            // Decrement the selection counts
            replacedItem.SelectionInfo.CycleSelectionCount--;
            replacedItem.SelectionInfo.TotalSelectionCount--;

            RecalculateWeight(replacedItem);
        }

        private bool IsExhausted()
        {
            bool exhausted = false;

            switch (_sampleMode)
            {
                case SampleMode.SampleWithReplacement:
                    // Nothing to do
                    break;
                case SampleMode.SampleWithoutReplacement:
                case SampleMode.SampleWithWeightDecay:
                    exhausted = _items.All(i => i.Weight.CurrentWeight <= 0f);
                    break;
            }

            return exhausted;
        }

        private int GetRemainingItemCount()
        {
            return _items.Where(i => i.Weight.CurrentWeight > 0f).Count();
        }

        private void Reset()
        {
            foreach (WeightedProbabilityTableItem<TItem, TContext> item in _items)
            {
                item.Weight.CurrentWeight = item.Weight.BaseWeight;
                item.SelectionInfo.CycleSelectionCount = 0;
            }
        }

        private float GetRandomValue(ref UnityEngine.Random.State randState)
        {
            UnityEngine.Random.State prevState = UnityEngine.Random.state;
            UnityEngine.Random.state = randState;

            float r = UnityEngine.Random.value;

            randState = UnityEngine.Random.state;
            UnityEngine.Random.state = prevState;

            return r;
        }

        private void SyncPeekRandState()
        {
            _peekRandState = _getRandState;
        }

        protected void RecalculateWeight(WeightedProbabilityTableItem<TItem, TContext> item)
        {
            switch (_sampleMode)
            {
                case SampleMode.SampleWithReplacement:
                    // Nothing to do
                    break;
                case SampleMode.SampleWithoutReplacement:
                    if (item.SelectionInfo.CycleSelectionCount > 0)
                        item.Weight.CurrentWeight = 0f;
                    else
                        item.Weight.CurrentWeight = item.Weight.BaseWeight;
                    break;
                case SampleMode.SampleWithWeightDecay:
                    if (item.SelectionInfo.LimitSelectionsTotal && item.SelectionInfo.TotalSelectionCount >= item.SelectionInfo.MaxSelectionsTotal)
                    {
                        // If we've reached the maximum number of total selections then set the weight to 0
                        item.Weight.CurrentWeight = 0f;
                    }
                    else if (item.SelectionInfo.LimitSelectionsPerCycle)
                    {
                        // Decay the weight based on the number of selections in the current cycle
                        int cycleSelectionsRemaining = item.SelectionInfo.MaxSelectionsPerCycle - item.SelectionInfo.CycleSelectionCount;
                        item.Weight.CurrentWeight = item.Weight.BaseWeight * ((float)cycleSelectionsRemaining / item.SelectionInfo.MaxSelectionsPerCycle);
                    }
                    else
                    {
                        // If the weight isn't affected by the number of selections total or per cycle, then just set it to the base weight
                        item.Weight.CurrentWeight = item.Weight.BaseWeight;
                    }
                    
                    break;
            }
        }
    }
}
