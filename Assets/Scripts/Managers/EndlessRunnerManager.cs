// Written by Peter Thompson - Playify.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

namespace EndlessRunnerEngine
{
	public class EndlessRunnerManager : MonoBehaviour
	{
		#region Singleton
		public static EndlessRunnerManager instance { get; private set; }
		public static EndlessRunnerManager editorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<EndlessRunnerManager>();
					if (instance == null)
					{
						GameObject go = new GameObject("EndlessRunnerManager");
						instance = go.AddComponent<EndlessRunnerManager>();
					}
				}
				return instance;
			}
		}
		#endregion

		public GameVisuals gameVisuals;
		public Scoring scoring;
		public Security security;
		public Multiplayer multiplayer;
		public Player player;


		[Serializable]
		public class GameVisuals
		{
			internal enum RenderType { TwoDimentional, ThreeDimentional }

			[SerializeField, Tooltip("Whether the game should be a 2D game, or a 3D game.")]
			internal RenderType renderType = RenderType.TwoDimentional;

			internal enum GameDirection2D { Left, Right, Up, Down }
			[ConditionalField(nameof(renderType), false, RenderType.TwoDimentional), SerializeField, Tooltip("If 2D has been selected, this will affect which direction the player will move in.")]
			internal GameDirection2D direction2D = GameDirection2D.Down;

			internal enum GameDirection3D { TopDown, AngledTopDown, ThirdPerson, FirstPerson }
			[ConditionalField(nameof(renderType), false, RenderType.ThreeDimentional), SerializeField, Tooltip("If 3D is selected, this will affect the player's camera positioning.")]
			internal GameDirection3D direction3D = GameDirection3D.ThirdPerson;

		}

		[Serializable]
		public class Scoring
		{
			[HideInInspector]
			public int score { get; internal set; }
			[HideInInspector]
			public int collectableScore { get; internal set; }

			public bool gameUsesCollectables = true;
		}

		[Serializable]
		public class Security
		{
			[SerializeField]
			private bool quitIfDeviceCannotBeRecognised = true;


		}

		[Serializable]
		public class Game
		{
			public enum PlayType { Singleplayer, Multiplayer }

			[HideInInspector, Tooltip("Is the game currently singleplayer or multiplayer?")]
			public PlayType playType;

			public enum GameMode { Endless, Race }
			[HideInInspector, Tooltip("What is the game's current gamemode? eg. Endless, Race ect.")]
			public GameMode gameMode;
		}

		[Serializable]
		public class Multiplayer
		{
			public enum GameType { Regular, Splitscreen }
			[HideInInspector, Tooltip("Is the game currently splitscreen?")]
			public GameType gameType;

			[Range(2, 4), Tooltip("The maximum amount of players that can use splitscreen.")]
			public int maxSplitscreenPlayers = 4;
			[Tooltip("The maximum amount of players that can connect to the game.")]
			public int maxConnections = 4;

			[SerializeField, Tooltip("The amount of players currently connected to the same server you are connected to.")]
			internal int currentConnections = 0;

			public int currentPlayers = 0;
		}

		[Serializable]
		public class Player
		{
			[Tooltip("This is the player prefab. Don't get this confused with different player skins, as the skin is apart of the Player script.")]
			public EndlessRunnerEngine.Player playerPrefab;

			[HideInInspector, Tooltip("Retrieves the local player. This must be used in both singleplayer and multiplayer play types.")]
			public EndlessRunnerEngine.Player localPlayer;

			// Multiplayer (this must be called from the server only and recieved by the clients from the server)			[ConditionalField(nameof(spawnPlayerDuringStartCountdown)), SerializeField]
			[SerializeField, Tooltip("Whether the player will spawn during the game start countdown, or after the countdown finishes.")]
			internal bool spawnPlayerDuringStartCountdown = true;

			// Multiplayer (this must be called from the server only and recieved by the clients from the server)			[ConditionalField(nameof(spawnPlayerDuringStartCountdown)), SerializeField]
			[ConditionalField(nameof(spawnPlayerDuringStartCountdown)), SerializeField]
			internal int secondsToSpawnPlayerDuringCountdown = 2;

			[SerializeField, Tooltip("How responsive the player will feel. This is useful for plane games where the player can't instantly move left and right.")]
			internal float floatiness;

			[SerializeField, Tooltip("Speed that the player will move side to side.")]
			internal float sideSpeed;

			[SerializeField, Tooltip("Speed that the player will move forwards.")]
			internal float forwardSpeed;

		}

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(this);
			}
			else
			{
				instance = this;
			}

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

		}
	}

}
