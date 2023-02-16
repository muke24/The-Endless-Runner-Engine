// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class CustomSpawner : MonoBehaviour
    {
		[SerializeField, Tooltip("Select a prefab to spawn in.")]
        internal GameObject objectToSpawnIn;

		[SerializeField]
        internal Vector3 objectSpawnPosition;
    }
}
