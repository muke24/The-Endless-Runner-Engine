// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class CustomSpawner : MonoBehaviour
    {
		[SerializeField, Tooltip("Random range of which this object can move.")]
        internal Vector3 spawnPositionOffset;
        [SerializeField, Tooltip("Random range of which this object can rotate.")]
        internal Vector3 spawnRotationOffset;

        internal void Generate()
		{
            float posX = Random.Range(-spawnPositionOffset.x, spawnPositionOffset.x);
            float posY = Random.Range(-spawnPositionOffset.y, spawnPositionOffset.y);
            float posZ = Random.Range(-spawnPositionOffset.z, spawnPositionOffset.z);

            transform.localPosition = new Vector3(posX, posY, posZ);

            float x = Random.Range(-spawnRotationOffset.x, spawnRotationOffset.x);
            float y = Random.Range(-spawnRotationOffset.y, spawnRotationOffset.y);
            float z = Random.Range(-spawnRotationOffset.z, spawnRotationOffset.z);

            Vector3 rot = new Vector3(x, y, z);

            transform.localRotation = Quaternion.Euler(rot);
		}
    }
}
