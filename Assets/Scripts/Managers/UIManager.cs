// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace EndlessRunnerEngine
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager instance;

		public StartupScreen startupUI;
		public MainMenu menuUI;
		public GameStarting gameStartingUI;
		public InGame inGameUI;
		public Death deathUI;

		// Each class represents a "UI scene". The game may not utilise different scenes such as a 2D runner game.
		// So all UI will need to be referenced.
		[Serializable]
		public class StartupScreen
		{
			// Arrays used for different platforms
			public TextMeshProUGUI[] titleText;

		}

		[Serializable]
		public class MainMenu
		{
			// Arrays used for different platforms
			public TextMeshProUGUI[] titleText;
			public Image[] titleImage = null;
			public TextMeshProUGUI[] clickToPlayText;
			public TextMeshProUGUI[] copyrightText;
		}

		[Serializable]
		public class GameStarting
		{
			// Arrays used for different platforms
			public TextMeshProUGUI[] countdownTimerText;
			public TextMeshProUGUI[] titleText;
			public TextMeshProUGUI[] tooltipText;
		}

		[Serializable]
		public class InGame
		{
			public TextMeshProUGUI[] scoreText;
			public TextMeshProUGUI[] scoreRefText;
			public TextMeshProUGUI[] collectableText;
			public TextMeshProUGUI[] collectableRefText;
			public Image[] collectableRefImg;
		}

		[Serializable]
		public class Death
		{
			public TextMeshProUGUI[] scoreText;
			public TextMeshProUGUI[] scoreRefText;
			public TextMeshProUGUI[] collectableText;
			public TextMeshProUGUI[] collectableRefText;
			public Image[] collectableRefImg;
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
		}

		public void ApplyUITheme()
		{

		}
	}
}
