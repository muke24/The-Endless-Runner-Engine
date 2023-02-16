// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyBox;

namespace EndlessRunnerEngine
{
	public class PlayerControls : MonoBehaviour
	{
		#region Singleton
		public static PlayerControls instance;

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
		#endregion

		[Tooltip("Each control represents the controls used for a player. Eg. The first control element will control player 1, and the second control element will control player 2.")]
		public Control[] controls;

		[Serializable]
		public class Control
		{
			[SerializeField, HideInInspector]
			private string name = "Player";

			public enum ControlType { Keyboard, Touch, Gyroscope, ScreenButtons, Joystick, Camera }
			[SerializeField, SearchableEnum]
			public ControlType controlType = ControlType.Keyboard;

			public Player assignedPlayer { get; internal set; }

			[ConditionalField(nameof(controlType), false, ControlType.Keyboard)]
			public KeyboardControls keyboardControls;
			[ConditionalField(nameof(controlType), false, ControlType.Touch)]
			public TouchControls touchControls;

			[ConditionalField(nameof(controlType), false, ControlType.Gyroscope)]
			public GyroscopeControls gyroscopeControls;

			[ConditionalField(nameof(controlType), false, ControlType.ScreenButtons)]
			public ScreenButtonControls screenButtonControls;

			[ConditionalField(nameof(controlType), false, ControlType.Joystick)]
			public JoystickControls joystickControls;

			[ConditionalField(nameof(controlType), false, ControlType.Camera)]
			public CameraControls cameraControls;

			[Serializable]
			public class KeyboardControls
			{
				public KeyCode upButton = KeyCode.UpArrow;
				public KeyCode downButton = KeyCode.DownArrow;
				public KeyCode leftButton = KeyCode.LeftArrow;
				public KeyCode rightButton = KeyCode.RightArrow;

				public AlternativeControls alternativeControls;

				[Serializable]
				public class AlternativeControls
				{
					public KeyCode upButton = KeyCode.W;
					public KeyCode downButton = KeyCode.S;
					public KeyCode leftButton = KeyCode.A;
					public KeyCode rightButton = KeyCode.D;
				}
			}

			[Serializable]
			public class TouchControls
			{
				//public float sensitivity;
			}

			[Serializable]
			public class GyroscopeControls
			{
				[Tooltip("How much tilt will be needed in order to move the player.")]
				public float sensitivity;
				[Tooltip("How often the gyroscope will be updated.")]
				public int updateRate;

			}

			[Serializable]
			public class ScreenButtonControls
			{
				public float buttonSize;
			}

			[Serializable]
			public class JoystickControls
			{
				
			}

			[Serializable]
			public class CameraControls
			{
				[Tooltip("Using a camera to body track the player can cause jittery movement sometimes. This allows smoothing of the jitters.")]
				public float cameraSmoothing;
			}
		}
	}
}
