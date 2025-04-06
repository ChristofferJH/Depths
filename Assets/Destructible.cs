using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health = 1;
    public GameObject dropOnDeath;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            if (dropOnDeath != null)
            {
                GameObject go = Instantiate(dropOnDeath);
                go.transform.position = transform.position;
            }
            Destroy(this.gameObject);
        }
    }

}
