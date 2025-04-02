using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Actions : MonoBehaviour
{
    public enum Object
    {
        None,
        Stick,
        Sword,
        Potion,
        Key
    }

    public bool overObject;
    public bool overChest;
    public bool overDoor;
    public GameObject Item;
    public List<GameObject> nearbyItems = new List<GameObject>();
    public Object actualObject = Object.None;
    [SerializeField] GameObject stick, potion, sword, key;
    int object_;

    [SerializeField] GameObject atack;
    [SerializeField] public int maxHealth;
    public int health;

    [Header("WeaponDamage")]
    [SerializeField] int stickDamage;
    [SerializeField] int swordDamage;
    [Header("Potion")]
    [SerializeField] int potionHealth;
    [SerializeField] GameObject objectSprite;

    GameManager gameManager;

    Animator anim;

    bool atacking;


    void Start()
    {
        object_ = 0;
        health = maxHealth;
        anim = GetComponent<Animator>();
        atacking = false;
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Update()
    {
        objectSprite.GetComponent<ChangeSprite>().SwitchSprite(object_);

        if (Input.GetMouseButtonDown(1))
        {
            if(Item != null && overObject)
            {
                pickUpItem();
            }

            if (overChest)
            {
                OpenChest();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            action();
        }

        if (nearbyItems.Count > 0)
        {
            overObject = true;
            Item = nearbyItems[nearbyItems.Count - 1];
        }
        else
        {
            overObject = false;
            Item = null;
        }
    }

    void pickUpItem()
    {
        if (actualObject != Object.None)
        {
            switch (actualObject)
            {
                case Object.Sword:
                    Instantiate(sword, transform.position, Quaternion.identity);
                    break;
                case Object.Key:
                    Instantiate(key, transform.position, Quaternion.identity);
                    break;
                case Object.Stick:
                    Instantiate(stick, transform.position, Quaternion.identity);
                    break;
                case Object.Potion:
                    Instantiate(potion, transform.position, Quaternion.identity);
                    break;
            }
        }

        switch (Item.tag)
        {
            case "Sword":
                actualObject = Object.Sword;
                object_ = 1;
                break;
            case "Key":
                object_ = 3;
                actualObject = Object.Key;
                break;
            case "Stick":
                object_ = 4;
                actualObject = Object.Stick;
                break;
            case "Potion":
                object_ = 2;
                actualObject = Object.Potion;
                break;
        }

        Destroy(Item);
    }

    void action()
    {
        switch (actualObject)
        {
            case Object.Sword:
                SwordAtack(swordDamage);
                break;
            case Object.Key:
                if(overDoor)
                {
                    OpenDoor();
                }
                break;
            case Object.Stick:
                SwordAtack(stickDamage);
                break;
            case Object.Potion:
                if(health<maxHealth)
                    PotionUse();
                break;
            case Object.None:
                break;
        }
    }

    public void Hit(int damage)
    {
        if (this.gameObject.GetComponent<Player_Movement>().isDashing)
            return;

        health -= damage;
        anim.SetTrigger("damage");

        if(health <= 0)
        {
            this.gameObject.GetComponent<Player_Movement>().dead = true;
            anim.SetTrigger("die");
        }
    }
    void SwordAtack(int damage)
    {
        if (atacking)
            return;

        StartCoroutine(atackDelay(damage));
    }

    void PotionUse()
    {
        StartCoroutine(this.gameObject.GetComponent<Player_Movement>().healing());
        object_ = 0;
        health += potionHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        actualObject = Object.None;
    }

    void OpenChest()
    {
        var chest = GameObject.FindObjectOfType<Chest>();

        if (chest != null)
        {
            
            var chestInteraction = chest.GetComponent<Chest>();
            if (chestInteraction != null)
            {
                chestInteraction.OpenChest();
            }
        }
        overChest = false;
    }

    void OpenDoor()
    {
        var door = GameObject.FindObjectOfType<Door>();

        if (door != null)
        {
            object_ = 0;
            var chestInteraction = door.GetComponent<Door>();
            if (chestInteraction != null)
            {
                chestInteraction.Enter();
            }
        }
        overChest = false;
    }

    private IEnumerator atackDelay(int damage)
    {
        atacking = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -direction);

        GameObject VFXAtack = Instantiate(atack, transform.position, rotation, transform);

        VFXAtack.GetComponent<Sword_Atak>().damage = damage;
        Debug.Log(VFXAtack.GetComponent<Sword_Atak>().damage);

        yield return new WaitForSeconds(0.5f);
        atacking = false;
    }
    void GameOver()
    {
        gameManager.GameOver();
    }
}
