using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
	// "Central" logic for variety of things such as changing scene.

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
