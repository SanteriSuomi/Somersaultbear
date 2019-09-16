using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] menuButtons = default;

	[SerializeField]
	private ScoreManager scoreManager = default;

	void Update()
    {
        if (Input.GetButtonDown("Cancel"))
		{
			ShowMenuButtons();
		}
    }

	public void ShowMenuButtons()
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
