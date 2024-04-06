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
        /// True means take out of the pool once used.
        /// </summary>
        public bool OnlyUseOnce;
    }
}