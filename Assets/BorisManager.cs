using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorisManager : MonoBehaviour
{
    [SerializeField] private Canvas borisCanvas;
    
    [SerializeField] private Text borisText;
    float cooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldown < 0)
        {
            borisCanvas.gameObject.SetActive(false);
        }
        cooldown -= Time.deltaTime;
    }

    void SetBorisCanvas(string text)
    {
        borisCanvas.gameObject.SetActive(true);
        borisText.text = text;
        cooldown = 1f;
    
    }
}
