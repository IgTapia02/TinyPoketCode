using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Animator anim;

    Rigidbody2D rb;
    Vector2 movement;
    [SerializeField]
    float moveSpeed;

    [Header("Dash")]
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashDuration;
    [SerializeField]
    float dashCooldown;
    public bool isDashing;
    bool canDash;
    public bool isHealing;
    public bool dead;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        dead = false;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        if (Input.GetAxisRaw("Jump")>0 && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing || isHealing)
            return;
        if (dead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        anim.SetFloat("vely", rb.velocity.y);
        anim.SetFloat("velx", rb.velocity.x);
        anim.SetFloat("InputY", movement.y);
        anim.SetFloat("InputX", movement.x);

        if(movement.x > 0)
        {
            anim.SetBool("Input_Rigth", true);
            anim.SetBool("Input_Left", false);
        }
        else if(movement.x < 0)
        {
            anim.SetBool("Input_Left", true);
            anim.SetBool("Input_Rigth", false);
        }else if(movement.x == 0)
        {
            anim.SetBool("Input_Left", false);
            anim.SetBool("Input_Rigth", false);
        }
        if(movement.y > 0) 
        {
            anim.SetBool("Input_Top", true);
            anim.SetBool("Input_Bottom", false);
        }
        else if (movement.y < 0)
        {
            anim.SetBool("Input_Bottom", true);
            anim.SetBool("Input_Top", false);
        }
        else if(movement.y == 0)
        {
            anim.SetBool("Input_Bottom", false);
            anim.SetBool("Input_Top", false);
        }

    }

    private IEnumerator Dash()
    {
        canDash = false;
        anim.SetTrigger("Dash");
        isDashing = true;
        if(Input.GetAxisRaw("Vertical")==0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.velocity = new Vector2(.65f * dashSpeed, .37f * dashSpeed);
        }
        else
        {
            rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        }

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public IEnumerator healing()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("curar");
        isHealing = true;
        
        yield return new WaitForSeconds(1);
        isHealing = false;

    }
}
