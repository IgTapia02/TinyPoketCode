using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobling_Atack : MonoBehaviour
{
    BoxCollider2D collider_;

    public int damage = 0;

    void Start()
    {
        collider_ = GetComponent<BoxCollider2D>();
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

    void OnDrawGizmos()
    {
        if (collider_ == null)
        {
            collider_ = GetComponent<BoxCollider2D>();
        }

        if (collider_ != null && collider_.enabled)
        {
            Vector2 size = collider_.size;
            Vector2 offset = collider_.offset;
            Vector2 position = (Vector2)transform.position + offset;

            Vector2 topLeft = position + new Vector2(-size.x, size.y) * 0.5f;
            Vector2 topRight = position + new Vector2(size.x, size.y) * 0.5f;
            Vector2 bottomLeft = position + new Vector2(-size.x, -size.y) * 0.5f;
            Vector2 bottomRight = position + new Vector2(size.x, -size.y) * 0.5f;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}
