using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class TestCoroutine : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Tester());
        }

        IEnumerator Tester()
		{
			while (true)
			{
                Debug.Log("Tessting");
                yield return null;
			}
		}
    }
}
