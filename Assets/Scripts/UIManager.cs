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

				Time.timeScale = 1;

				item.SetActive(false);
			}
			else
			{
				scoreManager.PauseScoreCounting = true;

				Time.timeScale = 0;

				item.SetActive(true);
			}
		}
	}
}
