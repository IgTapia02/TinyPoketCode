using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string playerTag = "Player";

    [SerializeField] GameObject fog;
    [SerializeField] int ID;

    GameObject gameManager_;

    private void Start()
    {
        gameManager_ = FindAnyObjectByType<GameManager>().gameObject;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Player_Actions>().overDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Player_Actions>().overDoor = false;
        }
    }

    public void Enter()
    {
        fog.GetComponent<Fog>().CloseFog();
        StartCoroutine(Sceene());
    }

    private IEnumerator Sceene()
    {

        yield return new WaitForSeconds(2);

        gameManager_.GetComponent<GameManager>().ChangeLevel(ID);
    }

}


