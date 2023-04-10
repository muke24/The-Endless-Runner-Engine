// Written by Peter Thompson - Playify.

using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using System.Collections;

namespace EndlessRunnerEngine
{
	public class Level : MonoBehaviour
	{
		[Serializable]
		private class Data
		{
			internal enum GameMode { Endless, Race, Level }
			[SerializeField, Tooltip("What game mode is this level using?")]
			internal GameMode gameMode = GameMode.Endless;

			public int levelCount = 8;

		}

		[Serializable]
		private class Theme
		{
			#region Sprites
			public Sprite[] obstacleSprites;
			public Sprite[] backgroundDetails;
			#endregion

			#region Colours
			public Color[] levelBackgroundColours;
			public Color[] levelObstacleColors;
			public Color[] levelDetailColors;
			public Color[] levelLightingColors;
			public float[] levelLightingIntensity;
			#endregion

			public SpriteRenderer[] levelBackgrounds;
			public SpriteRenderer[] levelObstacles;
			public SpriteRenderer[] levelDetails;

			public float levelChangeSpeed = 5f;
		}

		[SerializeField]
		private Data levelData;
		[SerializeField]
		private Theme levelTheme;

		public int currentLevel = 0;

		private Light2D levelLighting;

		private void Start()
		{
			levelLighting = GetComponentInChildren<Light2D>();
		}

		void ApplyLevelColours()
		{
			StartCoroutine(LerpLevelBackgroundColours());
			StartCoroutine(LerpLevelObstacleColours());
			StartCoroutine(LerpLevelDetailColours());
		}

		IEnumerator LerpLevelBackgroundColours()
		{
			while (true)
			{
				int length = levelTheme.levelBackgrounds.Length;

				if (length != 0)
				{
					if (levelTheme.levelBackgrounds[length - 1].color != levelTheme.levelBackgroundColours[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelTheme.levelBackgrounds[i].color = Color.Lerp(levelTheme.levelBackgrounds[i].color, levelTheme.levelBackgroundColours[currentLevel], levelTheme.levelChangeSpeed * Time.smoothDeltaTime);
						}
						yield return null;
					}
					else
					{
						yield break;
					}
				}
				else
				{
					Debug.LogError("Unable to set level background colours! Please check if the levelBackgroundColors count is the same as the amount of levels (found in EndlessRunnerManager)");
					yield break;
				}				
			}
		}

		IEnumerator LerpLevelObstacleColours()
		{
			while (true)
			{
				int length = levelTheme.levelObstacles.Length;

				if (length != 0)
				{
					if (levelTheme.levelObstacles[length - 1].color != levelTheme.levelObstacleColors[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelTheme.levelObstacles[i].color = Color.Lerp(levelTheme.levelObstacles[i].color, levelTheme.levelObstacleColors[currentLevel], levelTheme.levelChangeSpeed * Time.smoothDeltaTime);
						}
						yield return null;
					}
					else
					{
						yield break;
					}
				}
				else
				{
					Debug.LogError("Unable to set level obstacle colours! Please check if the levelObstacleColors count is the same as the amount of levels (found in EndlessRunnerManager)");
					yield break;
				}
			}
		}

		IEnumerator LerpLevelDetailColours()
		{
			while (true)
			{
				int length = levelTheme.levelDetails.Length;

				if (length != 0)
				{
					if (levelTheme.levelDetails[length - 1].color != levelTheme.levelObstacleColors[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelTheme.levelDetails[i].color = Color.Lerp(levelTheme.levelDetails[i].color, levelTheme.levelDetailColors[currentLevel], levelTheme.levelChangeSpeed * Time.smoothDeltaTime);
						}
						yield return null;
					}
					else
					{
						yield break;
					}
				}
				else
				{
					Debug.LogError("Unable to set level detail colours! Please check if the levelDetailColors count is the same as the amount of levels (found in EndlessRunnerManager)");
					yield break;
				}
			}
		}
	}
}
