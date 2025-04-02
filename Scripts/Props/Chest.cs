using UnityEngine;

public class Chest : MonoBehaviour
{
    bool isOpen;
    public string playerTag = "Player"; // Etiqueta del jugador
    Animator animator;
    [SerializeField] GameObject key;
    Collider2D collider_;
    [SerializeField] Transform spawnKeyPoint;


    void Start()
    {
        animator = GetComponent<Animator>();   
        collider_ = GetComponent<Collider2D>();
        isOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Player_Actions>().overChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Player_Actions>().overChest = false;

            if(isOpen) 
            { 
                CloseChest(); 
            }
        }
    }

    public void OpenChest()
    {
        animator.SetTrigger("Open");
        isOpen = true;
    }

    public void KeyDrop()
    {
        Instantiate(key, spawnKeyPoint.position , Quaternion.identity, transform);
    }

    void CloseChest()
    {
        animator.SetTrigger("Close");
        collider_.enabled = false;
    }
}

