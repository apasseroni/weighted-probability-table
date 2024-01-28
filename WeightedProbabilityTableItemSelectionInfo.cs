using UnityEngine;

namespace GLHFStudios.Utility.Generic.WeightedProbabilityTable
{
    [System.Serializable]
    public class WeightedProbabilityTableItemSelectionInfo
    {
        public int CycleSelectionCount { get => _cycleSelectionCount; set => _cycleSelectionCount = value; }
        public int TotalSelectionCount { get => _totalSelectionCount; set => _totalSelectionCount = value; }
        public bool LimitSelectionsPerCycle => _limitSelectionsPerCycle;
        public bool LimitSelectionsTotal => _limitSelectionsTotal;
        public int MaxSelectionsPerCycle => _maxSelectionsPerCycle;
        public int MaxSelectionsTotal => _maxSelectionsTotal;

        [SerializeField]
        private int _cycleSelectionCount;
        [SerializeField]
        private int _totalSelectionCount;
        [SerializeField]
        private bool _limitSelectionsPerCycle;
        [SerializeField]
        private bool _limitSelectionsTotal;
        [SerializeField]
        private int _maxSelectionsPerCycle;
        [SerializeField]
        private int _maxSelectionsTotal;

        public WeightedProbabilityTableItemSelectionInfo(bool limitSelectionsPerCycle = false, int maxSelectionsPerCycle = 0, bool limitSelectionsTotal = false, int maxSelectionsTotal = 0)
        {
            _limitSelectionsPerCycle = limitSelectionsPerCycle;
            _maxSelectionsPerCycle = maxSelectionsPerCycle;
            _limitSelectionsTotal = limitSelectionsTotal;
            _maxSelectionsTotal = maxSelectionsTotal;

            _cycleSelectionCount = 0;
            _totalSelectionCount = 0;
        }

        public WeightedProbabilityTableItemSelectionInfo(WeightedProbabilityTableItemSelectionInfo other)
        {
            _limitSelectionsPerCycle = other._limitSelectionsPerCycle;
            _maxSelectionsPerCycle = other._maxSelectionsPerCycle;
            _limitSelectionsTotal = other._limitSelectionsTotal;
            _maxSelectionsTotal = other._maxSelectionsTotal;

            _cycleSelectionCount = other._cycleSelectionCount;
            _totalSelectionCount = other._totalSelectionCount;
        }
    }
}
