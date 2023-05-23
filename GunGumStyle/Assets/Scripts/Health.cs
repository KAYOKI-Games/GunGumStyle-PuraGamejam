using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 3;
    public float currentHealth;
    public Animator animator;
    [SerializeField]
    BoxCollider2D boxCollider;
    [SerializeField]
    Rigidbody2D rigidbody2D;
    Enemy enemy;
    public bool isAlive = true;

    private void Start()
    {
        currentHealth = startingHealth;
        enemy= GetComponent<Enemy>();
    }

    public void TakeDamage(float damageAmount)
    {
        animator.SetTrigger("Damage");

        currentHealth -= damageAmount;
        if (currentHealth < 0) { 

            isAlive = false;
            currentHealth = 0;
            Debug.Log("Object has died");
            animator.SetTrigger("Die");
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            boxCollider.enabled = false;
            enemy.enabled = false;
            isAlive = false;
        }
    }
}
