using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : BaseInteractable
{
    //public Transform parentTransform;
    public ScriptableItem scrItem;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!canBeUsed) { return; }
    //    if (InteractableManager.instance.activeInteractable == null)
    //    {
    //        InteractableManager.instance.activeInteractable = this;
    //    }
    //    List<Material> mats = new List<Material>(new []{ rend.material, InteractableManager.instance.outlineMaterial });
    //    rend.SetMaterials(mats);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (InteractableManager.instance.activeInteractable == this)
    //    {
    //        InteractableManager.instance.activeInteractable = null;
    //    }
    //    List<Material> mats = new List<Material>(new[] { rend.material});
    //    rend.SetMaterials(mats);
    //}

    public override void Interact()
    {

        if (scrItem.itemType == ScriptableItem.ItemType.gold)
        {
            GameStateManager.instance.gold += 1;
            GameStateManager.instance.PlayGoldSound();
            Destroy(transform.gameObject);
            return;
        }

        GameObject go = Instantiate(scrItem.ItemPrefab);
        go.transform.SetPositionAndRotation(transform.position, transform.rotation);
        InteractableManager.instance.heldItem = go.GetComponent<Item>();
        InteractableManager.instance.activeInteractable = null;
        //InteractableManager.instance.ResetCurrentActive();

        if (scrItem.heldType == ScriptableItem.HeldType.overHead)
        {
            PlayerController.instance.anim.SetBool("Hold", true);
        }
        Destroy(transform.gameObject);
    }



    public override void Start()
    {

        WeightManager.instance.weight += 1;
        base.Start();
        
    }

    public override void OnDestroy()
    {
        
        WeightManager.instance.weight -= 1;
        base.OnDestroy();
    }


}
