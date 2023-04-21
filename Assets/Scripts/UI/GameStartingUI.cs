using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EndlessRunnerEngine
{
	public class GameStartingUI : MonoBehaviour
	{
		[SerializeField]
		private Transform textPivot;

		[SerializeField]
		private TextMeshProUGUI[] countdownTexts;

		private Quaternion startRot;

		[SerializeField]
		private AnimationCurve rotationSpeedCurve;

		[SerializeField]
		private float rotationGeneralSpeed;

		private void Awake()
		{
			startRot = textPivot.rotation;
		}

		private void Start()
		{
			StartCountdownAnim();
		}

		private void StartCountdownAnim()
		{
			StartCoroutine(CountdownAnim());
		}

		IEnumerator CountdownAnim()
		{
			float angleMoved = 0f;
			int countdownCount = 3;

			Quaternion startRot = textPivot.rotation;

			while (true)
			{
				float scaledValue = (angleMoved - 0) / (90 - 0);
				//Debug.Log("Scaled value is: " + scaledValue);
				float rotationSpeed = (rotationSpeedCurve.Evaluate(scaledValue) * rotationGeneralSpeed) * Time.deltaTime;
				angleMoved += rotationSpeed;

				if (angleMoved >= 90)
				{
					angleMoved = 0;
					countdownCount--;
					//Debug.Log("Countdown should be at: " + countdownCount);
				}

				textPivot.Rotate(rotationSpeed, 0, 0);

				if (countdownCount == 3)
				{
					countdownTexts[3].gameObject.SetActive(true);
					countdownTexts[2].gameObject.SetActive(false);
					countdownTexts[1].gameObject.SetActive(false);
					countdownTexts[0].gameObject.SetActive(false);
				}
				else if (countdownCount == 2)
				{
					countdownTexts[3].gameObject.SetActive(true);
					countdownTexts[2].gameObject.SetActive(true);
					countdownTexts[1].gameObject.SetActive(false);
					countdownTexts[0].gameObject.SetActive(false);
				}
				else if (countdownCount == 1)
				{
					countdownTexts[3].gameObject.SetActive(false);
					countdownTexts[2].gameObject.SetActive(true);
					countdownTexts[1].gameObject.SetActive(true);
					countdownTexts[0].gameObject.SetActive(false);
				}
				else // Go!
				{
					countdownTexts[3].gameObject.SetActive(false);
					countdownTexts[2].gameObject.SetActive(false);
					countdownTexts[1].gameObject.SetActive(true);
					countdownTexts[0].gameObject.SetActive(true);
				}

				if (countdownCount == -1)
				{
					Debug.Log("Countdown finished");

					textPivot.rotation = startRot;
					UIManager.instance.SetPage(5);
					yield break;
				}
				else
				{
					yield return null;
				}
			}
		}

		private void OnEnable()
		{
			SetTexts();
		}

		void SetTexts()
		{
			textPivot.rotation = startRot;

			for (int i = 0; i < countdownTexts.Length; i++)
			{
				if (i != 3)
				{
					countdownTexts[i].gameObject.SetActive(false);
				}
			}
		}
	}
}
