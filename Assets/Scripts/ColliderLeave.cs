using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLeave : MonoBehaviour
{
	[SerializeField]
	private int maxRandomRange = 10;

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(RandomDelay());
		}
	}

	private IEnumerator RandomDelay()
	{
		// Get a new random number in the range of 0 and maxRandomRange
		int random = Random.Range(0, maxRandomRange);

		print($"{gameObject.transform.parent.gameObject.name} will be set inactive in {random} seconds.");

		// Wait for random amount of seconds.
		yield return new WaitForSeconds(random);

		// Deactivate the scene prefab object.
		gameObject.transform.parent.gameObject.SetActive(false);
	}
}