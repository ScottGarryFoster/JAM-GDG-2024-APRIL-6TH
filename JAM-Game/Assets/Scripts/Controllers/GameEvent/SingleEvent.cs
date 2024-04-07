using System;

namespace Controllers
{
    [Serializable]
    public struct SingleEvent
    {
        public string Description;

        public EventOption Left;
        public EventOption Right;

        /// <summary>
        /// True means this will remain in the pool no matter the difficulty.
        /// </summary>
        public bool RetainInPool;

        public bool OverrideTimesInPool;
        public int OverrideTimesInPoolAmount;
    }
}