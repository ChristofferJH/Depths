using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : BaseInteractable
{
    public Transform parentTransform;
    public ScriptableItem scrItem;
    private Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if (!canBeUsed) { return; }
        if (InteractableManager.instance.activeInteractable == null)
        {
            InteractableManager.instance.activeInteractable = this;
        }
        List<Material> mats = new List<Material>(new []{ rend.material, InteractableManager.instance.outlineMaterial });
        rend.SetMaterials(mats);
    }
    private void OnTriggerExit(Collider other)
    {
        if (InteractableManager.instance.activeInteractable == this)
        {
            InteractableManager.instance.activeInteractable = null;
        }
        List<Material> mats = new List<Material>(new[] { rend.material});
        rend.SetMaterials(mats);
    }

    public override void Interact()
    {
        
        GameObject go = Instantiate(scrItem.ItemPrefab);
        go.transform.SetPositionAndRotation(transform.position, transform.rotation);
        InteractableManager.instance.heldItem = go.GetComponent<Item>();
        InteractableManager.instance.activeInteractable = null;
        InteractableManager.instance.ResetCurrentActive();
        Destroy(transform.parent.gameObject);
    }



    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        
    }


}
