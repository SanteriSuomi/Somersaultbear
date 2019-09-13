using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
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
