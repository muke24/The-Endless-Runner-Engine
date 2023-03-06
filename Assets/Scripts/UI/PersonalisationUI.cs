using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunnerEngine
{
    public class PersonalisationUI : MonoBehaviour
    {
		[SerializeField]
		private Button[] backButton;

		private void Start()
		{
			for (int i = 0; i < backButton.Length; i++)
			{
				backButton[i].onClick.AddListener(GoBack);
			}
		}

		void GoBack()
		{
			// CHANGE: Make an animation play before this.
			UIManager.instance.SetPage(1);
		}
	}
}
