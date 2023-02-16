// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class Collectable : MonoBehaviour
    {
        public enum CollectableType { Power, Coin }
        public CollectableType type = CollectableType.Coin;


    }
}
