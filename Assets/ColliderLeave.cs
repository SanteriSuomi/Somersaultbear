using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLeave : MonoBehaviour
{

    void Start()
    {
		    
    }

    void Update()
    {
        
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			gameObject.SetActive(false);
		}
	}
}
