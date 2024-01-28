using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable
{
    [Serializable]
    public class WeightedProbabilityTableItemWeight<TItem, TContext>
        where TItem : class, ICloneable
        where TContext : WeightedProbabilityTableItemSelectionContext
    {
        public float BaseWeight { get => _baseWeight; set => _baseWeight = value; }
        public float CurrentWeight { get => _currentWeight; set => _currentWeight = value; }
        public WeightedProbabilityTableItemWeightModifier<TItem, TContext>[] Modifiers { get => _modifiers.ToArray(); }

        [SerializeField]
        private TItem _item;
        [SerializeField]
        private float _baseWeight;
        [SerializeField]
        private float _currentWeight;
        [SerializeField]
        private List<WeightedProbabilityTableItemWeightModifier<TItem, TContext>> _modifiers;

        public WeightedProbabilityTableItemWeight(TItem item, float baseWeight = 0f, IEnumerable<WeightedProbabilityTableItemWeightModifier<TItem, TContext>> modifiers = null)
        {
            _item = item;
            _baseWeight = baseWeight;
            _currentWeight = baseWeight;
            _modifiers = modifiers == null ? new List<WeightedProbabilityTableItemWeightModifier<TItem, TContext>>() : modifiers.ToList();
        }

        public WeightedProbabilityTableItemWeight(WeightedProbabilityTableItemWeight<TItem, TContext> other)
        {
            _item = other._item.Clone() as TItem;
            _baseWeight = other._baseWeight;
            _currentWeight = other._currentWeight;
            _modifiers = other._modifiers.Select(m => m.Copy() as WeightedProbabilityTableItemWeightModifier<TItem, TContext>).ToList();
        }

        public void Add(WeightedProbabilityTableItemWeightModifier<TItem, TContext> modifier)
        {
            _modifiers.Add(modifier);
        }

        public void Add(IEnumerable<WeightedProbabilityTableItemWeightModifier<TItem, TContext>> modifiers)
        {
            _modifiers.AddRange(modifiers);
        }

        public void Remove(int index)
        {
            if (index >= 0 && _modifiers.Count > index)
                _modifiers.RemoveAt(index);
        }

        public void Clear()
        {
            _modifiers.Clear();
        }

        public float GetEffectiveWeight(TContext context)
        {
            float effectiveWeight = _currentWeight;

            foreach (WeightedProbabilityTableItemWeightModifier<TItem, TContext> weightModifier in _modifiers)
                effectiveWeight = weightModifier.Modify(_item, effectiveWeight, context);

            return effectiveWeight;
        }
    }
}
