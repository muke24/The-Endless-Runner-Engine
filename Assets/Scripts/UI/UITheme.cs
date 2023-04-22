// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace EndlessRunnerEngine
{
	[CreateAssetMenu(fileName = "New UI Theme", menuName = "UI Theme")]
	public class UITheme : ScriptableObject
	{
		public Global global;
		public MainMenu menuUI;
		public GameStarting gameStartingUI;
		public InGame inGameUI;
		public Death deathUI;
		public Shop shopUI;
		public Settings settingsUI;
		public Multiplayer multiplayerUI;
		public Leaderboard leaderboardUI;
		public Personalisation personalisationUI;

		// Each class represents a "UI scene". The game may not utilise different scenes such as a 2D runner game.
		// So all UI will need to be referenced.
		[Serializable]
		public class Global
		{
			[Tooltip("If image is not null, image will be used instead of title text.")]

			public Sprite titleImages = null;
			public Sprite collectableRefImg = null;

			public ERMText titleTexts;
			public ERMText copyrightTexts;
		}

		[Serializable]
		public class MainMenu
		{
			public Sprite settingsButtonImg = null;
			public Sprite personalisationButtonImg = null;

			public ERMText clickToPlayText;
		}

		[Serializable]
		public class GameStarting
		{
			// Arrays used for different platforms
			public ERMText tooltipText; // Endless \n Fly as far as you can!
		}

		[Serializable]
		public class InGame
		{
			public ERMText scoreRefText;// = "Score";
			public ERMText collectableRefText;// = "Coins";

			public ERMText scoreText;
			public ERMText collectableText;
		}

		[Serializable]
		public class Death
		{
			public ERMText youDiedText;

			public ERMText scoreRefText;// = "Your score was: ";
			public ERMText collectableRefText;// = "Coins collected: ";

			public ERMText scoreText;
			public ERMText collectableText;
		}

		[Serializable]
		public class Shop
		{
			public ERMText subtitleText;

		}

		[Serializable]
		public class Settings
		{
			public ERMText subtitleText;

		}

		[Serializable]
		public class Multiplayer
		{
			public ERMText subtitleText;

		}

		[Serializable]
		public class Leaderboard
		{
			public ERMText subtitleText;
		}

		[Serializable]
		public class Personalisation
		{
			public ERMText subtitleText;
		}

		[Serializable]
		public class ERMText
		{
			public string text;
			public int fontSize;
			public TMP_FontAsset font;

			public bool active = true;
		}

		[Serializable]
		public class ERMTextAnimated
		{
			public string text;
			public int fontMinSize;
			public int fontMaxSize;
			public TMP_FontAsset font;
			public bool active = true;
			[Range(0, 2)]
			public float animationSpeed = 1;
		}
	}
}
