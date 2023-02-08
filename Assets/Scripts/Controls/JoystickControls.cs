// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
	public class JoystickControls : MonoBehaviour
	{
		public static JoystickControls instance;

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
	}
}
