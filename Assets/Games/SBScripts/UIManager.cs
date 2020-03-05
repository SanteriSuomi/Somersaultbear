using UnityEngine;
using UnityEngine.UI;

namespace Somersaultbear
{
    public class UIManager : MonoBehaviour
    {
        private ScoreManager scoreManager;
        [SerializeField]
        private Text deathText = default;
        [SerializeField]
        private Text totalScoreText = default;
        [SerializeField]
        private GameObject resetButton = default;
        [SerializeField]
        private GameObject mainMenuButton = default;
        [SerializeField]
        private GameObject mobileMenu = default;
        [SerializeField]
        private GameObject mobileMenuButton = default;

        private bool GameIsPaused { get { return Mathf.Abs(Time.timeScale) <= float.Epsilon; } }

        // Initialize the audiosources array by finding all audiosources active in the scene.
        private void Awake() => scoreManager = FindObjectOfType<ScoreManager>();

        private void Start()
        {
            InputManager.Instance.InputScheme.MenuEvent += OnMenu;
            GetCursorLockstate();
        }

        private static void GetCursorLockstate()
            => Cursor.lockState = CursorLockMode.Confined;

        public void OnMenuMobile() => OnMenu(); // Public because also accessed by mobile menu button

        private void OnMenu()
        {
            if (GameIsPaused)
            {
                SetResumeGame();
            }
            else
            {
                SetPauseGame();
            }
        }

        private void SetResumeGame()
        {
            resetButton.SetActive(false);
            mainMenuButton.SetActive(false);
            ActivateMobileMenu(true);
            MuteAudio(1);
            PauseGame(1);
            PauseScoreCounting(false);
        }

        private void SetPauseGame()
        {
            resetButton.SetActive(true);
            mainMenuButton.SetActive(true);
            ActivateMobileMenu(false);
            MuteAudio(0);
            PauseGame(0);
            PauseScoreCounting(true);
        }

        private void ActivateMobileMenu(bool value)
        {
            if (InputManager.Instance.InputScheme.InputType == InputType.Mobile)
            {
                mobileMenu.SetActive(value);
            }
        }

        /// <summary>
        /// Menu that happens on death.
        /// </summary>
        public void DeathMenu()
        {
            deathText.gameObject.SetActive(true);
            resetButton.SetActive(true);
            mainMenuButton.SetActive(true);
            EnableScoreText();

            mobileMenu.SetActive(false);
            mobileMenuButton.SetActive(false);

            MuteAudio(0);
            PauseGame(0);
            PauseScoreCounting(true);
        }

        private void EnableScoreText()
        {
            scoreManager.TextScore.enabled = false;
            totalScoreText.gameObject.SetActive(true);
            totalScoreText.text = $"Score: {scoreManager.CurrentScore}";
        }

        private void MuteAudio(int mute) => AudioListener.volume = mute;

        private void PauseGame(int pause) => Time.timeScale = pause;

        private void PauseScoreCounting(bool pause) => scoreManager.PauseScoreCounting = pause;
    }
}