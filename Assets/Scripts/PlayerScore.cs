using UnityEngine;
using TMPro; // 1. REQUIRED to control TextMeshPro via code

public class PlayerScore : MonoBehaviour
{
    public int score = 0;

    // 2. This creates a slot in the Inspector where we will link our UI text
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Set the initial text layout when the game begins
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // 3. A dedicated method that updates the visual text component
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text reference is missing on the Player! Drag your ScoreText UI object into the slot.");
        }
    }
}