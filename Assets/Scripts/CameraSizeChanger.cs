using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraSizeChanger : MonoBehaviour
{
	[SerializeField]
	private float lensSmoothTime = 0.5f;

	[SerializeField]
	private float detectionHeight = 1.5f;

	private float yVelocity = 0f;

	private const float maxLensSize = 7f;

	private const float minLensSize = 5f;

	private const float timeToWait = 2f;

    [SerializeField]
    private Camera _camera = default;

    [SerializeField]
	private GameObject player = default;

	[SerializeField]
	private LayerMask groundLayer = default;

	private void LateUpdate()
	{
		// Cast a 2d raycast down, and use detection height as the height. Only detect layers specified in groundLayer.
		RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, detectionHeight, groundLayer);

		Debug.DrawRay(player.transform.position, Vector2.down * detectionHeight, Color.black);

		// When the raycast hits the ground layer "Ground", change the size to small (normal)
		if (rayHit && rayHit.collider)
		{
			StartCoroutine(ChangeCameraSizeSmall());
		}
		// Else make it big when player is not close to the ground.
		else
		{
			StartCoroutine(ChangeCameraSizeBig());
		}
	}

	private IEnumerator ChangeCameraSizeSmall()
	{
		// Change size smoothly with smoothdamp.
		//cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, minLensSize, ref yVelocity, lensSmoothTime);

		// Wait for a X amount of seconds to prevent any unexpected "spam" behaviour or such.
		yield return new WaitForSeconds(timeToWait);
	}

	private IEnumerator ChangeCameraSizeBig()
	{
		//cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, maxLensSize, ref yVelocity, lensSmoothTime);

		yield return new WaitForSeconds(timeToWait);
	}
}