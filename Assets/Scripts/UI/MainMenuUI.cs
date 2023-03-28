using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EndlessRunnerEngine
{
    public class MainMenuUI : MonoBehaviour
	{
		#region Fade
		[SerializeField]
        private Image fadePanel;

		[SerializeField, Space, Range(0.1f, 5f)]
		private float fadeSpeed = 1f;
		#endregion

		#region Text Animation
		[SerializeField, Space]
		private TextMeshProUGUI tapToStartText;

		[SerializeField, Range(0.1f, 5f)]
		private float textFadeSpeed = 1f;
		[SerializeField, Range(0f, 1f)]
		private float textStayMaxOpacityTime = 0.25f;
		#endregion

		[SerializeField, Space]
		private Button settingsButton;

		[SerializeField]
		private Button personalisationButton;

		[SerializeField]
		private Button startGameButton;

		private void Start()
		{
			StartFadeAnimation();
			StartTextAnimation();
			SetupButtons();
		}

		void SetupButtons()
		{
			settingsButton.onClick.AddListener(SettingsButtonPressed);
			personalisationButton.onClick.AddListener(PersonalisationButtonPressed);
			startGameButton.onClick.AddListener(StartGameButtonPressed);
		}

		void StartGameButtonPressed()
		{
			Debug.Log("Start game has been tapped!");
		}

		void SettingsButtonPressed()
		{
			Debug.Log("Settings button pressed.");
			// CHANGE: Play animation here.
			UIManager.instance.SetPage(2);
		}

		void PersonalisationButtonPressed()
		{
			Debug.Log("Personalisation button pressed.");
			// CHANGE: Play animation here.
			UIManager.instance.SetPage(3);
		}

		void StartFadeAnimation()
		{
			fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1);
			StartCoroutine(AnimateFade());
		}

		IEnumerator AnimateFade()
		{
			while (true)
			{
				if (fadePanel.color.a > 0)
				{
					Color col = fadePanel.color;

					col.a -= fadeSpeed * Time.deltaTime;

					fadePanel.color = col;
					yield return null;
				}
				else
				{
					fadePanel.gameObject.SetActive(false);
					yield break;
				}
			}
		}

		void StartTextAnimation()
		{
			StartCoroutine(TextAnimate());
		}

		IEnumerator TextAnimate()
		{
			Color textCol = tapToStartText.color;
			bool direction = false;
			float maxOpacTimer = textStayMaxOpacityTime;
			while (true)
			{
				if (direction)
				{
					if (textCol.a >= 1)
					{
						if (maxOpacTimer > 0f)
						{
							maxOpacTimer -= Time.deltaTime;
							yield return null;
						}
						else
						{
							direction = false;
							maxOpacTimer = textStayMaxOpacityTime;
							yield return null;
						}
					}
					else
					{
						textCol.a += (textFadeSpeed * Time.deltaTime);
						yield return null;
					}
				}
				else
				{
					if (textCol.a <= 0)
					{
						if (maxOpacTimer > 0f)
						{
							maxOpacTimer -= Time.deltaTime;
							yield return null;
						}
						else
						{
							direction = true;
							maxOpacTimer = textStayMaxOpacityTime;
							yield return null;
						}
					}
					else
					{
						textCol.a -= (textFadeSpeed * Time.deltaTime);
						yield return null;
					}
				}

				tapToStartText.color = textCol;

				yield return null;
			}
		}
	}
}
