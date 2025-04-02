using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Pot_Interaction : MonoBehaviour
{
    [SerializeField] float dropRate;
    [SerializeField] GameObject potion;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Destroy()
    {
        animator.SetTrigger("Destroy");
    }

    public void Delete()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue <= dropRate)
        {
            Instantiate(potion, transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}

