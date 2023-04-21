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
			public ControlType selectedControl = ControlType.Keyboard;

			public Player assignedPlayer { get; internal set; }

			#region Control Type Variables
			[ConditionalField(nameof(selectedControl), false, ControlType.Keyboard)]
			public KeyboardControls keyboardControls;
			[ConditionalField(nameof(selectedControl), false, ControlType.Touch)]
			public TouchControls touchControls;

			[ConditionalField(nameof(selectedControl), false, ControlType.Gyroscope)]
			public GyroscopeControls gyroscopeControls;

			[ConditionalField(nameof(selectedControl), false, ControlType.ScreenButtons)]
			public ScreenButtonControls screenButtonControls;

			[ConditionalField(nameof(selectedControl), false, ControlType.Joystick)]
			public JoystickControls joystickControls;

			[ConditionalField(nameof(selectedControl), false, ControlType.Camera)]
			public CameraControls cameraControls;

			[Serializable]
			public class KeyboardControls
			{
				[SerializeField]
				internal AnimationCurve axisCurve;
				[SerializeField]
				internal float axisSpeed = 5f;

				[SerializeField, Range(0.025f, 0.25f)]
				internal float axisDeadZone = 0.05f;

				[SearchableEnum, Space]
				public KeyCode forwardButton = KeyCode.UpArrow;
				[SearchableEnum]
				public KeyCode backwardButton = KeyCode.DownArrow;
				[SearchableEnum]
				public KeyCode leftButton = KeyCode.LeftArrow;
				[SearchableEnum]
				public KeyCode rightButton = KeyCode.RightArrow;

				public AlternativeControls alternativeControls;

				[Serializable]
				public class AlternativeControls
				{
					[SearchableEnum]
					public KeyCode forwardButton = KeyCode.W;
					[SearchableEnum]
					public KeyCode backwardButton = KeyCode.S;
					[SearchableEnum]
					public KeyCode leftButton = KeyCode.A;
					[SearchableEnum]
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
				[Range(0, 1), Tooltip("Using a camera to body track the player can cause jittery movement sometimes. This allows smoothing of the jitters.")]
				public float positionSmoothing;
			}
			#endregion

		}

		private float lastHor = 0f;

		public float Horizontal(int playerId)
		{
			Control playerControls = controls[playerId];
			float curHor = 0;

			if (playerControls.selectedControl == Control.ControlType.Keyboard)
			{
				bool fwd = Input.GetKey(playerControls.keyboardControls.forwardButton) | Input.GetKey(playerControls.keyboardControls.alternativeControls.forwardButton);
				bool lft = Input.GetKey(playerControls.keyboardControls.leftButton) | Input.GetKey(playerControls.keyboardControls.alternativeControls.leftButton);
				bool rgt = Input.GetKey(playerControls.keyboardControls.rightButton) | Input.GetKey(playerControls.keyboardControls.alternativeControls.rightButton);

				// Start a timer and fix axis curve using the timer
				if (fwd && !lft && !rgt)
				{
					curHor = 0;
				}
				else if (fwd && lft && !rgt)
				{
					curHor = -0.5f;
				}
				else if (fwd && !lft && rgt)
				{
					curHor = 0.5f;
				}
				else if (!fwd && !lft && rgt)
				{
					curHor = 1;
				}
				else if (!fwd && lft && !rgt)
				{
					curHor = -1;
				}
				else
				{
					curHor = 0;
				}

				lastHor = Mathf.Lerp(lastHor, curHor, playerControls.keyboardControls.axisSpeed * Time.smoothDeltaTime);

				return lastHor;
			}

			// This is here for now to remove error
			return curHor;
		}

		public float Vertical(int playerId)
		{
			if (Horizontal(playerId) > 0)
			{
				return (1 - Horizontal(playerId));
			}
			else if (Horizontal(playerId) < 0)
			{
				return (1 + Horizontal(playerId));
			}

			return 0;
		}
	}
}
