using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
   

    [SerializeField]
    GameObject button;

    void EnAnimation()
    {
        button.SetActive(true);        
    }

}
