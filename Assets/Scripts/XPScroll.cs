using UnityEngine;
using System.Collections;

public class XPScroll : MonoBehaviour, IItem, IInteractable
{
    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Pick();
    }

    public void Pick()
    {
        GameManager.Inventory.Add(this);
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
