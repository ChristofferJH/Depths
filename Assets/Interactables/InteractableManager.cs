using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public float grabSpeed = 1.5f;
    public float useItemCooldown = -1f;


    public static InteractableManager instance;
    public Material outlineMaterial;

    public List<BaseInteractable> interactables = new List<BaseInteractable>();
    public BaseInteractable activeInteractable;
    public Item heldItem;


    [SerializeField] private Transform holdOverHead;
    [SerializeField] private Transform holdInHand;
    

    public Dictionary<ScriptableItem.HeldType, Transform> HoldingPositions = new Dictionary<ScriptableItem.HeldType, Transform>(3);

    public Transform dropPosition;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else {
            Destroy(gameObject);
        }

        HoldingPositions[ScriptableItem.HeldType.overHead] = holdOverHead;
        HoldingPositions[ScriptableItem.HeldType.rightHand] = holdInHand;

        Debug.Log(HoldingPositions[ScriptableItem.HeldType.overHead]);
    }

    void Update()
    {
        float minDist = 100f;
        int index = -1;
        Vector3 playerPos = PlayerController.instance.transform.position;
        for(int i=0;i<  interactables.Count;i++)
        {
            if (interactables[i].canBeUsed == false)
            {
                continue;
            }
            float dist = Vector3.Distance(interactables[i].transform.position, playerPos);
            if (dist < 1.8f)
            {
                if (dist < minDist)
                {
                    index = i;
                    minDist = dist;
                }
            }
        }
        if (index == -1)
        {
            activeInteractable = null;
        }
        else {
            activeInteractable = interactables[index];
            Graphics.DrawMesh(activeInteractable.mf.mesh, activeInteractable.transform.localToWorldMatrix, outlineMaterial, 0);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                if (activeInteractable != null)
                {
                    activeInteractable.Interact();
                }
            }
            else
            {
                if (activeInteractable != null)
                {
                    if (activeInteractable is Processor)
                    {
                        activeInteractable.Interact();
                        return;
                    }
                }

                heldItem.Interact();
            }
            
            
        }

        useItemCooldown -= Time.deltaTime;

        if (heldItem != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (useItemCooldown < 0f)
                {
                    PlayerController.instance.playerItemUser.UseItem(heldItem.scrItem.itemType);
                    useItemCooldown = 1f;
                }
            }
        }
    }

    //public void ResetCurrentActive()
    //{
    //    BaseInteractable[] objects = FindObjectsOfType<BaseInteractable>();
    //    Collider playerCollider = PlayerController.instance.GetComponent<Collider>();
    //    for (int i = 0; i < objects.Length; i++)
    //    {
    //        SphereCollider col = objects[i].GetComponent<SphereCollider>();
    //        if(Vector3.Distance(col.ClosestPoint(playerCollider.transform.position),objects[i].transform.position)<col.radius)
    //        {
    //            activeInteractable = objects[i];
    //            return;
    //        }
    //    }
    //}
}
