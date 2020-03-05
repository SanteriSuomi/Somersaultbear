using UnityEngine;
using UnityEngine.SceneManagement;

namespace Somersaultbear
{
	#pragma warning disable CA1822 // Methods accessed from unity events so cannot be marked as static
	public class UILogic : MonoBehaviour
	{
		private AsyncOperation mainSceneLoad;

		private enum SceneType
		{
			MainMenu,
			MainScene
		}
		[SerializeField]
		private SceneType sceneType = default;

		private void Awake()
		{
			if (sceneType == SceneType.MainMenu)
			{
				mainSceneLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
				mainSceneLoad.allowSceneActivation = false;
			}
		}

		// "Central" logic for variety of things such as changing scene.
		public void ChangeScene(string sceneName)
		{
			switch (sceneType)
			{
				case SceneType.MainMenu:
					mainSceneLoad.allowSceneActivation = true;
					break;

				case SceneType.MainScene:
					SceneManager.LoadScene(sceneName);
					AudioListener.volume = 1; // Make sure volume is on when loading main menu
					break;

				default:
					break;
			}

			Time.timeScale = 1;
		}

		public void ExitGame() => Application.Quit();
	}
}