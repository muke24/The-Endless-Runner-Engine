using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
	[CreateAssetMenu(fileName = "New Level Theme", menuName = "Level Theme")]
	public class LevelTheme : ScriptableObject
	{
		public bool levelIsPreMade = false;

		[Header("Only applicable to 2D games")]
		public Sprite[] obstacleSprites;
		public Sprite[] backgroundDetails;
		[Tooltip("Only use this if pre-making level rather than generating the level.")]
		public Vector3[] obstaclePositions;

		public int levelCount = 8;

		public Color[] levelBackgroundColours;
		public Color[] obstacleColors;
		public Color[] detailColors;

	}
}
