using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Controllers
{
    public static class IHateStatics
    {
        public static float GetProgressBarValue(float given, float max)
        {
            return given / max;
        }
        
        public static List<T> Shuffle<T>(this List<T> list, System.Random rnd)
        {
            for(var i=list.Count; i > 0; i--)
                list.Swap(0, rnd.Next(0, i));

            return list;
        }

        public static void Swap<T>(this List<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}