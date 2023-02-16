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

		public Render render;
		public Game game;
		public Scoring scoring;
		public Security security;
		public Multiplayer multiplayer;
		public Player player;
		public Environment environment;
		[SerializeField]
		private ScriptOptions scriptOptions;
		public Application application;


		#region Variable Classes
		[Serializable]
		internal class Version
		{
			internal enum GameVersion { Regular, Business, Educational, Rental }

			[SerializeField, Tooltip("For business use. Remove this when on asset store.")]
			internal GameVersion gameVersion;
		}

		[Serializable]
		public class Render
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

			internal enum CameraType { Orthographic, Perspective }
			[Tooltip("How the camera will render the game (Orthographic or Perspective).")]
			internal CameraType cameraType;

			public enum Quality { Low, Medium, High }
			[Tooltip("Graphics quality level.")]
			public Quality quality;


		}

		[Serializable]
		public class Scoring
		{
			[HideInInspector]
			public int score { get; private set; }
			[HideInInspector]
			public int collectableScore { get; private set; }

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
			[SerializeField, Tooltip("The whole look to the game. This can be used to create the entire look of the game.")]
			internal Theme gameTheme;

			public enum PlayType { Singleplayer, Multiplayer }

			[HideInInspector, Tooltip("Is the game currently singleplayer or multiplayer?")]
			public PlayType playType;
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
			internal float floatiness = 1;

			[SerializeField, Tooltip("Speed that the player will move side to side.")]
			internal float sideSpeed = 1;

			[SerializeField, Tooltip("Speed that the player will move forwards.")]
			internal float forwardSpeed = 1;

			[SerializeField, Tooltip("General speed of the player. This affects both side speed and forwards speed concurrently.")]
			internal float generalSpeed = 1;

		}

		[Serializable]
		public class Environment
		{
			[SerializeField, Tooltip("How many rows to spawn ahead of player. This is useful for 3D as you are able to see in front of the player, meaning more of the world will need to be visable.")]
			internal int rowsToSpawn = 3;

			[SerializeField, Tooltip("This is for editing the world surroundings. Eg, spawning trees in selected areas at a random spawn rate. Or spawning constants like side walls for a 2d game.")]
			internal CustomSpawner[] customSpawners;
		}

		[Serializable]
		public class ScriptOptions
		{
			[Tooltip("How often SlowUpdate() will be called.")]
			public float slowUpdateTime = 3f;
		}

		[Serializable]
		public class Application
		{
			public bool useVsync = false;
			public int targetFps = 0;
		}
		#endregion

		#region Coroutines

		private IEnumerator SlowUpdateCaller()
		{
			while (true)
			{
				SlowUpdate();
				yield return new WaitForSeconds(scriptOptions.slowUpdateTime);
			}
		}

		#endregion

		#region Methods
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

		/// <summary>
		/// This is essentially the Awake() method.
		/// </summary>
		private void Initialise()
		{
			StartCoroutine(SlowUpdateCaller());
		}

		/// <summary>
		/// This is essentially the Start() method.
		/// </summary>
		private void LateInitialise()
		{

		}

		/// <summary>
		/// This gets called every X seconds. It should be used for any checks that are constantly neccessary but dont need to be updated every frame.
		/// </summary>
		private void SlowUpdate()
		{

		}


		#endregion
	}

}
