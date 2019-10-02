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
            if (item.activeSelf && item.CompareTag("Button"))
            {
                scoreManager.PauseScoreCounting = false;
                Time.timeScale = 1;
                // Find all audio sources in the active scene.
                var audioSources = FindAudioSources();
                foreach (var audioSrc in audioSources)
                {
                    audioSrc.mute = false;
                }
                item.SetActive(false);
            }
            else if (item.CompareTag("Button"))
            {
                scoreManager.PauseScoreCounting = true;
                Time.timeScale = 0;
                var audioSources = FindAudioSources();
                foreach (var audioSrc in audioSources)
                {
                    audioSrc.mute = true;
                }
                item.SetActive(true);
            }
        }
    }

    public void ShowMenuItemsDeath()
    {
        foreach (var item in menuItems)
        {
            scoreManager.PauseScoreCounting = true;
            Time.timeScale = 0;
            var audioSources = FindAudioSources();
            foreach (var audioSrc in audioSources)
            {
                audioSrc.mute = true;
            }
            scoreManager.TextScore.enabled = false;
            // Get the Text component from the array.
            Text totalScoreText = menuItems[3].GetComponent<Text>();
            totalScoreText.text = $"Score: {scoreManager.CurrentScore}";
            item.SetActive(true);
        }
    }

    private AudioSource[] FindAudioSources()
    {
        return FindObjectsOfType<AudioSource>();
    }
}