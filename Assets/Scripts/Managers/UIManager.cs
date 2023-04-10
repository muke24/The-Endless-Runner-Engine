// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

namespace EndlessRunnerEngine
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager instance;

		private EndlessRunnerManager erm;

		[Range(0.5f, 2f)]
		public float uiScale = 1;

		public UITheme uiTheme;

		public Pages pages;

		public int currentPage = 0;

		//public StartupScreen startupUI;
		public MainMenu mainMenuUI;
		public GameStarting gameStartingUI;
		public InGame inGameUI;
		public Death deathUI;

		[Serializable]
		public class Pages
		{
			[Tooltip("Each page is the index number.")]
			public GameObject[] gamePages;

			[SerializeField]
			internal GameObject[,] gamePlatformPages;
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
			public Image[] titleImage = null;
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
			erm = EndlessRunnerManager.instance;
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
			if (erm == null)
			{
				erm = EndlessRunnerManager.instance;
			}

			int platform = (int)erm.version.platformType;

			

			#region Starting

			#endregion

			#region Game

			#endregion
		}

		public void ApplyMainMenuTheme()
		{
			int platform = (int)erm.version.platformType;
			mainMenuUI.copyrightText[platform].text = "Copyright © " + erm.settings.companyName + " 2023. All Rights Reserved.";
			mainMenuUI.clickToPlayText[platform].text = uiTheme.menuUI.clickToPlayText;
			
			ApplyTitle(mainMenuUI.titleText[platform], mainMenuUI.titleImage[platform]);
		}

		private void ApplyTitle(TextMeshProUGUI titleText, Image titleImg)
		{
			
			if (uiTheme.global.titleImage == null)
			{
				titleText.text = erm.settings.gameName;
			}
			else
			{
				titleText.text = "";
				titleImg.sprite = uiTheme.global.titleImage;
			}
		}

		public void ApplyGameStartTheme()
		{
			int platform = (int)erm.version.platformType;
			
			ApplyTitle(gameStartingUI.titleText[platform], gameStartingUI.titleImage[platform]);

		}

		private void Start()
		{
			SetPage(1);
			
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

			currentPage = pageIndex;
		}
	}
}
