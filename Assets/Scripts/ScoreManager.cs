using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField]
	private Text textScore = null;

	private int currentScore;

	public bool gameIsPaused = false;

    void Update()
    {
		if (!gameIsPaused)
		{
			currentScore += (int)Time.time;

			textScore.text = $"Score: {currentScore / 10}";
		}
	}
}
