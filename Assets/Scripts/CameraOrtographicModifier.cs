using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOrtographicModifier : MonoBehaviour
{
	[SerializeField]
	private float lensSmoothTime = 0.25f;

	private const float maxLensSize = 7f;

	private const float minLensSize = 5f;

	private float yVelocity = 0f;

	private CinemachineVirtualCamera cinemachine = default;

	private Rigidbody2D playerRigidbody = default;

	void Start()
	{
		cinemachine = GetComponent<CinemachineVirtualCamera>();

		playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (playerRigidbody.velocity.y >= 2f)
		{
			StartCoroutine(ChangeOrtographicSize(true));
		}
		else if (playerRigidbody.velocity.y >= 0f && playerRigidbody.velocity.y <= 0.1f)
		{
			StartCoroutine(ChangeOrtographicSize(false));
		}
	}

	private IEnumerator ChangeOrtographicSize(bool changeToBigger)
	{
		if (changeToBigger)
		{
			cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, maxLensSize, ref yVelocity, lensSmoothTime);
		}
		else if (!changeToBigger)
		{
			cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, minLensSize, ref yVelocity, lensSmoothTime);
		}

		yield return new WaitForSeconds(2.5f);
	}
}