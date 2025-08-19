using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float enemySpeed = 5f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;   // distance at which enemy attacks
    public float attackCooldown = 1f;  // time between attacks

    private float lastAttackTime = 0f;
    private Animator animator;
    private bool isAttacking = false; //  flag to lock movement while attacking

    private void Start()
    {
        animator = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        

        if (isAttacking)
        {
            //  Enemy is attacking - do not move
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            //  Attack
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackRoutine());
                lastAttackTime = Time.time;
            }

            // Face the player
            FaceTarget(player.position);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Chase player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * enemySpeed * Time.deltaTime;

            FaceTarget(player.position);
            animator.SetBool("isMoving", true);
        }
        else
        {
            //  Default movement: always move left
            Vector3 direction = Vector3.left;
            transform.position += direction * enemySpeed * Time.deltaTime;

            FaceTarget(transform.position + direction);
            animator.SetBool("isMoving", true);
        }
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // stop movement animation
        animator.SetBool("isMoving", false);

        // play attack animation
        animator.SetTrigger("Attack");

        Debug.Log("Enemy attacks the player!");

        // wait until animation is done (adjust 0.8f to your attack animation length)
        yield return new WaitForSeconds(0.8f);

        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            Destroy(gameObject);
            // TODO: Add Game Over logic here
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
