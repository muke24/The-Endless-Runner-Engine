// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EndlessRunnerEngine
{
	public class RowMovement : MonoBehaviour
	{
		[SerializeField]
		internal InternalValues internalValues;
		[SerializeField]
		internal Customisation customisation;

		[Serializable]
		internal class InternalValues
		{
			internal float verticalPosition;
			internal float horizontalPosition;

			internal Row lastSpawnedRow;
			[SerializeField]
			internal Transform rowParent;

			internal Row[] spawnedRows;

			public Row[] backgroundRows;

			public SpriteRenderer[] backgroundSprites;
			public SpriteRenderer[] SideWallSprites;
						
			public float distanceLoggerSpeed = 0f;

			[HideInInspector, Tooltip("Checks if this is the original background. Useful for resetting the game so it doesnt destroy this object.")]
			public bool isFirstBackground = false; // Not functional yet

			internal float currentBackgroundSpeed = 0f;

			internal Vector3[] startPositions;

			//private ObstacleGeneration[] obstacleParents;
		}

		[Serializable]
		public class Customisation
		{
			public Row[] rowsThatCanSpawn;
			public int amountOfRowsToSpawn = 5;
		}

		private void Awake()
		{
			Initialise();
		}

		private void Start()
		{
			LateInitialise();
		}

		private void Initialise()
		{
			
		}

		private void LateInitialise()
		{
			#region Destroy any rows that are already in scene
			if (internalValues.rowParent.childCount > 0)
			{
				for (int i = 0; i < internalValues.rowParent.childCount; i++)
				{
					Destroy(internalValues.rowParent.GetChild(i).gameObject);
				}
			}
			#endregion

			#region Spawn new rows
			internalValues.spawnedRows = new Row[customisation.amountOfRowsToSpawn];

			for (int i = 0; i < customisation.amountOfRowsToSpawn; i++)
			{
				//internalValues.spawnedRows[i] = Instantiate(customisation.rowsThatCanSpawn[UnityEngine.Random.Range(0, customisation.rowsThatCanSpawn.Length)]);

				if (customisation.amountOfRowsToSpawn > customisation.rowsThatCanSpawn.Length)
				{

				}
				else if (customisation.amountOfRowsToSpawn < customisation.rowsThatCanSpawn.Length)
				{
					// Set this so it spawns the rest in another for loop
					if (i == customisation.amountOfRowsToSpawn - 1) // If last obstacle in obstacles to spawn is being spawned but more obstacles are to spawn
					{
						for (int x = 0; x < (customisation.rowsThatCanSpawn.Length - customisation.amountOfRowsToSpawn); x++)
						{

						}
					}
				}
				else
				{
					internalValues.spawnedRows[i] = Instantiate(customisation.rowsThatCanSpawn[i]);
				}
			}
			#endregion
		}

		private void Update()
		{
			//MoveRows();
			// Row movement is done via the player script
		}

		void MoveRows()
		{
			if (EndlessRunnerManager.gameStarted)
			{
				for (int i = 0; i < internalValues.spawnedRows.Length; i++)
				{

				}
			}
		}
	}
}
