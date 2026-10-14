using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 100f;
    public float floatSpeed = 0.5f;
    public float floatHeight = 0.2f;

    private Vector3 startPos;

    void Start()
    {
        // Save the starting position for the floating animation
        startPos = transform.position;
    }

    void Update()
    {
        // 1. Make the item spin beautifully in place
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // 2. Make the item gently float up and down (perfect for underwater or flying items)
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // 3. Unity automatically calls this when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hitting us is tagged as the "Player"
        if (other.CompareTag("Player"))
        {
            OnCollected();
        }
    }

    void OnCollected()
    {
        // Try to find the PlayerScore script on the object that touched the collectible
        PlayerScore player = GameObject.FindWithTag("Player").GetComponent<PlayerScore>();

        if (player != null)
        {
            player.AddScore(1); // Gives the player 1 point
        }

        Destroy(gameObject);
    }
}