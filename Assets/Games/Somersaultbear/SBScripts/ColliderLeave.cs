using System.Collections;
using UnityEngine;

namespace Somersaultbear
{
	public class ColliderLeave : MonoBehaviour
	{
		[SerializeField]
		[Range(1, 20)]
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

			#if UNITY_EDITOR
			Debug.Log($"{transform.parent.name} will be set inactive in {random} seconds.");
			#endif

			yield return new WaitForSeconds(random);
			// Deactivate the whole scene prefab including the children.
			transform.parent.gameObject.SetActive(false);
		}
	}
}