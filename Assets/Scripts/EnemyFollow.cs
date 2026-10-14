using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;

    public float swimForce = 10f;
    public float turnSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate() // Use FixedUpdate for Rigidbody physics
    {
        if (player == null) return;

        // 1. Calculate direction to player
        Vector3 direction = (player.position - transform.position).normalized;

        // 2. Smoothly rotate the Rigidbody forward vector towards the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));

        // 3. Push the fish forward in the direction it is facing
        rb.AddForce(transform.forward * swimForce);
    }
}