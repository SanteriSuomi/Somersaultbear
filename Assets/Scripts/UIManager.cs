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
        [SerializeField]
        private GameObject mobileMenu = default;
        private AudioSource[] audioSources;
        private bool setMenusOnce;

        // Initialize the audiosources array by finding all audiosources active in the scene.
        private void Awake() => audioSources = FindObjectsOfType<AudioSource>();

        private void Start()
        {
            InputManager.Instance.InputScheme.MenuEvent += OnMenu;
            GetCursorLockstate();
        }

        private static void GetCursorLockstate()
        {
            // Lock cursor to the game window.
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void OnMenu() => Menu();

        private void Menu() // Public because also accessed by mobile menu button
        {
            foreach (GameObject item in menuItems)
            {
                if (item == null) continue;

                if (item.activeSelf)
                {
                    SetResumeGame(item);
                }
                else
                {
                    SetPauseGame(item);
                }
            }

            setMenusOnce = false;
        }

        private void SetResumeGame(GameObject item)
        {
            item.SetActive(false);
            if (!setMenusOnce)
            {
                setMenusOnce = true;
                PauseScoreCounting(false);
                PauseGame(1);
                MuteAudio(false);
                ActivateMobileMenu(true);
            }
        }

        private void SetPauseGame(GameObject item)
        {
            item.SetActive(true);
            if (!setMenusOnce)
            {
                setMenusOnce = true;
                PauseScoreCounting(true);
                PauseGame(0);
                MuteAudio(true);
                ActivateMobileMenu(false);
            }
        }

        /// <summary>
        /// Menu that happens on death.
        /// </summary>
        public void DeathMenu()
        {
            foreach (GameObject item in menuItems)
            {
                item.SetActive(true);
            }

            PauseScoreCounting(true);
            PauseGame(0);
            MuteAudio(true);
            EnableTextScore();
        }

        private void EnableTextScore()
        {
            scoreManager.TextScore.enabled = false;
            Text totalScoreText = menuItems[3].GetComponent<Text>();
            totalScoreText.text = $"Score: {scoreManager.CurrentScore}";
        }

        private void ActivateMobileMenu(bool value)
        {
            if (InputManager.Instance.InputScheme.InputType == InputType.Mobile)
            {
                mobileMenu.SetActive(value);
            }
        }

        private void MuteAudio(bool mute)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = mute;
            }
        }

        private void PauseGame(int pause) => Time.timeScale = pause;

        private void PauseScoreCounting(bool pause) => scoreManager.PauseScoreCounting = pause;
    }
}