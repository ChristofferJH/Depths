using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{

    public static InteractableManager instance;

    public List<Interactable> activeInteractables = new List<Interactable>();

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (activeInteractables.Count > 0)
            {
                activeInteractables[0].parentTransform.parent = PlayerController.instance.transform;
            }
        }
    }
}
