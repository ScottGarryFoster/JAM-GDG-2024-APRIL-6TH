using System;
using Unity.VisualScripting;

namespace Controllers
{
    [Serializable]
    public struct EventOption
    {
        public string ButtonDescription;
        
        public TypeOfEvent TypeOfEvent;
        public DifficultyOfEvent Difficulty;
        public PlayerProjectStat DifficultyStat;
        public int ValueToBeat;

        public ProjectTask WorkTask;
        public PlayTask PlayTask;

        public string SuccessText;
        public string FailureText;
    }
}