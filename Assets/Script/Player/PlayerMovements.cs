using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private PlayerControls inputActions;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        // Convert 2D input (x,y) into 3D world movement (x, 0, y)
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;

        // Apply velocity while keeping gravity
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
