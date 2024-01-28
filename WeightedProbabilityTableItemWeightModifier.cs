using System;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable
{
    [Serializable]
    public abstract class WeightedProbabilityTableItemWeightModifier<TItem, TContext>
        where TItem : class, ICloneable
        where TContext : WeightedProbabilityTableItemSelectionContext
    {
        public abstract float Modify(TItem item, float weight, TContext context);

        public abstract WeightedProbabilityTableItemWeightModifier<TItem, TContext> Copy();
    }
}
