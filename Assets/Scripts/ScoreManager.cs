using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public bool PauseScoreCounting { get; set; } = false;

	private int currentScore;

	private const int divideScoreBy = 10;

	private const float minXVelocity = 0.1f;

	[SerializeField]
	private Text textScore = default;

	private Rigidbody2D playerRigidbody = default;

	private void Start()
	{
		playerRigidbody = GameObject.Find("PRE_Player").GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		// Update score when not pausing the score and player's velocity is more than X amount in the positive X.
		if (!PauseScoreCounting && playerRigidbody.velocity.x > minXVelocity)
		{
			// Add time to the currentScore and cast it in integer because Time.time returns float and we don't want decimals.
			currentScore += (int)Time.time;

			// Update the UI score display and divide it by specified amount for smaller numbers.
			textScore.text = $"Score: {currentScore / divideScoreBy}";
		}
	}
}
