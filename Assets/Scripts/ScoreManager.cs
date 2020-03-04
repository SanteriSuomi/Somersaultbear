using UnityEngine;
using UnityEngine.UI;

namespace Somersaultbear
{
    public class ScoreManager : MonoBehaviour
    {
        public bool PauseScoreCounting { get; set; }
        public int CurrentScore { get; set; }

        [SerializeField]
        private Text textScore = default;
        public Text TextScore => textScore;

        [SerializeField]
        private Rigidbody2D playerRigidbody = default;
        [SerializeField]
        private float minVelocityForScoring = 3;
        [SerializeField]
        private int updateScoreRate = 10;
        [SerializeField]
        private string scoreString = "Score: ";

        private void FixedUpdate() => UpdateScore();

        private void UpdateScore()
        {
            if (!PauseScoreCounting && playerRigidbody.velocity.x > minVelocityForScoring)
            {
                CurrentScore += 1;
                if (CurrentScore % updateScoreRate == 0)
                {
                    textScore.text = $"{scoreString} {CurrentScore}";
                }
            }
        }
    }
}