using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeightManager : MonoBehaviour
{
    public static WeightManager instance;
    public TMP_Text weightText;

    private void Awake()
    {
        instance = this;
    }

    public int weight;
    public int maxWeight;


    private void Update()
    {
        if (weight > maxWeight)
        {
            Engine.instance.TakeDamage(Time.deltaTime * 3f);
        }

        weightText.text = "Weight: "+ weight.ToString() + " / " + maxWeight.ToString();
    }

}
