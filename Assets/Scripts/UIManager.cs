using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] menuButtons = default;

	[SerializeField]
	private ScoreManager scoreManager = default;

	private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
		{
			ShowMenuButtons();
		}
    }

	private void ShowMenuButtons()
	{
		foreach (var item in menuButtons)
		{
			if (item.activeSelf)
			{
				scoreManager.PauseScoreCounting = false;

				// Unpause the game.
				Time.timeScale = 1;

				// Hide the menu buttons.
				item.SetActive(false);
			}
			else
			{
				scoreManager.PauseScoreCounting = true;

				// Pause the game.
				Time.timeScale = 0;

				// Show the menu buttons.
				item.SetActive(true);
			}
		}
	}
}
