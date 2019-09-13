using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] menuButtons;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
		{
			foreach (var item in menuButtons)
			{
				if (item.activeSelf)
				{
					Time.timeScale = 1;

					item.SetActive(false);
				}
				else
				{
					Time.timeScale = 0;

					item.SetActive(true);
				}
			}
		}
    }
}
