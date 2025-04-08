using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTimeOut : MonoBehaviour
{
    int framesLived = 0;

    void Update()
    {
        transform.localScale = transform.localScale * 0.98f;
        framesLived++;
        if (framesLived > 300)
        {
            Destroy(this.gameObject);
        }
    }
}
