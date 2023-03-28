// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

namespace EndlessRunnerEngine
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Managers/Game Level Data", order = 1)]
	public class GameLevel : ScriptableObject
	{
		internal string levelName = "New Level";

		[SerializeField, Range(1f, 3f), Tooltip("Difficulty of the level. This is a combination of both the speed of the player and obstacle positioning and scaling.")]
		internal float levelDifficulty = 2f;


		public LevelTheme levelTheme;
	}
}
