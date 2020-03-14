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

        private bool GameIsPaused => Mathf.Abs(Time.timeScale) <= float.Epsilon;

        private void Awake() => scoreManager = FindObjectOfType<ScoreManager>();

        private void Start()
        {
            InputManager.Instance.InputScheme.MenuEvent += OnMenu;
            InitialCursorLockstate();
        }

        private static void InitialCursorLockstate()
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
            ChangeVolumeTo(1);
            ChangeTimeScaleTo(1);
            PauseScoreCounting(false);
        }

        private void SetPauseGame()
        {
            resetButton.SetActive(true);
            mainMenuButton.SetActive(true);
            ActivateMobileMenu(false);
            ChangeVolumeTo(0);
            ChangeTimeScaleTo(0);
            PauseScoreCounting(true);
        }

        private void ActivateMobileMenu(bool value)
        {
            if (InputManager.Instance.InputScheme.InputType == InputType.Mobile)
            {
                mobileMenu.SetActive(value);
            }
        }

        public void DeathMenu()
        {
            deathText.gameObject.SetActive(true);
            resetButton.SetActive(true);
            mainMenuButton.SetActive(true);
            EnableScoreText();

            mobileMenu.SetActive(false);
            mobileMenuButton.SetActive(false);

            ChangeVolumeTo(0);
            ChangeTimeScaleTo(0);
            PauseScoreCounting(true);
        }

        private void EnableScoreText()
        {
            scoreManager.TextScore.enabled = false;
            totalScoreText.gameObject.SetActive(true);
            totalScoreText.text = $"Score: {scoreManager.CurrentScore}";
        }

        private void ChangeVolumeTo(int volume) 
            => AudioListener.volume = volume;

        private void ChangeTimeScaleTo(int timeScale) 
            => Time.timeScale = timeScale;

        private void PauseScoreCounting(bool pause) 
            => scoreManager.PauseScoreCounting = pause;
    }
}