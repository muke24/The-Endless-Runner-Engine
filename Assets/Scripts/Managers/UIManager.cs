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

		public Pages pages;

		[Space]
		public StartupScreen startupUI;
		public MainMenu menuUI;
		public GameStarting gameStartingUI;
		public InGame inGameUI;
		public Death deathUI;

		[Serializable]
		public class Pages
		{
			public int currentPage = 0;

			[Tooltip("Each page is the index number.")]
			public GameObject[] gamePages;
		}

		// Each class represents a "UI scene". The game may not utilise different scenes such as a 2D runner game.
		// So all UI will need to be referenced.
		[Serializable]
		public class StartupScreen
		{
			// Arrays used for different platforms
			public TextMeshProUGUI[] companyText;

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
			public TextMeshProUGUI[] toolTipText;
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

		void PlatformSetup()
		{
			for (int i = 0; i < pages.gamePages.Length; i++)
			{
				for (int x = 0; x < pages.gamePages[i].transform.childCount; x++)
				{
					if (true)
					{

					}
				}
			}
		}

		public void ApplyUITheme()
		{

		}

		private void Start()
		{
			SetPage(0);
		}

		public void SetPage(int pageIndex)
		{
			for (int i = 0; i < pages.gamePages.Length; i++)
			{
				pages.gamePages[i].SetActive(false);

				for (int x = 0; x < pages.gamePages[i].transform.childCount; x++)
				{
					pages.gamePages[i].transform.GetChild(x).gameObject.SetActive(false);
				}
			}

			pages.gamePages[pageIndex].SetActive(true);
			pages.gamePages[pageIndex].transform.GetChild((int)EndlessRunnerManager.instance.version.platformScreens).gameObject.SetActive(true);

			pages.currentPage = pageIndex;
		}
	}
}
