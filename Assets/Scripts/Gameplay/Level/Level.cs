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
		internal class Data
		{
			internal enum GameMode { Endless, Race, Level }
			[SerializeField, Tooltip("What game mode is this level using?")]
			internal GameMode gameMode = GameMode.Endless;

			public int levelCount = 8;
			public float colourChangeSpeed = 5f;

			[SerializeField]
			internal GameObject row;

			[SerializeField]
			internal Transform rowParent;
		}

		[Serializable]
		internal class Theme
		{
			#region Sprites
			[Header("Sprites")]
			public Sprite[] obstacleSprites;
			public Sprite[] backgroundSprites;
			public Sprite[] sidewallSprites;
			public Sprite[] detailSprites;
			#endregion

			#region Colours
			[Header("Colours")]
			public Color[] backgroundColours;
			public Color[] obstacleColours;
			public Color[] sidewallColours;
			public Color[] detailColours;
			#endregion

			#region Lighting
			[Header("Lighting")]
			public Color[] lightingColors;
			public float[] lightingIntensity;
			#endregion
		}

		[Serializable]
		internal class Renderers
		{
			public SpriteRenderer[] backgroundRenderers;
			public SpriteRenderer[] obstacleRenderers;
			public SpriteRenderer[] sidewallRenderers;
			public SpriteRenderer[] detailRenderers;
		}

		[SerializeField]
		private Data levelData;
		[SerializeField]
		private Theme levelTheme;
		[SerializeField]
		private Renderers levelRenderers;

		internal int currentLevel = 0;

		private Light2D levelLighting;

		private void Start()
		{
			levelLighting = GetComponentInChildren<Light2D>();
		}

		public void ApplyLevelColours()
		{
			StartCoroutine(LerpbackgroundColours());
			StartCoroutine(LerpLevelObstacleColours());
			StartCoroutine(LerpLevelDetailColours());
		}

		IEnumerator LerpbackgroundColours()
		{
			while (true)
			{
				int length = levelRenderers.backgroundRenderers.Length;

				if (length != 0)
				{
					if (levelRenderers.backgroundRenderers[length - 1].color != levelTheme.backgroundColours[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelRenderers.backgroundRenderers[i].color = Color.Lerp(levelRenderers.backgroundRenderers[i].color, levelTheme.backgroundColours[currentLevel], levelData.colourChangeSpeed * Time.smoothDeltaTime);
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
				int length = levelRenderers.obstacleRenderers.Length;

				if (length != 0)
				{
					if (levelRenderers.obstacleRenderers[length - 1].color != levelTheme.obstacleColours[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelRenderers.obstacleRenderers[i].color = Color.Lerp(levelRenderers.obstacleRenderers[i].color, levelTheme.obstacleColours[currentLevel], levelData.colourChangeSpeed * Time.smoothDeltaTime);
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
					Debug.LogError("Unable to set level obstacle colours! Please check if the obstacleColours count is the same as the amount of levels (found in EndlessRunnerManager)");
					yield break;
				}
			}
		}

		IEnumerator LerpLevelDetailColours()
		{
			while (true)
			{
				int length = levelRenderers.detailRenderers.Length;

				if (length != 0)
				{
					if (levelRenderers.detailRenderers[length - 1].color != levelTheme.obstacleColours[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelRenderers.detailRenderers[i].color = Color.Lerp(levelRenderers.detailRenderers[i].color, levelTheme.detailColours[currentLevel], levelData.colourChangeSpeed * Time.smoothDeltaTime);
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
					Debug.LogError("Unable to set level detail colours! Please check if the detailColours count is the same as the amount of levels (found in EndlessRunnerManager)");
					yield break;
				}
			}
		}

		public void LoadLevel()
		{

		}
	}
}
