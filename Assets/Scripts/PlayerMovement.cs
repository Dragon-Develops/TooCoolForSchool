using UnityEngine;
using UnityEngine.InputSystem;

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
        // Check for active keyboard device
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        // WASD / Arrow key inputs using New Input System controls
        moveHorizontal = 0f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveHorizontal += 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveHorizontal -= 1f;

        moveVertical = 0f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveVertical += 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveVertical -= 1f;

        // Space to ascend, Left Shift to descend
        if (keyboard.spaceKey.isPressed) moveUpDown = 1f;
        else if (keyboard.leftShiftKey.isPressed) moveUpDown = -1f;
        else moveUpDown = 0f;
        
        //Attempt at 3rd person pov camera
        float mouseX = Input.GetAxis("Mouse X");

        x += 1 * mouseX;

        Quaternion angle = new Quaternion();
        angle.eulerAngles = new Vector3(0, transform.rotation.y + x, 0);
        transform.rotation = angle;


        Camera.main.transform.LookAt(transform);
        Camera.main.transform.Translate(Vector3.right * mouseX * Time.deltaTime * 2.8f);//the 2.8f is the sensibility
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