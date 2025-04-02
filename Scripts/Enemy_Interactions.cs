using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Interactions : MonoBehaviour
{

    [SerializeField]
    int maxHealt;

    float pushForce = 2f;
    Rigidbody2D rb;

    public int healt;

    bool hited = false;
    Animator animator;

    public bool death;

    [SerializeField] float atackOffset;
    [SerializeField] GameObject _atack;
    void Start()
    {
        death = false;
        healt = maxHealt;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Damage(int damage, Vector2 playerPosition)
    {
        if (death)
            return;

        animator.SetTrigger("Damage");
        
        if (healt > 0 && !hited)
        {
            healt -= damage;
        }

        if(healt <= 0)
        {
            death = true;
            animator.SetTrigger("Death");
        }
        else
        {
            Vector2 pushDirection = (transform.position - (Vector3)playerPosition).normalized;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            Debug.Log("force");
            StartCoroutine(CknocBack());
            StartCoroutine(HitedTimer());
        }
    }

    private IEnumerator HitedTimer()
    {
        hited = true;
        yield return new WaitForSeconds (0.7f);
        hited = false;
    }
    private IEnumerator CknocBack()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Atack()
    {
        animator.SetTrigger("Atack");     
    }

    public void InsntantiateAtack()
    {
        Instantiate(_atack, transform.position * (1 + atackOffset), transform.rotation, transform);
    }
}
