// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace EndlessRunnerEngine
{
    [CreateAssetMenu(fileName = "NewTheme", menuName = "Create Endless Runner Theme")]
    public class UITheme : ScriptableObject
    {
        public enum ThemeType { ThreeDimentional, TwoDimentional }
        public ThemeType themeType = ThemeType.ThreeDimentional;

		public Global global;
		public MainMenu menuUI;
		public GameStarting gameStartingUI;
		public InGame inGameUI;
		public Death deathUI;

		// Each class represents a "UI scene". The game may not utilise different scenes such as a 2D runner game.
		// So all UI will need to be referenced.
		[Serializable]
		public class Global
		{
			// Arrays used for different platforms
			[Tooltip("If image is not null, image will be used instead.")]
			public string titleText = "Game";
			public Sprite titleImage = null;
			public Sprite collectableRefImg = null;


		}

		[Serializable]
		public class MainMenu
		{
			// Arrays used for different platforms
			public Sprite settingsButtonImg = null;
			public string clickToPlayText = "Tap to play!";
			public string copyrightText = "Copyright COMPANY 2023©";
		}

		[Serializable]
		public class GameStarting
		{
			// Arrays used for different platforms
			public string tooltipText = "Endless \n Fly as far as you can!";
		}

		[Serializable]
		public class InGame
		{
			public string scoreRefText = "Score";
			public string collectableRefText = "Coins";
		}

		[Serializable]
		public class Death
		{
			public string scoreRefText = "Your score was: ";
			public string collectableRefText = "Coins collected: ";
		}

		[Serializable]
		public class Shop
		{

		}

		[Serializable]
		public class Settings
		{

		}

		[Serializable]
		public class Multiplayer
		{

		}

		[Serializable]
		public class Leaderboard
		{

		}
	}
}
