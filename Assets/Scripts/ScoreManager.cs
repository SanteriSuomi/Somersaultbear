using UnityEngine;
using UnityEngine.Assertions;
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

    private const int DIVIDE_SCORE_BY = 20;
    private const float MIN_X_VELOCITY = 3;
    private const string SCORE_STRING = "Score: ";

    private void Start()
    {
        #if UNITY_EDITOR
        Assert.IsNotNull(textScore);
        Assert.IsNotNull(playerRigidbody);
        #endif

        textScore.text = SCORE_STRING;
    }

    private void Update()
    {
        // Update the time and round the time to integer continuously.
        time = Mathf.RoundToInt(Time.fixedTime);
        // Update score using the time value.
        if (!PauseScoreCounting && playerRigidbody.velocity.x > MIN_X_VELOCITY)
        {
            CurrentScore += time;
            // Update the UI score string every updateScoreTime.
            if (time % updateScoreTime == 0)
            {
                textScore.text = $"{SCORE_STRING} {CurrentScore / DIVIDE_SCORE_BY}";
            }
        }
    }
}
