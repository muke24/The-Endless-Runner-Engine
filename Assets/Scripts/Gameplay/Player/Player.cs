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
		//private EndlessRunnerManager manager
		//{
		//	get
		//	{
		//		return FindObjectOfType<EndlessRunnerManager>();
		//	}
		//}

		[Serializable]
		internal class Movement
		{
			[SerializeField, Tooltip("How responsive the player will feel. This is useful for plane games where the player can't instantly move left and right.")]
			internal float floatiness = 1;

			[SerializeField, Tooltip("Speed that the player will move side to side.")]
			internal float sideSpeed = 1;

			[SerializeField, Tooltip("Speed that the player will move forwards.")]
			internal float forwardSpeed = 1;

			[SerializeField, Tooltip("General speed of the player. This affects both side speed and forwards speed concurrently.")]
			internal float generalSpeed = 1;

			internal float playerAngle = 0f;
			internal float playerStartAngle = 0f;
		}

		[Serializable]
		public class Model
		{
			//
			//private EndlessRunnerManager manager
			//{
			//	get
			//	{
			//		return FindObjectOfType<EndlessRunnerManager>();
			//	}
			//}

			//[ConditionalField(nameof(manager.render.renderType), false, EndlessRunnerManager.Render.RenderType.TwoDimentional)]
			[Tooltip("If game is 2D")]
			public TwoDimentional twoDimentional;
			[Tooltip("If game is 3D")]
			public ThreeDimentional threeDimentional;

			[Serializable]
			public class TwoDimentional
			{
				//[ConditionalField(nameof(EndlessRunnerManager.editorInstance.render.renderType), false, EndlessRunnerManager.Render.RenderType.ThreeDimentional)]
				public GameObject playerModel2D;

				[Tooltip("Player sprites must be in order of this: turning left, forward sprite, turning right.")]
				public Sprite[] playerSprites;

				public bool useShadow = false;

				public Shadow shadow;
				public class Shadow
				{
					public enum ShadowType { Round, FromPlayerSprite}
					public ShadowType type = ShadowType.Round;
				}
			}

			[Serializable]
			public class ThreeDimentional
			{
				//[ConditionalField(nameof(EndlessRunnerManager.editorInstance.render.renderType), false, EndlessRunnerManager.Render.RenderType.TwoDimentional)]
				public GameObject playerModel3D;

				
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

		void Awake()
		{
			movement.playerAngle = movement.playerStartAngle;
		}

		void Start()
		{
			if (this == EndlessRunnerManager.localPlayer)
			{

			}
		}

		IEnumerator StartingAnimation()
		{
			while (true)
			{
				yield break;
			}
		}
	}
}
