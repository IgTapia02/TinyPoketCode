using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    [SerializeField] Player_Actions playerActions;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerActions = FindAnyObjectByType<Player_Actions>();
        ChangeMaxHealth(playerActions.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCurrentHealth(playerActions.health);
    }

    public void ChangeMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(int health)
    {
        slider.value = health;
    }
}
