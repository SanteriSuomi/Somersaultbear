using System.Text;
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

    [SerializeField]
    private float updateScoreTime = 0.5f;

    private int time;

    private const int divideScoreBy = 10;

    private const float minXVelocity = 3;

    private const string scoreString = "Score: ";

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
            CurrentScore += time;

            // Update the UI score string every updateScoreTime.
            if (time % updateScoreTime == 0)
            {
                textScore.text = $"{scoreString} {CurrentScore / divideScoreBy}";
            }
        }
    }
}
