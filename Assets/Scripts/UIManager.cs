using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager = default;
    [SerializeField]
    private GameObject[] menuItems = default;
    private AudioSource[] audioSources;

    private void Start()
    {
        // Lock cursor to the game window.
        Cursor.lockState = CursorLockMode.Confined;
        // Initialize the audiosources array by finding all audiosources active in the scene.
        audioSources = FindObjectsOfType<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ShowMenuItems();
        }
    }

    private void ShowMenuItems()
    {
        foreach (var item in menuItems)
        {
            if (item.activeSelf)
            {
                PauseScoreCounting(false);
                PauseGame(1);
                MuteAudio(false);
                item.SetActive(false);
            }
            else
            {
                PauseScoreCounting(true);
                PauseGame(0);
                MuteAudio(true);
                item.SetActive(true);
            }
        }
    }

    public void ShowMenuItemsDeath()
    {
        foreach (var item in menuItems)
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
    private void PauseGame(int pause)
    {
        Time.timeScale = pause;
    }

    private void PauseScoreCounting(bool pause)
    {
        scoreManager.PauseScoreCounting = pause;
    }
}