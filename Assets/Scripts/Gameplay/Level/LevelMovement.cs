// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EndlessRunnerEngine
{
	public class LevelMovement : MonoBehaviour
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
					Destroy(internalValues.rowParent.GetChild(i));
				}
			}
			#endregion

			#region Spawn new rows
			internalValues.spawnedRows = new Row[customisation.amountOfRowsToSpawn];

			for (int i = 0; i < customisation.amountOfRowsToSpawn; i++)
			{
				if (customisation.amountOfRowsToSpawn > customisation.rowsThatCanSpawn.Length)
				{

				}
				else
				{
					internalValues.spawnedRows[i] = Instantiate(customisation.rowsThatCanSpawn[UnityEngine.Random.Range(0, customisation.rowsThatCanSpawn.Length)]);

				}
			}
			#endregion
		}

		private void Update()
		{
			
		}

		void RowMovement()
		{

		}

	}
}
