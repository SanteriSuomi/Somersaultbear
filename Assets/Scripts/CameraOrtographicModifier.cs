using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOrtographicModifier : MonoBehaviour
{
	private CinemachineVirtualCamera cinemachine;

	private GameObject player;

	private Rigidbody2D playerRigidbody;

	[SerializeField]
	private float lensSmoothTime = 0.25f;

	private const float maxLensSize = 7f;

	private const float minLensSize = 5f;

	private float yVelocity = 0f;

	void Start()
	{
		cinemachine = GetComponent<CinemachineVirtualCamera>();

		player = GameObject.FindGameObjectWithTag("Player");

		playerRigidbody = player.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (playerRigidbody.velocity.y >= 1.5f)
		{
			StartCoroutine(ChangeOrtographicSize(true));
		}
		else if (playerRigidbody.velocity.y >= 0f && playerRigidbody.velocity.y <= 0.15f)
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

		yield return new WaitForSeconds(1.5f);
	}
}
