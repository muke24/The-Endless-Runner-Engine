using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EndlessRunnerEngine
{
    public class StartupScreenAnimations : MonoBehaviour
    {
		[SerializeField, Range(0.1f, 5f)]
		private float delayTime = 1f;
		[SerializeField, Range(0.1f, 5f)]
		private float fadeInSpeed = 1.5f;
		[SerializeField, Range(0.1f, 5f)]
		private float timeToStay = 1f;
		[SerializeField, Range(0.1f, 5f)]
		private float fadeOutSpeed = 1;
		[SerializeField, Range(0.1f, 5f)]
		private float timeToChangeScene;

		[SerializeField]
        private bool useImage;

        [SerializeField]
        private TextMeshProUGUI[] companyTexts;
        [SerializeField]
        private Image[] companyImages;

		private void Start()
		{
			StartAnimation();
		}



		void StartAnimation()
		{
			StartCoroutine(AnimationDelay());
		}

		IEnumerator AnimationDelay()
		{
			float timer = delayTime;
			while (true)
			{
				if (timer > 0)
				{
					timer -= Time.deltaTime;
					yield return null;
				}
				else
				{
					StartCoroutine(CompanyAnimation());
					yield break;
				}
			}
		}

		IEnumerator CompanyAnimation()
		{
			//Color companyColour;
			TextMeshProUGUI companyTxt = null;
			Image companyImg = null;

			if (!useImage)
			{
				companyTxt = companyTexts[(int)EndlessRunnerManager.instance.version.platformScreens];
				companyTxt.color = new Color(companyTxt.color.r, companyTxt.color.g, companyTxt.color.b, 0);
				//companyColour = companyTxt.color;
			}
			else
			{
				companyImg = companyImages[(int)EndlessRunnerManager.instance.version.platformScreens];
				companyImg.color = new Color(companyImg.color.r, companyImg.color.g, companyImg.color.b, 0);
				//companyColour = companyImg.color;
			}

			float timer = timeToStay;

			// False = fade in, True = fade out
			bool direction = false;

			while (true)
			{
				if (!useImage)
				{
					if (!direction)
					{
						if (companyTxt.color.a < 1)
						{
							companyTxt.color = new Color(companyTxt.color.r, companyTxt.color.g, companyTxt.color.b, companyTxt.color.a + (fadeInSpeed * Time.deltaTime));
							//Debug.Log("Should see this multiple times");
							yield return null;
						}
						else
						{
							companyTxt.color = new Color(companyTxt.color.r, companyTxt.color.g, companyTxt.color.b, 1);
							direction = true;
							yield return null;
							
						}
						//Debug.Log("Direction is: " + direction + ". Coroutine is reaching end when it shouldnt be.");

						yield return null;
					}
					else
					{
						if (timer > 0)
						{
							timer -= Time.deltaTime;
							yield return null;
						}
						else
						{
							if (companyTxt.color.a > 0)
							{
								companyTxt.color = new Color(companyTxt.color.r, companyTxt.color.g, companyTxt.color.b, companyTxt.color.a - (fadeOutSpeed * Time.deltaTime));
								yield return null;
							}
							else
							{
								companyTxt.color = new Color(companyTxt.color.r, companyTxt.color.g, companyTxt.color.b, 0);
								//Debug.Log("Breaking text coroutine");

								StartCoroutine(PageChange());

								yield break;
							}
						}
						//Debug.Log("Direction is: " + direction + ". Coroutine is reaching end when it shouldnt be.");
						yield return null;
					}
				}
				else
				{
					if (!direction)
					{
						if (companyImg.color.a < 1)
						{
							companyImg.color = new Color(companyImg.color.r, companyImg.color.g, companyImg.color.b, companyImg.color.a + (fadeInSpeed * Time.deltaTime));
							yield return null;
						}
						else
						{
							direction = true;
							yield return null;
						}
					}
					else
					{
						if (timer > 0)
						{
							timer -= Time.deltaTime;
							yield return null;
						}
						else
						{
							if (companyImg.color.a > 0)
							{
								companyImg.color = new Color(companyImg.color.r, companyImg.color.g, companyImg.color.b, companyImg.color.a - (fadeOutSpeed * Time.deltaTime));
								yield return null;
							}
							else
							{
								companyImg.color = new Color(companyImg.color.r, companyImg.color.g, companyImg.color.b, 0);

								//Debug.Log("Breaking Image coroutine.");

								StartCoroutine(PageChange());

								yield break;
							}
						}
					}
				}
			}
		}

		IEnumerator PageChange()
		{
			float timer = timeToChangeScene;

			while (true)
			{
				if (timer > 0)
				{
					timer -= Time.deltaTime;
					yield return null;
				}
				else
				{
					UIManager.instance.SetPage(1);
					yield break;
				}
			}
		}
	}
}
