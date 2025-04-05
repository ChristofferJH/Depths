using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public float grabSpeed = 1.5f;

    public static InteractableManager instance;
    public Material outlineMaterial;

    public BaseInteractable activeInteractable;
    public Item heldItem;


    [SerializeField] private Transform holdOverHead;
    [SerializeField] private Transform holdInHand;
    

    public Dictionary<ScriptableItem.HeldType, Transform> HoldingPositions = new Dictionary<ScriptableItem.HeldType, Transform>(3);

    public Transform dropPosition;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
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
    }

    public void ResetCurrentActive()
    {
        BaseInteractable[] objects = FindObjectsOfType<BaseInteractable>();
        Collider playerCollider = PlayerController.instance.GetComponent<Collider>();
        for (int i = 0; i < objects.Length; i++)
        {
            SphereCollider col = objects[i].GetComponent<SphereCollider>();
            if(Vector3.Distance(col.ClosestPoint(playerCollider.transform.position),objects[i].transform.position)<col.radius)
            {
                activeInteractable = objects[i];
                return;
            }
        }
    }
}
