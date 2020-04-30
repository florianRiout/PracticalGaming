using System;
using UnityEngine;

public class Building : MonoBehaviour, IInteractable
{
    bool isOpen = true ;
    Animator animatorParameter;

    private void Start()
    {
        animatorParameter = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public void Interact()
    {
        if (isOpen)
        {
            animatorParameter.ResetTrigger("Open");
            animatorParameter.SetTrigger("Close");
            isOpen = false;
        }
        else
        {
            animatorParameter.ResetTrigger("Close");
            animatorParameter.SetTrigger("Open");
            isOpen = true;
        }
    }
}
