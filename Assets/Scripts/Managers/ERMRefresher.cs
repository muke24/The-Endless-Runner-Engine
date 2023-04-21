using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class ERMRefresher : MonoBehaviour
    {
        [SerializeField]
        private GameObject ermPrefab;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnERMonNextFrame());
        }

        IEnumerator SpawnERMonNextFrame()
		{
            bool firstFramePassed = false;
			while (true)
			{
				if (!firstFramePassed)
				{
                    firstFramePassed = true;
                    yield return null;
				}
                else
				{
                    Instantiate(ermPrefab);
                    Destroy(gameObject);
                    yield break;
                }
			}
		}
    }
}
