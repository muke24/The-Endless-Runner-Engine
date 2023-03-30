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
				[SearchableEnum]
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
				[Tooltip("Using a camera to body track the player can cause jittery movement sometimes. This allows smoothing of the jitters.")]
				public float cameraSmoothing;
			}
			#endregion

		}

		private float lastVertical;
		public float Vertical()
		{
			float axis = lastVertical;
			int playerId = EndlessRunnerManager.localPlayer.playerId;
			Control playerControls = controls[playerId];

			if (playerControls.selectedControl == Control.ControlType.Keyboard)
			{
				var render = EndlessRunnerManager.instance.render;

				// Game Orientation
				var down = EndlessRunnerManager.Render.GameDirection2D.Down;
				var up = EndlessRunnerManager.Render.GameDirection2D.Up;

				// If game orientation is vertical
				if (render.direction2D == up || render.direction2D == down)
				{
					if (Input.GetKey(playerControls.keyboardControls.forwardButton))
					{
						float curVert = lastVertical + Time.deltaTime;
						return curVert;
					}
				}
				// If game orientation is horizontal
				else
				{

				}

				return axis;
			}
			// This is here for now to remove error
			return 0;
		}
	}
}
