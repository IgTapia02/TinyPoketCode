using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Atak : MonoBehaviour
{

    Collider2D collider_;

    public int damage = 0;
    void Start()
    {
        collider_ = GetComponent<Collider2D>();
        collider_.enabled = false;
    }

    void ActiveCollider()
    {
        collider_.enabled = true;
    }

    void UnActiveCollider()
    {
        collider_.enabled = false;
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            if(collision.gameObject.GetComponent<Enemy_Interactions>() != null)
                collision.gameObject.GetComponent<Enemy_Interactions>().Damage(damage, GetComponentInParent<Transform>().position);

            if (collision.gameObject.GetComponent<Gobling_Interactions>() != null)
                collision.gameObject.GetComponent<Gobling_Interactions>().Damage(damage, GetComponentInParent<Transform>().position);
        }

        if(collision.gameObject.CompareTag("Pot"))
        {
            collision.gameObject.GetComponent<Pot_Interaction>().Destroy();
        }
    }
}
