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

    //private float lastAttackTime = 0f;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //if (distanceToPlayer <= attackRange)
        //{
        //    // Attack the player if cooldown is ready
        //    if (Time.time >= lastAttackTime + attackCooldown)
        //    {
        //        AttackPlayer();
        //        lastAttackTime = Time.time;
        //    }
        //}
        if (distanceToPlayer <= detectionRange)
        {
            // Chase player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * enemySpeed * Time.deltaTime, Space.World);
        }
        else
        {
            // Default movement: always move left
            transform.Translate(Vector3.left * enemySpeed * Time.deltaTime, Space.World);
        }
    }

    //void AttackPlayer()
    //{
    //    Debug.Log("Enemy attacks the player!");
    //    // Example: player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            Destroy(gameObject);
            //LAAG GAME OVER IGDI
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
