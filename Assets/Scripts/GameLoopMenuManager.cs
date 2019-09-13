using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] menuButtons = null;

	[SerializeField]
	private ScoreManager scoreManager = null;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
		{
			foreach (var item in menuButtons)
			{
				if (item.activeSelf)
				{
					scoreManager.gameIsPaused = false;

					Time.timeScale = 1;

					item.SetActive(false);
				}
				else
				{
					scoreManager.gameIsPaused = true;

					Time.timeScale = 0;

					item.SetActive(true);
				}
			}
		}
    }
}
