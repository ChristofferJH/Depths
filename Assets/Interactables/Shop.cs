using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : BaseInteractable
{

    [SerializeField] private GameObject shopCanvas;

    public override void Start()
    {
        base.Start();

    }


    public override void Interact()
    {

        Time.timeScale = 0.0f;
        shopCanvas.SetActive(true);
    }

}
