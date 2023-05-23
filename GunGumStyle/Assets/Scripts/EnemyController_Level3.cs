using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController_Level3 : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D enemyRigid;
    BoxCollider2D boxCollider2D;
    Health health;
    Animator enemyAnim;
    Transform target;
    bool alive = true;

    [SerializeField] private float shootingRange = 15f;
    [SerializeField] private float shootingInterval = 1f;

    [SerializeField] float speed;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject acid;
    [SerializeField] float rangeX, rangeY;


    void Start()
    {
        StartCoroutine(Shoot());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigid = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        health = GetComponent<Health>();
        boxCollider2D= GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        alive = health.isAlive;
        moveEnemy();
        checkFall();

        if (enemyRigid.velocity.magnitude > 0)
        {
            enemyAnim.SetBool("isRunning", true);
        }

        if(alive == false && boxCollider2D.enabled == true)
        {
            boxCollider2D.enabled = false;
            enemyAnim.SetTrigger("Die");
        }
    }

    void moveEnemy()
    {
        Vector3 Distance = target.position - transform.position;
        if (Distance.x < rangeX && Distance.y < rangeY && checkFall() == false)
        {
            enemyAnim.SetBool("isRunning", true);
            enemyRigid.velocity = new Vector2(speed * transform.localScale.x, 0);
        }
        else
        {
            enemyAnim.SetBool("isRunning", false);
        }
    }

    private IEnumerator Shoot()
    {
        while (alive == true)
        {
            yield return new WaitForSeconds(shootingInterval);
            if (Vector2.Distance(transform.position, target.position) <= shootingRange)
            {
                NormalShot();
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

        Vector2 direction = target.position - muzzle.position;
        direction += direction + offset;
        tempRigidbody.AddForce(direction.normalized * 600);
    }
    bool checkFall()
    {
        if (enemyRigid.velocity.y < 0)
        {
            enemyAnim.SetBool("isFalling", true);
            return true;
        }
        else
            enemyAnim.SetBool("isFalling", false);
        return false;  
    }
    void changeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ForEnemy")
        {
            changeDirection();
        }
    }
}
