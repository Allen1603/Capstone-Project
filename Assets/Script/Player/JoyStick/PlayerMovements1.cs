using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Animator animator;

    // 🎮 Reference to Joystick
    public Joystick joystick; // assign in Inspector

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("isMoving", false);
        }
    }

    void HandleMovement()
    {
        // Read joystick input
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 velocity = move * moveSpeed;
        Vector3 currentVelocity = rb.velocity;
        rb.velocity = new Vector3(velocity.x, currentVelocity.y, velocity.z);

        // Rotate player to face direction
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }

        // Play walk animation
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);
    }
}
