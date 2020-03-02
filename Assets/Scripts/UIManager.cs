using UnityEngine;
using UnityEngine.UI;

namespace Somersaultbear
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager scoreManager = default;
        [SerializeField]
        private GameObject[] menuItems = default;
        private AudioSource[] audioSources;

        // Initialize the audiosources array by finding all audiosources active in the scene.
        private void Awake() => audioSources = FindObjectsOfType<AudioSource>();

        private void Start() => GetCursorLockstate();

        private static void GetCursorLockstate()
        {
            // Lock cursor to the game window.
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update() => GetInput();

        private void GetInput()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                ShowMenuItems();
            }
        }

        private void ShowMenuItems()
        {
            foreach (GameObject item in menuItems)
            {
                if (item.activeSelf)
                {
                    SetResumeGame(item);
                }
                else
                {
                    SetPauseGame(item);
                }
            }
        }

        private void SetResumeGame(GameObject item)
        {
            PauseScoreCounting(false);
            PauseGame(1);
            MuteAudio(false);
            item.SetActive(false);
        }

        private void SetPauseGame(GameObject item)
        {
            PauseScoreCounting(true);
            PauseGame(0);
            MuteAudio(true);
            item.SetActive(true);
        }

        public void ShowMenuItemsDeath()
        {
            foreach (GameObject item in menuItems)
            {
                PauseScoreCounting(true);
                PauseGame(0);
                MuteAudio(true);
                // Enable the game over score display.
                scoreManager.TextScore.enabled = false;
                // Get the Text component from the array.
                Text totalScoreText = menuItems[3].GetComponent<Text>();
                totalScoreText.text = $"Score: {scoreManager.CurrentScore}";
                item.SetActive(true);
            }
        }

        // Audio mute control.
        private void MuteAudio(bool mute)
        {
            foreach (var audSrc in audioSources)
            {
                audSrc.mute = mute;
            }
        }

        // Time scale control.
        private void PauseGame(int pause) => Time.timeScale = pause;

        private void PauseScoreCounting(bool pause) => scoreManager.PauseScoreCounting = pause;
    }
}