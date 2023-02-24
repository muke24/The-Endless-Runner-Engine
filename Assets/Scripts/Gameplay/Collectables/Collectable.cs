// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

namespace EndlessRunnerEngine
{
    public class Collectable : MonoBehaviour
    {
        public enum CollectableType { PowerUp, Coin }
        public CollectableType type = CollectableType.Coin;

        public enum SpawnType { Singular, Path }
        public SpawnType spawnType = SpawnType.Singular;

		[ConditionalField(nameof(spawnType), false, SpawnType.Path)]
        public int pathLength = 10;
        [ConditionalField(nameof(type), false, CollectableType.Coin)]
        public int collectableWorth = 1;
    }
}
