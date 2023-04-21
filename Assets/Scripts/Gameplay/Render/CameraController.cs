using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerEngine
{
    public class CameraController : MonoBehaviour
    {
		[SerializeField]
		internal float size = 5;
		[SerializeField]
		internal float sizeSmoothingAmount;

		[SerializeField]
		internal float positionSmoothingAmount;
		[SerializeField]
		internal float sidewaysPositionMultiplier = 0.5f;

        private Camera cam;
		private Player playerToFollow;

		internal Vector3 curVelocity;
		internal float sidewaysVelocity;

		[SerializeField]
		internal float velocityMultiplier = 0.5f;

		private Vector3 startPos = new Vector3(0, 0, 0);

		public static CameraController instance;

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = this;
			}

			cam = GetComponent<Camera>();
			startPos = transform.position;
		}

		private void Start()
		{
			ResetCameraPosition();
		}

		private void Update()
		{
			if (EndlessRunnerManager.gameStarted)
			{
				if (playerToFollow == null)
				{
					playerToFollow = EndlessRunnerManager.localPlayer;
				}

				if (EndlessRunnerManager.instance.render.direction == EndlessRunnerManager.Render.GameDirection.Down ||
					EndlessRunnerManager.instance.render.direction == EndlessRunnerManager.Render.GameDirection.Up)
				{
					sidewaysVelocity = curVelocity.x;
				}
				else
				{
					sidewaysVelocity = curVelocity.y;
				}

				transform.position = Vector3.SmoothDamp(transform.position, playerToFollow.transform.position / sidewaysPositionMultiplier, ref curVelocity, (positionSmoothingAmount + (sidewaysVelocity * velocityMultiplier)) * Time.smoothDeltaTime);
			}
		}

		void ResetCameraPosition()
		{
			StartCoroutine(LerpCamBackToOgPos());
		}

		IEnumerator LerpCamBackToOgPos()
		{
			while (true)
			{
				if (transform.position != startPos)
				{
					transform.position = Vector3.Lerp(transform.position, startPos, positionSmoothingAmount * Time.smoothDeltaTime);
					yield return null;
				}
				else
				{
					yield break;
				}
			}
		}
	}
}
