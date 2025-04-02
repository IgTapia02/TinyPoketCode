using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAtack : MonoBehaviour
{

    Collider2D collider_;

    public int damage = 0;
    void Start()
    {
        collider_ = GetComponent<Collider2D>();
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
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Golpe");
            collision.gameObject.GetComponent<Player_Actions>().Hit(damage);
        }

    }
}

