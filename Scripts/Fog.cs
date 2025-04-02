using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        OpenFog();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) { OpenFog(); }
    }

    public void OpenFog()
    {
        animator.SetTrigger("Open");
    }

    public void CloseFog()
    {
        animator.SetTrigger("Close");
    }

    
}
