using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Level1 : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D enemyRigid;
    Animator enemyAnim;
    [SerializeField]
    float speed;
    Transform target;
    [SerializeField]
    float rangeX,rangeY;
    float damage = 0.5f;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        enemyRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
         moveEnemy();
        
    }

    void moveEnemy()
    {
        Vector3 Distance = target.position - transform.position;
        if (Distance.x < rangeX && Distance.y < rangeY)
        {
            enemyRigid.velocity = new Vector2(speed * transform.localScale.x, 0);
        }
        
    }
    void changeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ForEnemy")
        {
            changeDirection();
        }
        else if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

}
