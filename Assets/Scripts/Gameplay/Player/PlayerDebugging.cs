using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
	[ExecuteInEditMode]
	public class PlayerDebugging : MonoBehaviour
	{
		[SerializeField]
		internal bool updateDisplaySprite = false;
		public Sprite playerDisplaySprite;
		internal SpriteRenderer playerRenderer;

		// Update is called once per frame
		void Update()
		{
			UpdateDisplaySprite();
		}

		/// <summary>
		/// This changes the player sprite in the editor simply for display and debug purposes.
		/// </summary>
		void UpdateDisplaySprite()
		{

			if (updateDisplaySprite)
			{
				updateDisplaySprite = false;

				if (playerRenderer != null && playerDisplaySprite != null)
				{
					playerRenderer.sprite = playerDisplaySprite;
					Debug.Log("Player display sprite changed.");

				}
				else
				{
					if (playerRenderer == null && playerDisplaySprite != null)
					{
						Debug.LogWarning("Unable to change player display sprite. Make sure to reference the player sprite renderer.");

					}
					else if (playerRenderer != null && playerDisplaySprite == null)
					{
						Debug.LogWarning("Unable to change player display sprite. Make sure to reference the sprite you would like the player sprite renderer to display.");
					}
					else
					{
						Debug.LogWarning("Unable to change player display sprite. Make sure to reference the player sprite renderer.");
						Debug.LogWarning("Unable to change player display sprite. Make sure to reference the sprite you would like the player sprite renderer to display.");
					}
				}
			}
		}
	}
}
