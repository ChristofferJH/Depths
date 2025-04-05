using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] BackGroundObjects;
    public float speed;
    public float max;
    public float offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            BackGroundObjects[i].transform.Translate(speed*Time.deltaTime*Vector3.up);
            if (BackGroundObjects[i].transform.position.y > max)
            {
                BackGroundObjects[i].transform.Translate(-offset*Vector3.up);
            }
        }
    }
}
