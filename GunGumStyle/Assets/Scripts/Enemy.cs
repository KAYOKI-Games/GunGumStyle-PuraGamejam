using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject acid;
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private float shootingRange = 15f;
    [SerializeField] private float shootingInterval = 1f;
    [SerializeField] private float ultimateShotInterval = 5f;
    [SerializeField] Health health;
    [SerializeField] public Animator animator;
    bool alive = true;
    private Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine(Shoot());
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.Translate(direction.normalized * followSpeed * Time.deltaTime);

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(rb.velocity.magnitude > 0){
            animator.SetBool("isRunning", true);
        }

        

        if (health.currentHealth == 0)
        {
            alive = false;
        }

    }

    private IEnumerator Shoot()
    {
        while(alive == true)
        {
            yield return new WaitForSeconds(shootingInterval);
            if (Vector2.Distance(transform.position, player.position) <= shootingRange)
            {
                if (Random.value < 0.2f) 
                {
                    animator.SetTrigger("Attack");
                    UltimateShot();
                }
                else
                {
                    NormalShot();
                }
            }
        }
       
    }

    private void NormalShot()
    {
    GameObject tempAcid = Instantiate(acid, muzzle.position, Quaternion.identity);
    Rigidbody2D tempRigidbody = tempAcid.GetComponent<Rigidbody2D>();

    float angle = Random.Range(0f, Mathf.PI * 2f);
    float radius = Random.Range(0f, 0.5f); 
    Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

    Vector2 direction = player.position - muzzle.position;
    direction += direction + offset;
    tempRigidbody.AddForce(direction.normalized * 600);
    }

    private void UltimateShot()
    {
        float angleIncrement = 160f / 6f; // Divide 160 degrees by the number of shots (30)
        float startingAngle = -80f; // Starting angle for the shots

        for (int i = 0; i < 6; i++)
        {
            float currentAngle = startingAngle + i * angleIncrement;

            // Calculate the direction vector based on the current angle
            Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * Vector2.up;

            GameObject tempAcid = Instantiate(acid, muzzle.position, Quaternion.identity);
            Rigidbody2D tempRigidbody = tempAcid.GetComponent<Rigidbody2D>();
            tempRigidbody.AddForce(direction.normalized * 600);
        }
    }
}