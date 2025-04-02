using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{

    [SerializeField] Sprite none,stick, sword, potion, key;
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }
    
    public void SwitchSprite(int num)
    {
        switch (num)
        {
            case 0:

                image.sprite = none;
                image.enabled = false;
                break;

            case 1:

                image.sprite = sword;
                image.enabled = true;
                break;

            case 2:

                image.sprite = potion;
                image.enabled = true;
                break;

            case 3:

                image.sprite = key;
                image.enabled = true;
                break;

            case 4:

                image.sprite = stick;
                image.enabled = true;
                break;
        }
    }
}
