// Written by Peter Thompson - Playify.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyBox;

namespace EndlessRunnerEngine
{
	public class Player : MonoBehaviour
	{
		[SerializeField]
		internal Movement movement;
		public Model model;
		public Animations animations;

		public int playerId { get; internal set; }

		private RowMovement rows;

		[Serializable]
		internal class Movement
		{
			[Range(0.5f, 2f), SerializeField, Tooltip("How responsive the player will feel. This is useful for plane games where the player can't instantly move left and right.")]
			internal float floatiness = 1;

			[Range(0.5f, 2f), SerializeField, Tooltip("Speed that the player will move side to side.")]
			internal float sideSpeed = 1;

			[Range(0.5f, 2f), SerializeField, Tooltip("Speed that the player will move forwards.")]
			internal float forwardSpeed = 1;

			[Range(0.5f, 2f), SerializeField, Tooltip("General speed of the player. This affects both side speed and forwards speed concurrently.")]
			internal float generalSpeed = 1;

			internal float playerAngle = 0f;
			internal float playerStartAngle = 0f;
		}

		[Serializable]
		public class Model
		{
			[Tooltip("Player sprites must be in order of this: turning left, forward sprite, turning right.")]
			public Sprite[] playerSprites;

			public Shadow shadow;
			public class Shadow
			{
				public enum ShadowType { Round, FromPlayerSprite, None }
				public ShadowType type = ShadowType.Round;
			}
		}

		[Serializable]
		public class Animations
		{
			public class ThreeDimentional
			{
				public AnimationClip forwardAnimation;
				public AnimationClip leftAnimation;
				public AnimationClip rightAnimation;

				public AnimationClip jumpingAnimation;
				public AnimationClip slidingAnimation;
			}

			public class TwoDimentional
			{
				public Sprite[] forwardAnimation;
				public Sprite[] leftAnimation;
				public Sprite[] rightAnimation;
			}
		}

		#region Script movement variables
		private float horizontalSpeedAngleEffector;
		private float curAngle;
		//private float curScaledAngle;
		private float lastPos;
		private float spriteAngle;
		private float spriteAngleSensitivity;
		#endregion

		void Awake()
		{
			movement.playerAngle = movement.playerStartAngle;
		}

		void Start()
		{
			if (this == EndlessRunnerManager.localPlayer)
			{
				rows = EndlessRunnerManager.localRow;
			}
		}

		IEnumerator StartingAnimation()
		{
			while (true)
			{
				yield break;
			}
		}

		private void Update()
		{
			RetrieveControls();
			PlayerMovement();
		}

		void RetrieveControls()
		{

		}

		/// <summary>
		/// This applies keyboard controls that is meant so "simulate" the same movement experience as a Kinect sensor. 
		/// This is pretty much just a smoothed keyboard controller.
		/// </summary>
		void OldControls()
		{
			float keyboardPos = lastPos;

			//// Down arrow
			//if (PlayerControls.instance.Vertical(playerId) > 0)
			//{
			//	if (keyboardPos > transform.position.x + 0.1f && keyboardPos < transform.position.x - 0.1f)
			//	{
			//		keyboardPos = transform.position.x;
			//	}
			//	else if (keyboardPos > transform.position.x)
			//	{
			//		keyboardPos -= keyboardAngleSensitivity * Time.deltaTime;
			//	}
			//	else if (keyboardPos < transform.position.x)
			//	{
			//		keyboardPos += keyboardAngleSensitivity * Time.deltaTime;
			//	}
			//}

			//// Left arrow
			//if (KeyboardControls.instance.keys[playerID - 1].Left())
			//{
			//	if (keyboardPos > -maxKeyboardPos)
			//	{
			//		keyboardPos -= keyboardAngleSensitivity * Time.deltaTime;
			//	}
			//}

			//// Right arrow
			//if (KeyboardControls.instance.keys[playerID - 1].Right())
			//{
			//	if (keyboardPos < maxKeyboardPos)
			//	{
			//		keyboardPos += keyboardAngleSensitivity * Time.deltaTime;
			//	}
			//}

			//// Down & Left arrow
			//if (KeyboardControls.instance.keys[playerID - 1].LeftHalf())
			//{
			//	if (keyboardPos > -maxKeyboardPos)
			//	{
			//		keyboardPos -= (keyboardAngleSensitivity / 2) * Time.deltaTime;
			//	}
			//}

			//// Down & Right arrow
			//if (KeyboardControls.instance.keys[playerID - 1].RightHalf())
			//{
			//	if (keyboardPos < maxKeyboardPos)
			//	{
			//		keyboardPos += (keyboardAngleSensitivity / 2) * Time.deltaTime;
			//	}
			//}

			//lastPos = keyboardPos;

			//float planeAngleLerped = Mathf.Lerp(planeLastAngle, (keyboardPos - transform.position.x) * kinectAngleSensitivity, planeAngleSmoothing * Time.deltaTime);

			//rawPlaneAngle = (keyboardPos - transform.position.x) * kinectAngleSensitivity;
			//planeAngle = planeAngleLerped;
			//planeLastAngle = planeAngleLerped;

			//Debug.Log(EziCode.LogString(name) + "Plane angle is: " + planeAngle + ". Plane Position is: " + plane.transform.position.x + ". User Position is: " + keyboardPos);

			//AngleBoundaries();
		}

		/// <summary>
		/// This makes the player able to move using the selected controls.
		/// </summary>
		void PlayerMovement()
		{
			// Make sure to do the background movement logic here too.
			if (EndlessRunnerManager.gameStarted)
			{
				AngleBoundaries();

				// Make power number a variable
				horizontalSpeedAngleEffector = 30f - Mathf.Pow(movement.generalSpeed + movement.sideSpeed, movement.floatiness * 2.375f);

				if (horizontalSpeedAngleEffector < 10)
				{
					horizontalSpeedAngleEffector = 10f;
				}

				//float xPos = transform.position.x + (curAngle / horizontalSpeedAngleEffector) * Time.deltaTime;

				// Scales the angle to the frame animation count for the player
				int planeAnimFrameCount = (model.playerSprites.Length / 2) - 1;
				spriteAngle = (int)Scale(-90, 90, -planeAnimFrameCount, planeAnimFrameCount, curAngle * spriteAngleSensitivity);
			}
		}

		/// <summary>
		/// If the players current angle exceeds the maximum angle then this sets it back to the maximum angle.
		/// </summary>
		void AngleBoundaries()
		{
			if (curAngle > 90f)
			{
				curAngle = 90f;
			}
			else if (curAngle < -90f)
			{
				curAngle = -90f;
			}
		}

		private static float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
		{

			float OldRange = (OldMax - OldMin);
			float NewRange = (NewMax - NewMin);
			float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

			return (NewValue);
		}
	}
}
