﻿using UnityEngine;

namespace Controllers
{
    public static class IHateStatics
    {
        public static float GetProgressBarValue(float given, float max)
        {
            return given / max;
        }
    }
}