using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunnerEngine
{
    public class StartGameButton : MonoBehaviour
    {
        [Header("Place this script on buttons that will start a game. \n" +
            "This allows each respective button to hold a reference to \nthe game level to load."), SerializeField]
        private Level levelToLoadAponPressing;

        private Button thisButton;

		private void OnEnable()
		{
			if (thisButton == null)
			{
				thisButton = GetComponent<Button>();
			}

			thisButton.onClick.AddListener(StartGame);
		}

		private void OnDisable()
		{
			thisButton.onClick.RemoveListener(StartGame);
		}

		void StartGame()
		{
            EndlessRunnerManager.instance.StartGame(levelToLoadAponPressing);
		}
	}
}
