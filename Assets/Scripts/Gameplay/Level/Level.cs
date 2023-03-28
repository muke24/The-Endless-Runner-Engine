// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EndlessRunnerEngine
{
    public class Level : ScriptableObject
    {
        public LevelData levelData;
        public LevelTheme levelTheme;
        public enum Mode { Endless, Custom }

        public float difficulty = 1f;

        [SerializeField]
        internal float obstacleSpacing = 10f;

        internal bool levelUsesCollectables = true;
    }
}
