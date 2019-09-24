using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager = default;

    [SerializeField]
    private AudioSource audioSource = default;

    [SerializeField]
    private GameObject[] menuItems = default;

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
                // Score counting control.
                scoreManager.PauseScoreCounting = false;

                // Game freeze control.
                Time.timeScale = 1;

                // Game music control.
                audioSource.Play();

                // Show/hide menu buttons.
                item.SetActive(false);
            }
            else if (item.CompareTag("Button"))
            {
                scoreManager.PauseScoreCounting = true;

                Time.timeScale = 0;

                audioSource.Stop();

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

            // Stop game music.
            audioSource.Stop();

            // Disable/enable the UI score text.
            scoreManager.TextScore.enabled = false;

            // Get the Text component from the array.
            var totalScoreText = menuItems[3].GetComponent<Text>();

            // Update the score to represent the current score.
            totalScoreText.text = $"Score: {scoreManager.CurrentScore}";

            // Show all menu items.
            item.SetActive(true);
        }
    }
}
