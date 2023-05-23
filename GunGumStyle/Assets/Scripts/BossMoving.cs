using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float runSpeed;
    Rigidbody2D bossRigid;
    [SerializeField]
    Animator bossAnim;
    bool isBoundries;
    bool isGrounded;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayer;
    void Start()
    {
        bossRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {

        // Check if the boss is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Calculate the horizontal movement direction
        float runDirection = isGrounded ? 1 : 0; // Boss moves when grounded, otherwise stops

        // Move the boss horizontally
        bossRigid.velocity = new Vector2(runDirection * runSpeed, bossRigid.velocity.y);

        // Update the boss's animation and rotation
        if (runDirection != 0)
        {
            bossAnim.SetBool("isRunning", true);
            if (runDirection < 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
        }
        else
        {
            bossAnim.SetBool("isRunning", false);
        }
    } 

        bool groundcheck()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
            bossAnim.SetBool("isJumping", !isGrounded);
            return isGrounded;
        }
    
}

