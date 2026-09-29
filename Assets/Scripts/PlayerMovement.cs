using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ZeroGMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    
    private Rigidbody rb;
    private float moveHorizontal;
    private float moveVertical;
    private float moveUpDown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gather standard WASD / Arrow Key inputs
        moveHorizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        moveVertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down

        // Optional: Use Space to ascend and Left Shift to descend
        if (Input.GetKey(KeyCode.Space)) moveUpDown = 1f;
        else if (Input.GetKey(KeyCode.LeftShift)) moveUpDown = -1f;
        else moveUpDown = 0f;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Calculate directions relative to the player's current orientation
        Vector3 moveDirection = (transform.forward * moveVertical) + 
                                (transform.right * moveHorizontal) + 
                                (transform.up * moveUpDown);

        // Normalize the vector so diagonal movement isn't faster
        moveDirection = moveDirection.normalized;

        // Apply force to move the Rigidbody smoothly
        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
    }
}