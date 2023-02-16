// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

namespace EndlessRunnerEngine
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Managers/Game Level Data", order = 1)]
	public class GameLevel : MonoBehaviour
	{
		internal string levelName = "New Level";

		internal enum GameMode { Endless, Race }
		[SerializeField, Tooltip("What game mode is this level using?")]
		internal GameMode gameMode = GameMode.Endless;

		[SerializeField, Tooltip("If the level uses different objects to other levels, set this as true.")]
		internal bool levelUsesCustomTheme = false;

		[SerializeField, ConditionalField(nameof(levelUsesCustomTheme), false, true), Tooltip("The theme of the level itself.")]
		internal Theme levelTheme = null;

		[SerializeField, Range(1f, 3f), Tooltip("Difficulty of the level. This is a combination of both the speed of the player and obstacle positioning and scaling.")]
		internal float levelDifficulty = 2f;

		//public Audio
	}
}
