using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public bool PauseScoreCounting { private get; set; } = false;

    [SerializeField]
    private float updateScoreTime = 0.5f;

    private int currentScore;

    private int time;

    private const int divideScoreBy = 10;

    private const float minXVelocity = 3.25f;

    private const string scoreString = "Score: ";

    [SerializeField]
    private Text textScore = default;

    [SerializeField]
    private Rigidbody2D playerRigidbody = default;

    private void Start()
    {
        textScore.text = scoreString;
    }

    private void Update()
    {
        // Update the time.
        time = (int)Time.fixedTime;

        // Update score.
        if (!PauseScoreCounting && playerRigidbody.velocity.x > minXVelocity)
        {
            currentScore += time;

            // Update the UI score string every updateScoreTime.
            if (time % updateScoreTime == 0)
            {
                textScore.text = $"{scoreString} {currentScore / divideScoreBy}";
            }
        }
    }
}
