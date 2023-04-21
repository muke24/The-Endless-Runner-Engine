using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
	public class ShopAdScaler : MonoBehaviour
	{
		[SerializeField]
		private float minScale;
		[SerializeField]
		private float maxScale;

		[SerializeField]
		private float scaleSpeed = 1f;

		private void OnEnable()
		{
			StartCoroutine(Scale());
		}

		private void OnDisable()
		{
			StopCoroutine(Scale());
		}

		IEnumerator Scale()
		{
			bool curDir = false;
			while (true)
			{
				if (curDir)
				{
					if (transform.localScale.x < maxScale)
					{
						Vector3 scale = transform.localScale;
						float scaleSpd = scaleSpeed * Time.deltaTime;
						scale += new Vector3(scaleSpd, scaleSpd, scaleSpd);
						transform.localScale = scale;
						//Debug.Log("Scaling up...");

						yield return null;

					}
					else
					{
						curDir = false;
						//Debug.Log("Switching scale direction to " + curDir);
						yield return null;

					}
				}
				else
				{
					if (transform.localScale.x > minScale)
					{
						Vector3 scale = transform.localScale;
						float scaleSpd = scaleSpeed * Time.deltaTime;
						scale -= new Vector3(scaleSpd, scaleSpd, scaleSpd);
						transform.localScale = scale;
						//Debug.Log("Scaling down...");

						yield return null;

					}
					else
					{
						curDir = true;
						//Debug.Log("Switching scale direction to " + curDir);
						yield return null;

					}
				}
				yield return null;
			}
		}
    }
}
