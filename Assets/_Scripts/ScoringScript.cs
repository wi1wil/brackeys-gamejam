using UnityEngine;
using TMPro;

public class ScoringScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;

    public void AddScore(int value)
    {
        playerScore += value;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score: {playerScore}";
    }
}
