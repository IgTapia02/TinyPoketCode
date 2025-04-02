using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Prop : MonoBehaviour
{

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        var playerActions = collision.gameObject.GetComponent<Player_Actions>();
    //        playerActions.overObject = true;
    //        playerActions.Item = this.gameObject;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        var playerActions = collision.gameObject.GetComponent<Player_Actions>();
    //        // Solo actualiza si el item actual es este objeto
    //        if (playerActions.Item == this.gameObject)
    //        {
    //            playerActions.overObject = false;
    //            playerActions.Item = null;
    //        }
    //    }
    //}

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
