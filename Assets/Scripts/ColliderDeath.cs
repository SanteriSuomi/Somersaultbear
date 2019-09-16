using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeath : MonoBehaviour
{
	private UILogic uiLogic = default;

	void Start()
	{
		uiLogic = GameObject.Find("PRE_UILogic").GetComponent<UILogic>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			uiLogic.ChangeScene("SCE_GameLoop");
		}
	}
}
