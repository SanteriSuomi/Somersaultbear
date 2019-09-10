using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject prefabStart;
	[SerializeField]
	private GameObject prefabOne;

	private Transform gridPosition;

	void Start()
    {
    }

    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			print($"Hit {collision.gameObject.name}");

			Instantiate(prefabOne, transform.parent.position + new Vector3(40, 0, 0), Quaternion.identity);
		}
	}
}
