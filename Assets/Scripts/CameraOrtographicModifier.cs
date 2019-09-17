using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOrtographicModifier : MonoBehaviour
{
	[SerializeField]
	private float lensSmoothTime = 0.25f;

	[SerializeField]
	private float detectionHeight = 2.5f;

	private float yVelocity = 0f;

	private const float maxLensSize = 7f;

	private const float minLensSize = 5f;

	private CinemachineVirtualCamera cinemachine = default;

	private GameObject player = default;

	[SerializeField]
	private LayerMask groundLayer = default;

	void Start()
	{
		cinemachine = GetComponent<CinemachineVirtualCamera>();

		player = GameObject.Find("PRE_Player");
	}

	void LateUpdate()
	{
		// Cast a 2d raycast down, and use detection height as the height. Only detect layers specified in groundLayer.
		RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, detectionHeight, groundLayer);

		Debug.DrawRay(player.transform.position, Vector2.down, Color.red);

		// When the raycast no longer hits the ground, change the camera size to big.
		if (!(rayHit && rayHit.collider))
		{
			StartCoroutine(ChangeOrtographicSizeBig());
		}
		// Else make it small when player is close to the ground.
		else
		{
			StartCoroutine(ChangeOrtographicSizeSmall());
		}
	}

	private IEnumerator ChangeOrtographicSizeBig()
	{
		// Change size smoothly with smoothdamp.
		cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, maxLensSize, ref yVelocity, lensSmoothTime);

		// Wait for a second to prevent any unexpected "spam" behaviour or such.
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator ChangeOrtographicSizeSmall()
	{
		cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, minLensSize, ref yVelocity, lensSmoothTime);

		yield return new WaitForSeconds(1f);
	}
}