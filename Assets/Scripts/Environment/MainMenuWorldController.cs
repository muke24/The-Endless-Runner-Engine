using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace EndlessRunnerEngine
{
	public class MainMenuWorldController : MonoBehaviour
	{
		[SerializeField]
		private Animator cameraAnimator;
		[SerializeField]
		private bool orthographic = true;
		[SerializeField]
		private bool testMode;
		[SerializeField]
		private Animator mainAnimator;

		[Space, SerializeField]
		private SceneChangeAnimations animations = new();
		[SerializeField]
		private ButtonReferences buttonsReferences = new();

		[Serializable]
		internal class SceneChangeAnimations
		{
			[SerializeField]
			internal Animation gameStart;
			[SerializeField]
			internal Animation gameEnd;

			[SerializeField]
			internal Animation shopStart;
			[SerializeField]
			internal Animation shopEnd;

			[SerializeField]
			internal Animation multiplayerLobbyStart;
			[SerializeField]
			internal Animation multiplayerLobbyEnd;

			[SerializeField]
			internal Animation settingsStart;
			[SerializeField]
			internal Animation settingsEnd;
			// Do all anims for each "scene". (multiplayer, shop, friends, ect...)
		}

		[Serializable]
		internal class ButtonReferences
		{
			[SerializeField, Tooltip("Supports both TMP and regular buttons")]
			internal Button[] gameButton;
			[SerializeField, Tooltip("Supports both TMP and regular buttons")]
			internal Button[] shopButton;
			[SerializeField, Tooltip("Supports both TMP and regular buttons")]
			internal Button[] multiplayerLobbyButton;
			[SerializeField, Tooltip("Supports both TMP and regular buttons")]
			internal Button[] settingsButton;
		}

		private void Start()
		{
			StartButtonListeners();
		}

		void StartButtonListeners()
		{
			if (buttonsReferences.gameButton.Length != 0)
			{
				for (int i = 0; i < EndlessRunnerManager.instance.version.platformScreensCount; i++)
				{
					buttonsReferences.gameButton[i].onClick.AddListener(delegate { PlaySceneAnimation(animations.gameStart); });
					buttonsReferences.shopButton[i].onClick.AddListener(delegate { PlaySceneAnimation(animations.shopStart); });
					buttonsReferences.multiplayerLobbyButton[i].onClick.AddListener(delegate { PlaySceneAnimation(animations.multiplayerLobbyStart); });
					buttonsReferences.settingsButton[i].onClick.AddListener(delegate { PlaySceneAnimation(animations.settingsStart); });
				}
			}
		}

		void PlaySceneAnimation(Animation animationToPlay)
		{

		}
	}
}
