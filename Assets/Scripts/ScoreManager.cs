using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	// A property for pausing the score counting when player presses "Cancel" (escape) for the menu.
	public bool pauseScoreCounting { get; set; } = false;

	private int currentScore;

	private const int divideScoreBy = 10;

	private const float minXVelocity = 0.1f;

	[SerializeField]
	private Text textScore = default;

	private Rigidbody2D playerRigidbody = default;

	void Start()
	{
		playerRigidbody = GameObject.Find("PRE_Player").GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// Update score when not pausing the score and player's velocity is more than X amount in the positive X.
		if (!pauseScoreCounting && playerRigidbody.velocity.x > minXVelocity)
		{
			// Add time to the current score and cast it with int because Time.time returns float normally and we don't want decimals.
			currentScore += (int)Time.time;

			// Update the UI score display.
			textScore.text = $"Score: {currentScore / divideScoreBy}";
		}
	}
}
