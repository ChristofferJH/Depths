using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] BackGroundObjects;
    public List<GameObject> BackGroundObjectsButDelete;
    public float speed;
    public float max;
    public float offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BackGroundObjects.Length; i++)
        {
            BackGroundObjects[i].transform.Translate(speed*Time.deltaTime*Vector3.up);
            if (BackGroundObjects[i].transform.position.y > max)
            {
                BackGroundObjects[i].transform.Translate(-offset*Vector3.up);
            }
        }


        for (int i = 0; i < BackGroundObjectsButDelete.Count; i++)
        {
            BackGroundObjectsButDelete[i].transform.Translate(speed * Time.deltaTime * Vector3.up);
            if (BackGroundObjectsButDelete[i].transform.position.y > 100f)
            {
                Destroy(BackGroundObjectsButDelete[i]);
                BackGroundObjectsButDelete.RemoveAt(i);
            }
        }
    }
}
