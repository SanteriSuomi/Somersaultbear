﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLeave : MonoBehaviour
{
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(RandomDelay());
		}
	}

	private IEnumerator RandomDelay()
	{
		var random = Random.Range(0f, 10f);

		print($"{gameObject.transform.parent.gameObject.name} will be set inactive in {random} seconds.");

		yield return new WaitForSeconds(random);

		gameObject.transform.parent.gameObject.SetActive(false);
	}
}
