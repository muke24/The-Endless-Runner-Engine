// Written by Peter Thompson - Playify.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using static UnityEngine.Diagnostics.Utils;

namespace EndlessRunnerEngine
{

	public class EndlessRunnerManager : MonoBehaviour
	{
		#region Singleton
		public static EndlessRunnerManager instance { get; private set; }

		[ExecuteInEditMode]
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

		#region Variables
		public static EndlessRunnerEngine.Player localPlayer;
		public static RowMovement localRow;
		public static Level currentLoadedLevel;
		public static bool gameStarted = false;

		[SerializeField]
		internal Version version;
		public Render render;
		public Game game;
		public Scoring scoring;
		public Connection connection;
		public Player player;
		public Environment environment;
		public Application application;
		public Scene scene;
		public Settings settings;

		[SerializeField]
		private ScriptOptions scriptOptions;
		[SerializeField]
		private Security security;
		#endregion

		#region Variable Classes
		[Serializable]
		internal class Version
		{
			internal enum GameVersion { Regular, Business, Educational, Rental }

			[Tooltip("For business use. Remove this when on asset store.")]
			internal GameVersion gameVersion;

			public enum PlatformScreens { Landscape, Portrait }
			[SerializeField]
			public PlatformScreens platformScreens;
			public int platformScreensCount
			{
				get
				{
					return Enum.GetValues(typeof(PlatformScreens)).Length;
				}
			}

			public enum PlatformType { Mobile = 0, PC = 1, Console = 2 }
			[SerializeField]
			public PlatformType platformType = PlatformType.Mobile;
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
		internal class Security
		{
			[SerializeField]
			private bool quitIfDeviceCannotBeRecognised = true;


		}

		[Serializable]
		public class Game
		{
			[SerializeField, Tooltip("The whole ui to the game. This can be used to create the entire look of the games user interface.")]
			internal UITheme uiTheme;

			public enum PlayType { Singleplayer, Multiplayer }

			[HideInInspector, Tooltip("Is the game currently singleplayer or multiplayer?")]
			public PlayType playType;
		}

		[Serializable]
		public class Connection
		{
			public bool ConnectedToInternet
			{
				get
				{
					if (UnityEngine.Application.internetReachability == NetworkReachability.NotReachable)
					{
						return false;
					}
					else
					{
						return true;
					}
				}

			}


			public enum GameType { Regular, Splitscreen }
			[HideInInspector, Tooltip("Is the game currently splitscreen?")]
			public GameType gameType;

			[Range(2, 4), Tooltip("The maximum amount of players that can use splitscreen.")]
			public int maxSplitscreenPlayers = 4;
			[SerializeField, Tooltip("The maximum amount of players that can connect to the game.")]
			internal int maxConnections = 4;
			[SerializeField, Tooltip("The maximum amount of connected players that can play the game (useful for having spectators).")]
			internal int maxPlayers = 4;

			[Tooltip("The amount of players currently connected to the same server you are connected to.")]
			internal int currentConnections = 0;
			[Tooltip("The amount of players currently playing same server you are connected to.")]
			internal int currentPlayers = 0;
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
		}

		[Serializable]
		public class Environment
		{
			[SerializeField, Tooltip("How many rows to spawn ahead of player. This is useful for 3D as you are able to see in front of the player, meaning more of the world will need to be visable.")]
			internal int rowsToSpawn = 3;

			[Tooltip("This is for editing the world surroundings. Eg, spawning trees in selected areas at a random spawn rate. Or spawning constants like side walls for a 2d game.")]
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

		[Serializable]
		public class Scene
		{
			[SerializeField]
			internal bool useSeparateScenes = true;
			[SerializeField, ConditionalField(nameof(useSeparateScenes))]
			internal SceneReference startupScene = null;
			[SerializeField, ConditionalField(nameof(useSeparateScenes))]
			internal SceneReference menuScene = null;
			[SerializeField, ConditionalField(nameof(useSeparateScenes))]
			internal SceneReference gameScene = null;

		}

		[Serializable]
		public class Settings
		{
			public readonly string companyName = "Playify"; 
			public readonly string gameName = "Paper Plane"; 
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

			DontDestroyOnLoad(gameObject);

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

			//version.platformScreensCount = Enum.GetValues(typeof(Version.PlatformScreens)).Length;
		}

		/// <summary>
		/// This is essentially the Start() method.
		/// </summary>
		private void LateInitialise()
		{
			// Debugging
			//StartEndlessGame(new GameObject());
		}

		/// <summary>
		/// This gets called every X seconds. It should be used for any checks that are constantly neccessary (such as if the game is connected to the internet or something) but dont need to be updated every frame.
		/// </summary>
		private void SlowUpdate()
		{

		}

		private void SetupUI()
		{
			UIManager.instance.ApplyUITheme();
		}

		/// <summary>
		/// Starts the game given the selected level prefab
		/// </summary>
		/// <param name="level"></param>
		public void StartEndlessGame(GameObject selectedLevel)
		{
			Debug.Log("Starting Game!");
			RetrieveLocalRow(selectedLevel);
		}

		void RetrieveLocalRow(GameObject selectedLevel)
		{
			GameObject spawnedLevel = Instantiate(selectedLevel);

			localRow = spawnedLevel.GetComponentInChildren<RowMovement>();

			// Crash application if cannot find RowMovement script
			if (localRow == null)
			{
				Debug.LogError("Unable to fetch the RowMovement script in the selected level prefab! Forcing crash...");
				//ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
				UnityEngine.Application.Quit();
			}
		}
		#endregion
	}
}
