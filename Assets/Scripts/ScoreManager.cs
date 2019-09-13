using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField]
	private Text textScore = null;

	private int currentScore;

    void Update()
    {
		currentScore += (int)Time.time;

		textScore.text = $"Score: {currentScore}";
	}
}
