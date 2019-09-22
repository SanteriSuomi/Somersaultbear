using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
	// "Central" logic mainly used for actions such as button presses.

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
