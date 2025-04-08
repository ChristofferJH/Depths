using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BorisManager : MonoBehaviour
{
    [SerializeField] private Canvas borisCanvas;

    [SerializeField] private RectTransform borisPanel;
    [SerializeField] private TMP_Text borisText;
    float cooldown = 0f;

    bool moveUp = false;


    [SerializeField] Vector3 upPos;
    [SerializeField] Vector3 downPos;


    [SerializeField] float moveSpeed;


    [SerializeField] private Transform tutorialDrop;
    [SerializeField] private List<GameObject> tutorialItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldown < 0)
        {
           
            if (moveUp == true)
            {
                GameStateManager.instance.tutorialStage += 1;
                switch (GameStateManager.instance.tutorialStage)
                {
                    case 1:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("Try smelting this gold ore in the furnace. You pick it up with E and interact with the furnace (grey) with E. [Space to continue]", 2f));
                        GameObject go = Instantiate(tutorialItems[0]);
                        go.transform.position = tutorialDrop.position;
                        break;

                    case 2:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("The Engine took some damage on the last shift. Try reparing it with this wrench. Click to use it. [Space to continue]", 2f));
                        GameObject go1 = Instantiate(tutorialItems[1]);
                        go1.transform.position = tutorialDrop.position;
    
                        break;


                    case 3:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("Watch out! Debris is dropping on the lift! [Space to continue]", 2f));
                        GameStateManager.instance.DropDebris(GameStateManager.Drop.debris);

                        break;

                    case 4:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("If the amount of debris and other things on the lift is higher than the weight limit, the lift will fall down! so be careful! [Space to continue]", 2f));
                        GameStateManager.instance.DropDebris(GameStateManager.Drop.debris);

                        break;

                    case 5:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("Mine the debris with this pickaxe. [Space to continue]", 2f));
                        GameObject go2 = Instantiate(tutorialItems[2]);
                        go2.transform.position = tutorialDrop.position;
                        break;

                    case 6:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("In an emergency, you can get rid of ore in the trash machine (green). But you lose out on the gold [Space to continue]", 2f));
                        break;

                    case 7:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("Be careful! there are toxic slimes down here. Whack them with this bat! They like the heat of the engine for some reason. [Space to continue]", 2f));
                        GameObject go3 = Instantiate(tutorialItems[3]);
                        go3.transform.position = tutorialDrop.position;
                        break;

                    case 8:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("Check out the shoppy shop. It contains upgrades to your gear and the lift [Space to continue]", 2f));
                        
                        break;

                    case 9:
                        GameStateManager.instance.StartCoroutine(GameStateManager.instance.WaitAndDisplayBoris("Good luck buddy. Now, i gotta go call my ex-wife. Talk to you later! [Space to continue]", 2f));
                        GameStateManager.instance.tutorialDone = true;

                        break;
                    default:
                        break;
                }
            }
            moveUp = false;

            //borisCanvas.gameObject.SetActive(false);
        }
        cooldown -= Time.deltaTime;

        if (moveUp)
        {
            borisPanel.transform.position = Vector3.MoveTowards(borisPanel.transform.position, upPos, moveSpeed*Time.deltaTime);
        }
        else {
            borisPanel.transform.position = Vector3.MoveTowards(borisPanel.transform.position, downPos, moveSpeed*Time.deltaTime);
        }
    }

    public void SetBorisCanvas(string text)
    {
        moveUp = true;
        borisCanvas.gameObject.SetActive(true);
        borisText.text = text;
        cooldown = 1f;
        
    }
}
