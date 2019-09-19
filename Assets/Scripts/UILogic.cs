using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
	// "Central" logic for variety of UI logic, mainly using buttons.

	// Change the scene using the scene name, and unpause the game (since the game is most likely paused).
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);

		Time.timeScale = 1;
	}

	public void ExitGame()
	{
		Application.Quit(0);
	}
}
