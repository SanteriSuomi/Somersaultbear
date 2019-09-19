using System.Collections;
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
		int random = Random.Range(0, maxRandomRange);

		print($"{gameObject.transform.parent.gameObject.name} will be set inactive in {random} seconds.");

		yield return new WaitForSeconds(random);

		// Deactivate the whole scene prefab, parent of the script.
		gameObject.transform.parent.gameObject.SetActive(false);
	}
}