using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public bool PauseScoreCounting { private get; set; } = false;
    public int CurrentScore { get; set; }

    [SerializeField]
    private Text textScore = default;
    public Text TextScore
    {
        get { return textScore; }
        set { textScore.enabled = false; }
    }
    [SerializeField]
    private Rigidbody2D playerRigidbody = default;

    private const float MIN_X_VELOCITY = 3;
    private const int UPDATE_SCORE_TIME = 10;
    private const string SCORE_STRING = "Score: ";

    private void FixedUpdate()
    {
        if (!PauseScoreCounting && playerRigidbody.velocity.x > MIN_X_VELOCITY)
        {
            // Update the score.
            CurrentScore += 1;    
            // Update the UI score string every updateScoreTime.
            if (CurrentScore % UPDATE_SCORE_TIME == 0)
            {
                // Update the score text.
                textScore.text = $"{SCORE_STRING} {CurrentScore}";
            }
        }
    }
}
