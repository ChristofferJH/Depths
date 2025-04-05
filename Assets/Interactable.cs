using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform parentTransform;
    MeshRenderer rend;
    private void OnTriggerEnter(Collider other)
    {
        InteractableManager.instance.activeInteractables.Add(this);
        rend.material.color = Color.green;
    }
    private void OnTriggerExit(Collider other)
    {
        InteractableManager.instance.activeInteractables.Remove(this);
        rend.material.color = Color.red;
    }


    void Start()
    {
        rend = GetComponentInParent<MeshRenderer>();
    }

    void Update()
    {
        
    }
}
