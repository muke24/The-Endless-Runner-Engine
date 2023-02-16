// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class Level : ScriptableObject
    {
        public enum Difficulty { Easy, Normal, Hard }
		[Tooltip("This is the difficulty level of this game level.")]
        public Difficulty difficulty = Difficulty.Normal;

        internal float obstacleSpacing = 10f;

        internal bool levelUsesCollectables = true;

        internal Collectable[] collectables;
    }
}
