using UnityEngine;
using UnityEngine.SceneManagement;

namespace Somersaultbear
{
	#pragma warning disable CA1822 // Methods accessed from unity events so cannot be marked as static
	public class UILogic : MonoBehaviour
	{
		// "Central" logic for variety of things such as changing scene.
		public void ChangeScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
			Time.timeScale = 1;
		}

		public void ExitGame() => Application.Quit(0);
	}
}