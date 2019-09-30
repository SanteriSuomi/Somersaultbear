using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager = default;
    [SerializeField]
    private GameObject[] menuItems = default;
    private AudioSource[] audioSources;

    private void Start()
    {
        #if UNITY_EDITOR
        Assert.IsNotNull(scoreManager);
        Assert.IsNotNull(menuItems);
        #endif
    }

    private void Update()
    {
        // Continuously scan and wait for the user input "ESC".
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
                // Score counting control.
                scoreManager.PauseScoreCounting = false;
                // Game freeze control.
                Time.timeScale = 1;
                // Find all the active audiosources in the scene and mute them.
                var audioSources = FindAudioSources();
                foreach (var audioSrc in audioSources)
                {
                    audioSrc.mute = false;
                }
                // Show/hide menu buttons.
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
            // Find all the active audiosources in the scene.
            var audioSources = FindAudioSources();
            // Then iterate over the array and mute all audiosources.
            foreach (var audioSrc in audioSources)
            {
                audioSrc.mute = true;
            }
            // Disable/enable the UI score text.
            scoreManager.TextScore.enabled = false;
            // Get the Text component from the array.
            Text totalScoreText = menuItems[3].GetComponent<Text>();
            // Update the score to represent the current score.
            totalScoreText.text = $"Score: {scoreManager.CurrentScore}";
            // Show all menu items.
            item.SetActive(true);
        }
    }

    private AudioSource[] FindAudioSources()
    {
        return FindObjectsOfType<AudioSource>();
    }
}