using System;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable
{
    [Serializable]
    public class WeightedProbabilityTableItem<TItem, TContext>
        where TItem : class, ICloneable
        where TContext : WeightedProbabilityTableItemSelectionContext
    {
        public TItem Item { get => _item; }
        public WeightedProbabilityTableItemWeight<TItem, TContext> Weight { get => _weight; }
        public WeightedProbabilityTableItemSelectionInfo SelectionInfo { get => _selectionInfo; }

        [SerializeField]
        private TItem _item;
        [SerializeField]
        private WeightedProbabilityTableItemWeight<TItem, TContext> _weight;
        [SerializeField]
        private WeightedProbabilityTableItemSelectionInfo _selectionInfo;

        public WeightedProbabilityTableItem(TItem item, WeightedProbabilityTableItemWeight<TItem, TContext> weight,
            WeightedProbabilityTableItemSelectionInfo selectionInfo)
        {
            _item = item;
            _weight = weight;
            _selectionInfo = selectionInfo;
        }

        public WeightedProbabilityTableItem(WeightedProbabilityTableItem<TItem, TContext> other)
        {
            _item = other._item.Clone() as TItem;
            _weight = new WeightedProbabilityTableItemWeight<TItem, TContext>(other._weight);
            _selectionInfo = new WeightedProbabilityTableItemSelectionInfo(other._selectionInfo);
        }

        public WeightedProbabilityTableItem<TItem, TContext> Copy()
        {
            return new WeightedProbabilityTableItem<TItem, TContext>(this);
        }
    }
}
