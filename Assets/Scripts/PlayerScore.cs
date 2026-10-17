using UnityEngine;
using TMPro; // 1. REQUIRED to control TextMeshPro via code

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public int gameState= 0;

    // 2. This creates a slot in the Inspector where we will link our UI text
    public TextMeshProUGUI scoreText;
    public Canvas loseText;
    public Canvas winText;

    void Start()
    {
        // Set the initial text layout when the game begins
        UpdateScoreUI();
    }

    void Update()
    {
        checkGameState(gameState);
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
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hitting us is tagged as the "Enemy"
        if (other.CompareTag("Enemy"))
        {
            gameState=-1;
        }

        // Check if the object hitting us is tagged as the "Goal"
        if (other.CompareTag("Goal"))
        {
            gameState=1;
        }
    }

    private void checkGameState(int state)
    {
        if (state==0)
        {
            loseText.enabled= false;
            winText.enabled= false;
        }
        
        if (state==-1)
        {
            loseText.enabled= true;
        }

        if (state==1)
        {
            winText.enabled= true;
        }
    }
}