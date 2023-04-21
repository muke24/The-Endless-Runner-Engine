// Written by Peter Thompson - Playify.

using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EndlessRunnerEngine
{
	public class Level : MonoBehaviour
	{
		[Serializable]
		internal class Data
		{
			[SerializeField]
			internal bool rotateObjectsToScreenOrientation = true;

			internal enum GameMode { Endless, Race, Level }
			[SerializeField, Tooltip("What game mode is this level using?")]
			internal GameMode gameMode = GameMode.Endless;

			[SerializeField]
			internal int levelCount = 8;
			[SerializeField]
			internal float colourChangeSpeed = 5f;
			[SerializeField]
			internal int rowStartPoint = -2;
			[SerializeField]
			internal float sidewallWidthMultiplier = 3;
			[SerializeField]
			internal Vector2 rowSize;
			internal RowMovement rowParent;
		}

		[Serializable]
		internal class SpawnableObjects
		{
			[SerializeField]
			internal GameObject row;
			[SerializeField]
			internal GameObject sidewall;
			[SerializeField]
			internal GameObject background;
			[SerializeField]
			internal GameObject detail; // Window in this case
			[SerializeField]
			internal GameObject obstacle;
		}

		[Serializable]
		internal class Theme
		{
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

		internal class Renderers
		{
			internal List<SpriteRenderer> backgroundRenderers = new();
			internal List<SpriteRenderer> obstacleRenderers = new();
			internal List<SpriteRenderer> sidewallRenderers = new();
			internal List<SpriteRenderer> detailRenderers = new();

			internal void InitLists()
			{
				sidewallRenderers = new();
				backgroundRenderers = new();
				obstacleRenderers = new();
				detailRenderers = new();
			}
		}

		[Serializable]
		private class Debugging
		{
			[SerializeField]
			internal bool applyChangesAtRuntime = false;
		}

		[SerializeField]
		private Debugging debugging;
		[SerializeField]
		private Data levelData;
		[SerializeField]
		private Theme levelTheme;
		[SerializeField]
		private Renderers levelRenderers = new();
		[SerializeField]
		internal SpawnableObjects spawnableObjects;

		public int currentLevel { get; internal set; } = 0;

		//[SerializeField]
		internal List<Row> rows = new List<Row>();
		//[SerializeField]
		internal List<GameObject> sidewalls = new List<GameObject>();
		//[SerializeField]
		internal List<GameObject> backgrounds = new List<GameObject>();
		//[SerializeField]
		internal List<GameObject> details = new List<GameObject>();
		//[SerializeField]
		internal List<GameObject> obstacles = new List<GameObject>();

		private Light2D levelLighting;

		private void Start()
		{
			levelData.rowParent = GetComponentInChildren<RowMovement>();
			levelLighting = GetComponentInChildren<Light2D>();
			InitLists();
			LoadLevel();
			ApplyLevelColoursRaw();
		}

		void InitLists()
		{
			if (levelRenderers == null)
			{
				levelRenderers = new Renderers();
			}
			levelRenderers.InitLists();
		}

		private void Update()
		{
			if (debugging.applyChangesAtRuntime)
			{
				for (int i = 0; i < EndlessRunnerManager.instance.environment.rowsToSpawn; i++)
				{
					SpawnBackgrounds(rows[i], false);
					SpawnSidewalls(rows[i], false);
					RepositionAllRows();
				}
			}
		}

		/// <summary>
		/// Lerps the last level colour to the current level colour
		/// </summary>
		public void ApplyLevelColours()
		{
			StartCoroutine(LerpbackgroundColours());
			StartCoroutine(LerpSidewallColours());
			//StartCoroutine(LerpLevelObstacleColours());
			//StartCoroutine(LerpLevelDetailColours());
		}

		/// <summary>
		/// Sets the level colours to the current level without any lerping
		/// </summary>
		public void ApplyLevelColoursRaw()
		{
			SetBackgroundColour();
			SetSidewallColour();
		}

		void SetBackgroundColour()
		{
			int length = levelRenderers.backgroundRenderers.Count;

			if (length != 0)
			{
				for (int i = 0; i < length; i++)
				{
					levelRenderers.backgroundRenderers[i].color = levelTheme.backgroundColours[currentLevel];
				}
			}
			else
			{
				Debug.LogError("Unable to set level background colours! Please check if the levelBackgroundColors count is the same as the amount of levels (found in EndlessRunnerManager)");
			}
		}

		void SetSidewallColour()
		{
			int length = levelRenderers.sidewallRenderers.Count;

			if (length != 0)
			{
				for (int i = 0; i < length; i++)
				{
					levelRenderers.sidewallRenderers[i].color = levelTheme.sidewallColours[currentLevel];
				}
			}
			else
			{
				Debug.LogError("Unable to set level background colours! Please check if the levelBackgroundColors count is the same as the amount of levels (found in EndlessRunnerManager)");
			}
		}

		IEnumerator LerpbackgroundColours()
		{
			while (true)
			{
				//Debug.Log("This works");
				int length = levelRenderers.backgroundRenderers.Count;

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

		IEnumerator LerpSidewallColours()
		{
			while (true)
			{
				//Debug.Log("This works");
				int length = levelRenderers.sidewallRenderers.Count;

				if (length != 0)
				{
					if (levelRenderers.sidewallRenderers[length - 1].color != levelTheme.sidewallColours[currentLevel])
					{
						for (int i = 0; i < length; i++)
						{
							levelRenderers.sidewallRenderers[i].color = Color.Lerp(levelRenderers.sidewallRenderers[i].color, levelTheme.sidewallColours[currentLevel], levelData.colourChangeSpeed * Time.smoothDeltaTime);
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
				int length = levelRenderers.obstacleRenderers.Count;

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
				int length = levelRenderers.detailRenderers.Count;

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
			if (levelData.gameMode != Data.GameMode.Level)
			{
				if (levelData.rowParent.transform.childCount > 0)
				{
					for (int i = 0; i < levelData.rowParent.transform.childCount; i++)
					{
						Debug.Log("Some rows already exist. Deleting them now and spawning new rows.");
						Destroy(levelData.rowParent.transform.GetChild(i).gameObject);
					}
				}

				rows = new List<Row>();

				for (int i = 0; i < EndlessRunnerManager.instance.environment.rowsToSpawn; i++)
				{
					SpawnRow();
				}
			}
			else
			{
				Debug.Log("Level gamemode has been selected. Retrieving all of the obstacles in the level prefab.");
				RetrieveLevel();
			}
		}

		/// <summary>
		/// Gets references to all of the obstacles for a pre-made level
		/// </summary>
		void RetrieveLevel()
		{

		}

		void SpawnRow()
		{
			Row row = Instantiate(spawnableObjects.row.GetComponent<Row>(), levelData.rowParent.transform);

			rows.Add(row);

			SpawnBackgrounds(row, true);
			SpawnSidewalls(row, true);
			RepositionAllRows();

			SpawnBackgrounds(row, false);
			SpawnSidewalls(row, false);
			RepositionAllRows();
		}

		void SpawnSidewalls(Row row, bool start)
		{
			for (int i = 0; i < 2; i++)
			{
				// Left
				if (i == 0)
				{
					GameObject sidewall;

					// If this is called at start
					if (start)
					{
						sidewall = Instantiate(spawnableObjects.sidewall, row.leftSidewallParent);

						sidewalls.Add(sidewall);
					}
					else
					{
						sidewall = row.leftSidewallParent.GetChild(0).gameObject;
					}

					sidewall.transform.localPosition = -RepositionSidewalls(sidewall)[0];
					sidewall.transform.rotation = Quaternion.Euler(RepositionSidewalls(sidewall)[1]);

					var rend = sidewall.GetComponent<SpriteRenderer>();
					levelRenderers.sidewallRenderers.Add(rend);
					// Apply background size
					var erm = EndlessRunnerManager.instance;
					if (erm.render.direction == EndlessRunnerManager.Render.GameDirection.Down || erm.render.direction == EndlessRunnerManager.Render.GameDirection.Up)
					{
						rend.size = levelData.rowSize / 10 * new Vector2(levelData.sidewallWidthMultiplier, 1);
					}
					else
					{
						rend.size = levelData.rowSize / 10 * new Vector2(1, levelData.sidewallWidthMultiplier);
					}
					rend.sortingOrder = 0;
				}
				// Right
				else if (i == 1)
				{
					GameObject sidewall;

					// If this is called at start
					if (start)
					{
						sidewall = Instantiate(spawnableObjects.sidewall, row.rightSidewallParent);

						sidewalls.Add(sidewall);
					}
					else
					{
						sidewall = row.rightSidewallParent.GetChild(0).gameObject;
					}

					sidewall.transform.localPosition = RepositionSidewalls(sidewall)[0];
					sidewall.transform.rotation = Quaternion.Euler(RepositionSidewalls(sidewall)[1]);

					var rend = sidewall.GetComponent<SpriteRenderer>();
					levelRenderers.sidewallRenderers.Add(rend);
					// Apply background size
					var erm = EndlessRunnerManager.instance;
					if (erm.render.direction == EndlessRunnerManager.Render.GameDirection.Down || erm.render.direction == EndlessRunnerManager.Render.GameDirection.Up)
					{
						rend.size = levelData.rowSize / 10 * new Vector2(levelData.sidewallWidthMultiplier, 1);
					}
					else
					{
						rend.size = levelData.rowSize / 10 * new Vector2(1, levelData.sidewallWidthMultiplier);
					}
					rend.sortingOrder = 0;
				}
			}
		}

		Vector2[] RepositionSidewalls(GameObject sidewall)
		{
			var erm = EndlessRunnerManager.instance;
			Vector3 position = Vector3.zero;
			Vector3 rotation = Vector3.zero;

			// Portrait positioning
			if (erm.render.direction == EndlessRunnerManager.Render.GameDirection.Down || erm.render.direction == EndlessRunnerManager.Render.GameDirection.Up)
			{
				float sidewallSize = (sidewall.GetComponent<SpriteRenderer>().size.x * 5);
				position = new Vector3((levelData.rowSize.x / 2) + (sidewallSize), 0, 0);
				//Debug.Log("Calculated sidewall positions (Portrait)");
				return new Vector2[] { position, rotation };

			}
			// Landscape positioning
			else
			{
				float sidewallSize = (sidewall.GetComponent<SpriteRenderer>().size.y * 5);
				position = new Vector3(0, (levelData.rowSize.y / 2) + (sidewallSize), 0);

				if (levelData.rotateObjectsToScreenOrientation)
				{
					//rotation = new Vector3(0, 0, 90);
				}

				//Debug.Log("Calculated sidewall positions (Landscape)");

				return new Vector2[] { position, rotation };
			}

			//Debug.LogError("Failed to reposition sidewalls!");
			//return new Vector2[] { position, rotation };
		}

		void SpawnBackgrounds(Row row, bool start)
		{
			GameObject background;
			if (start)
			{
				background = Instantiate(spawnableObjects.background, row.backgroundParent);
				backgrounds.Add(background);

			}
			else
			{
				background = row.backgroundParent.GetChild(0).gameObject;
			}

			var rend = background.GetComponent<SpriteRenderer>();
			if (levelRenderers.backgroundRenderers == null)
			{
				levelRenderers.backgroundRenderers = new List<SpriteRenderer>();
			}
			levelRenderers.backgroundRenderers.Add(rend);
			// Apply background size
			rend.size = levelData.rowSize / 10;
			rend.sortingOrder = 1;
		}

		void RepositionAllRows()
		{
			// Add splitscreen code here

			// Add screen orientation code here
			var erm = EndlessRunnerManager.instance;
			if (erm.render.direction == EndlessRunnerManager.Render.GameDirection.Down || erm.render.direction == EndlessRunnerManager.Render.GameDirection.Up)
			{
				for (int i = 0; i < rows.Count; i++)
				{
					rows[i].transform.position = new Vector3(0, levelData.rowSize.y * (-i - levelData.rowStartPoint), 0);
				}
			}
			else
			{
				for (int i = 0; i < rows.Count; i++)
				{
					rows[i].transform.position = new Vector3(levelData.rowSize.x * (-i - levelData.rowStartPoint), 0, 0);
				}
			}
		}
	}
}
