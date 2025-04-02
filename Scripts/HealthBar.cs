using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] Player_Actions playerActions;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerActions = FindAnyObjectByType<Player_Actions>();
        ChangeMaxHealth(playerActions.maxHealth);
    }

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
