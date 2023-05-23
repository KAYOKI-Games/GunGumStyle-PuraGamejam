using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    string tagName;
    [SerializeField]
    Rigidbody2D rb;



    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
 

        if (collision.tag == "Ground")
        {

            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (collision.tag == tagName)
        {
            if (tagName == "Player")
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(0.5f);
            }
            else
            {
                collision.GetComponent<Health>().TakeDamage(0.5f);
            }
            Destroy(this.gameObject);
        }

    }
}
