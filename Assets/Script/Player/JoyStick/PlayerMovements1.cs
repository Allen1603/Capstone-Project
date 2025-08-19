using UnityEngine;
using UnityEngine.UI; // needed for Button

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Animator animator;

    // 🎮 References
    public Joystick joystick;   // assign in Inspector
    public Button attackButton; // assign in Inspector

    private bool isAttacking;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Add listener for attack button
        if (attackButton != null)
            attackButton.onClick.AddListener(DoAttack);
    }

    void FixedUpdate()
    {
        if (canMove && !isAttacking) // prevent moving while attacking (optional)
        {
            HandleMovement();
        }
        else if (!isAttacking)
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

    void DoAttack()
    {
        if (!isAttacking) // prevent spamming
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        isAttacking = true;
         animator.SetTrigger("Attack"); // trigger attack animation

        Debug.Log("Nag Attackkkkkkkk!!!");
        rb.velocity = Vector3.zero;    // stop moving during attack

        yield return new WaitForSeconds(0.5f); // duration of attack animation

        isAttacking = false;
    }
}
