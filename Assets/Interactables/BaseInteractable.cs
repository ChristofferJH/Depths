using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    public MeshRenderer rend;
    private SphereCollider col;
    public MeshFilter mf;
    public bool canBeUsed = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null) { return; }

        if (InteractableManager.instance.activeInteractable == null)
        {
            InteractableManager.instance.activeInteractable = this;
        }
        List<Material> mats = new List<Material>(new[] { rend.material, InteractableManager.instance.outlineMaterial });
        rend.SetMaterials(mats);
    }
    private void OnTriggerExit(Collider other)
    {
        if (InteractableManager.instance.activeInteractable == this)
        {
            InteractableManager.instance.activeInteractable = null;
        }
        List<Material> mats = new List<Material>(new[] { rend.material });
        rend.SetMaterials(mats);
    }

    public void DelayBeingUseable()
    {
        canBeUsed = false;
        StartCoroutine(WaitDelayTimer());
    }

    IEnumerator WaitDelayTimer()
    {
        yield return new WaitForSeconds(1.0f);
        canBeUsed = true;

        //if (InteractableManager.instance.activeInteractable == null)
        //{
        //    if (Vector3.Distance(col.ClosestPoint(PlayerController.instance.transform.position), transform.position) < col.radius)
        //    {
        //        InteractableManager.instance.activeInteractable = this;

        //    }
        //}
        yield return null;
    }

    public virtual void Start()
    {
        col = GetComponent<SphereCollider>();
        rend = GetComponent<MeshRenderer>();
        mf = GetComponent<MeshFilter>();
        InteractableManager.instance.interactables.Add(this);
    }

    public virtual void OnDestroy()
    {
        InteractableManager.instance.interactables.Remove(this);
    }

    public virtual void Interact()
    {

    }
}
