using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLeave : MonoBehaviour
{
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			print($"{gameObject.transform.parent.gameObject.name} has been set inactive.");

			gameObject.transform.parent.gameObject.SetActive(false);	
		}
	}
}
