using UnityEngine;

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

            audioSource.Stop();

            item.SetActive(true);
        }
    }
}
