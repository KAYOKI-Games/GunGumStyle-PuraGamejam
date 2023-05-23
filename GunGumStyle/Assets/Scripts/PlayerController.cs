using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float runSpeed;
    [SerializeField]
    float jumpForce;
    private float nextJumpTime;
    [SerializeField]
    float jumpFrequency;
    Rigidbody2D playerRigid;
    [SerializeField]
    Animator playerAnim;
    bool isGrounded;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    GameObject gum;

    public static bool canDash = true;
    public static bool isDashing;
    [SerializeField] private float dashAmount = 20f;
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float dashCoolDown = 1f;

    [SerializeField] private float lifeTime = 10f;

    public static bool facingRight = true;
    public static float dashVelocity;
  

    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        faceRight();
        checkDash();
        checkJump();
        checkFall();
        gumControl();
        Run();

        
    }

    void checkDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && (Input.GetAxis("Horizontal") != 0))
        {
            StartCoroutine(Dash());

        }
    }

    void checkJump()
    {
        groundcheck();
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && groundcheck() && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            jump();

        }
    }

    void checkFall()
    {
        if (playerRigid.velocity.y < 0)
        {
            playerAnim.SetBool("isFall", true);
        }
        else
            playerAnim.SetBool("isFall", false);
    }

    void Run()
    {
        if (isDashing)
            return;
        float runDirection = Input.GetAxis("Horizontal");
        transform.position = new Vector3(transform.position.x + runDirection * runSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        if (runDirection != 0)
        {
            playerAnim.SetBool("isRunning", true);
            if (runDirection < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
            else
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
        

    }

    void jump()
    {
        if (isDashing)
            return;
        playerRigid.AddForce(new Vector2(0, jumpForce*10000));
    }

    bool groundcheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.1f,groundLayer);
        playerAnim.SetBool("isJumping", !isGrounded);
        return isGrounded;
    }
   
    IEnumerator Dash()
    {
        playerAnim.SetBool("isDashing", true);
        canDash = false;
        isDashing = true;
        playerRigid.gravityScale = 0f;
        dashVelocity = Input.GetAxis("Horizontal");
        playerRigid.velocity = new Vector2(Mathf.Sign(dashVelocity) * dashAmount, 0);
        yield return new WaitForSeconds(dashTime);
        playerAnim.SetBool("isDashing", false);
        isDashing = false;
        playerRigid.gravityScale = 1f;
        playerRigid.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    bool faceRight()
    {

        if (transform.rotation.y == 180)
            facingRight = false;
        else
            facingRight = true;

        return facingRight;
    }

    void gumControl()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(destroyGum());
        }
            

    }

    IEnumerator destroyGum()
    {
        gum.SetActive(true);
        jumpForce = 1.5f;
        yield return new WaitForSeconds(lifeTime);
        gum.SetActive(false);
        jumpForce = 1.2f;
    }


}
