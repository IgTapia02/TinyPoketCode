using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Prop : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerActions = collision.gameObject.GetComponent<Player_Actions>();
            if (!playerActions.nearbyItems.Contains(this.gameObject))
            {
                playerActions.nearbyItems.Add(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerActions = collision.gameObject.GetComponent<Player_Actions>();
            if (playerActions.nearbyItems.Contains(this.gameObject))
            {
                playerActions.nearbyItems.Remove(this.gameObject);
            }
        }
    }

}
