using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	// Array for the menu buttons in the main game loop.
	[SerializeField]
	private GameObject[] menuButtons = default;

	[SerializeField]
	private ScoreManager scoreManager = default;

	// Check for user input continously.
	void Update()
    {
        if (Input.GetButtonDown("Cancel"))
		{
			ShowMenuButtons();
		}
    }

	public void ShowMenuButtons()
	{
		// Loop through the menu buttons array.
		foreach (var item in menuButtons)
		{
			if (item.activeSelf)
			{
				// Continue score counting.
				scoreManager.PauseScoreCounting = false;

				// Unpause.
				Time.timeScale = 1;

				// Deactive (hide) the menu buttons.
				item.SetActive(false);
			}
			else
			{
				// Pause score counting.
				scoreManager.PauseScoreCounting = true;

				// Pause, or freeze the time.
				Time.timeScale = 0;

				// Show menu buttons.
				item.SetActive(true);
			}
		}
	}
}
