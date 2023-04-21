// Written by Peter Thompson - Playify.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Rendering;
using System.IO;
using UnityEngine.SceneManagement;

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
		public static RowMovement localBackgroundMovement;
		public static Level currentLoadedLevel;
		public static bool gameStarted = false;

		[SerializeField]
		internal Version version;

		public Render render;
		public Graphics graphics;
		public Game game;
		public Scoring scoring;
		public Connection connection;
		public Player player;
		public Environment environment;
		public Application application;
		public Scene scene;
		public Settings settings;

		[SerializeField]
		internal ScriptOptions scriptOptions;
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

			public enum PlatformScreens { Landscape = 1, Portrait = 0 }
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

			internal enum GameDirection { Left, Right, Up, Down, Behind }
			//[ConditionalField(nameof(renderType), false, RenderType.TwoDimentional), SerializeField, Tooltip("If 2D has been selected, this will affect which direction the player will move in.")]
			internal GameDirection direction = GameDirection.Down;

			internal enum CameraType { Orthographic, Perspective }
			[Tooltip("How the camera will render the game (Orthographic or Perspective).")]
			internal CameraType cameraType;
		}

		[Serializable]
		public class Graphics
		{
			[SerializeField, Tooltip("If checked, the ERM graphics settings below will be ignored.")]
			internal bool useCustomSettings = false;

			[Tooltip("The first asset in the array will represent the lowest graphical settings," +
				" the higher the asset is in the array the higher the quality level should be. The current render pipeline asset will be swapped with the selected one here.")]
			public GraphicsQualityAsset[] graphicsQualityAssets;
			[Tooltip("Adding an asset here will override any previously saved settings. Otherwise it will act as the loaded quality asset.")]
			public GraphicsQualityAsset currentQuality;

			[Serializable]
			public class GraphicsQualityAsset
			{
				public string qualityName;
				public RenderPipelineAsset graphicsQualityAsset;
			}
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

			internal Level currentLevel = null;

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
			[SerializeField, Tooltip("How often SlowUpdate() will be called.")]
			internal float slowUpdateTime = 3f;

			[SerializeField, Range(0, 1)]
			internal float semiSmoothedDeltaTime = 1f;
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

			internal enum MainMenuRenderType { TwoDimentional, ThreeDimentional }

			[SerializeField]
			internal MainMenuRenderType mainMenuRenderType = MainMenuRenderType.ThreeDimentional;

			[Space]
			[SerializeField, ConditionalField(nameof(useSeparateScenes))]
			internal SceneReference menuScene = null;
			[SerializeField, ConditionalField(nameof(useSeparateScenes))]
			internal SceneReference gameScene = null;

			[SerializeField, ConditionalField(nameof(useSeparateScenes), true)]
			internal GameObject[] gameObjectsToRemoveIfSameScene;
		}

		[Serializable]
		public class Settings
		{
			[SerializeField]
			internal GameObject ERM_Prefab;
			//public readonly string companyName = "Playify";
			//public readonly string gameName = "Paper Plane";
		}
		#endregion

		#region Coroutines

		private IEnumerator SlowUpdateCaller()
		{
			bool firstUpdate = true;
			while (true)
			{
				if (firstUpdate)
				{
					firstUpdate = false;
					yield return new WaitForSeconds(scriptOptions.slowUpdateTime);
				}
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
			CheckScreenType();
			//version.platformScreensCount = Enum.GetValues(typeof(Version.PlatformScreens)).Length;
		}

		/// <summary>
		/// This is essentially the Start() method.
		/// </summary>
		private void LateInitialise()
		{
			// Debugging
			//StartGame(new GameObject());
			LoadGameQuality();
			SetupUI();
		}

		/// <summary>
		/// This gets called every X seconds. It should be used for any checks that are constantly neccessary (such as if the game is connected to the internet or something) but dont need to be updated every frame.
		/// </summary>
		private void SlowUpdate()
		{
			//RefreshERM();
		}

		public float SemiSmoothedDeltaTime
		{
			get
			{
				return Time.deltaTime - (Time.deltaTime * scriptOptions.semiSmoothedDeltaTime) + (Time.smoothDeltaTime - (Time.smoothDeltaTime * (1 - scriptOptions.semiSmoothedDeltaTime)));
			}
		}

		/// <summary>
		/// This should be called when a game finishes. This will clear up any garbage collection processes.
		/// </summary>
		void RefreshERM()
		{
			var newInstance = settings.ERM_Prefab;
			
			Instantiate(newInstance);
			Destroy(gameObject);

		}

		void CheckScreenType()
		{
			// If landscape
			if (Screen.currentResolution.width > Screen.currentResolution.height)
			{
				version.platformScreens = Version.PlatformScreens.Landscape;
			}
			// If portraite
			else
			{
				version.platformScreens = Version.PlatformScreens.Portrait;
			}
		}

		void SetGameQuality(Graphics.GraphicsQualityAsset asset)
		{
			QualitySettings.renderPipeline = asset.graphicsQualityAsset;
			graphics.currentQuality = asset;
		}

		void LoadGameQuality()
		{
			if (graphics.currentQuality != null)
			{
				if (graphics.currentQuality.graphicsQualityAsset == null)
				{
					string saveFile = UnityEngine.Application.dataPath + "/GraphicsSettings.json";

					// Does it exist?
					if (File.Exists(saveFile))
					{
						Debug.Log("Quality file found. Trying to read data from it...");
						var jsonString = File.ReadAllText(saveFile);
						var deserialisedFile = JsonUtility.FromJson<Graphics.GraphicsQualityAsset>(jsonString);
						// File exists!
						if (deserialisedFile.graphicsQualityAsset != null)
						{
							SetGameQuality(deserialisedFile);
							Debug.Log("Game quality loaded successfully.");
						}
						else
						{
							CheckIfQualitySettingsExist();
							Debug.Log("Failed to load quality file. Setting defaults instead...");

						}
					}
					else
					{
						CheckIfQualitySettingsExist();
					}

					SaveGameQuality();
				}
				else
				{
					SetGameQuality(graphics.currentQuality);
				}
			}
			else
			{
				graphics.currentQuality = new Graphics.GraphicsQualityAsset();

				string saveFile = UnityEngine.Application.dataPath + "/GraphicsSettings.json";

				// Does it exist?
				if (File.Exists(saveFile))
				{
					Debug.Log("Quality file found. Trying to read data from it...");
					var jsonString = File.ReadAllText(saveFile);
					var deserialisedFile = JsonUtility.FromJson<Graphics.GraphicsQualityAsset>(jsonString);
					// File exists!
					if (deserialisedFile.graphicsQualityAsset != null)
					{
						SetGameQuality(deserialisedFile);
						Debug.Log("Game quality loaded successfully.");
					}
					else
					{
						CheckIfQualitySettingsExist();
						Debug.Log("Failed to load quality file. Setting defaults instead...");

					}
				}
				else
				{
					CheckIfQualitySettingsExist();
				}

				SaveGameQuality();
			}
		}

		void CheckIfQualitySettingsExist()
		{
			if (graphics.graphicsQualityAssets != null || graphics.graphicsQualityAssets.Length > 0)
			{
				Debug.LogWarning("Failed to load the last graphics quality asset. Setting the quality to " + graphics.graphicsQualityAssets[(int)graphics.graphicsQualityAssets.Length / 2].qualityName);
				SetGameQuality(graphics.graphicsQualityAssets[(int)graphics.graphicsQualityAssets.Length / 2]);
			}
			else
			{
				Debug.LogWarning("Failed to load the last quality asset and no render pipeline assets have been added to the graphics section in this script. Using Unity's current settings instead.");

				SetCustomQualitySettings();
			}
		}

		void SaveGameQuality()
		{
			if (!graphics.useCustomSettings)
			{
				if (graphics.currentQuality == null)
				{
					// Create an error msg handler for viewing errors in the game.
					Debug.Log("Unable to save current quality settings. Saving defaults instead.");

					CheckIfQualitySettingsExist();
				}

				string qual = JsonUtility.ToJson(graphics.currentQuality);
				File.WriteAllText(UnityEngine.Application.dataPath + "/GraphicsSettings.json", qual);

				QualitySettings.renderPipeline = graphics.currentQuality.graphicsQualityAsset;
			}
			else
			{
				SetCustomQualitySettings();
			}
		}

		void SetCustomQualitySettings()
		{
			graphics.currentQuality = new Graphics.GraphicsQualityAsset();
			graphics.currentQuality.qualityName = "Custom";

			if (QualitySettings.renderPipeline != null)
			{
				graphics.currentQuality.graphicsQualityAsset = QualitySettings.renderPipeline;
			}
			else
			{
				Debug.Log("This may be an error, or the current render pipeline is set to standard. Will need to test. Shouldn't affect anything however.");
			}
		}

		private void SetupUI()
		{
			UIManager.instance.ApplyUITheme();
		}

		/// <summary>
		/// Starts the game given the selected level prefab
		/// </summary>
		/// <param name="level"></param>
		public void StartGame(Level selectedLevel)
		{
			Debug.Log("Starting Game!");
			RetrievelocalBackgroundMovement(selectedLevel);
		}

		IEnumerator LoadGame(Level selectedLevel)
		{
			// The Application loads the Scene in the background as the current Scene runs.
			// This is particularly good for creating loading screens.
			// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
			// a sceneBuildIndex of 1 as shown in Build Settings.

			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.gameScene.SceneName);
			asyncLoad.allowSceneActivation = true;

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone)
			{
				yield return null;
			}

			while (asyncLoad.isDone)
			{
				SpawnLevel(selectedLevel);
				// Do whatever
				yield break;
			}
		}

		void RetrievelocalBackgroundMovement(Level selectedLevel)
		{
			if (scene.useSeparateScenes)
			{
				// Load game scene asynchronously (empty scene prefab)
				StartCoroutine(LoadGame(selectedLevel));



				// Play scene animation
			}
			else
			{
				// Play animation
				SpawnLevel(selectedLevel);
			}
		}

		void SpawnLevel(Level selectedLevel)
		{
			Debug.Log("Spawning the selected level.");
			GameObject spawnedLevel = Instantiate(selectedLevel.gameObject, Vector3.zero, Quaternion.identity);

			localBackgroundMovement = spawnedLevel.GetComponentInChildren<RowMovement>();

			// Crash application if cannot find RowMovement script
			if (localBackgroundMovement == null)
			{
				Debug.LogError("Unable to fetch the RowMovement script in the selected level prefab! Forcing crash...");
				//ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
				UnityEngine.Application.Quit();
			}
			if (!scene.useSeparateScenes)
			{
				for (int i = 0; i < scene.gameObjectsToRemoveIfSameScene.Length; i++)
				{
					Destroy(scene.gameObjectsToRemoveIfSameScene[i]);
				}
			}
		}

		#endregion
	}
}
