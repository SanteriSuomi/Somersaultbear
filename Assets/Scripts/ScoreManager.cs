using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField]
	private Text textScore = default;

	private Rigidbody2D playerRigidbody = default;

	private int currentScore;

	public bool gameIsPaused { get; set; } = false;

	void Start()
	{
		playerRigidbody = GameObject.Find("PRE_Player").GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		if (!gameIsPaused && playerRigidbody.velocity.x > 0.1f)
		{
			currentScore += (int)Time.time;

			textScore.text = $"Score: {currentScore / 10}";
		}
	}
}
