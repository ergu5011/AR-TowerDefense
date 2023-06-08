using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBone : MonoBehaviour, IDamageable
{
    private GameObject target;

    private float attackRange = 0.07f;
    private float distanceToTarget;

    private float attackCooldown = 2f; // 1.5
    private bool isCooldown;

    private float speed = 0.1f;
    private float damageDealt = 3f;

    private Animator animator;
    private GameObject player;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        player = GameObject.FindGameObjectWithTag("Player");

        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("IsMoving", true);

        transform.LookAt(target.transform);
    }

    void Update()
    {
        // Check distance to target
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        // Checks wether the enemy should move towards or attack the player
        if (distanceToTarget <= attackRange)
        {
            EnemyAttack();
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    // Damage player
    private void EnemyAttack()
    {
        if (!isCooldown)
        {
            animator.SetTrigger("EnemyAttack");
            animator.SetBool("IsMoving", false);

            IDamageable damageable = player.GetComponent<IDamageable>();
            damageable.Damage(damageDealt);

            StartCoroutine(Cooldown());
        }
    }

    // Cooldown for attacking
    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isCooldown = false;
    }

    // Interface for taking damage
    public void Damage(float damageAmount)
    {
        Damaged();
    }

    // Does things when damaged
    private void Damaged()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }

        Destroy(gameObject);
    }
}