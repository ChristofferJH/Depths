using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health = 1;
    public int maxHealth;
    public GameObject dropOnDeath;
    public int w = 0;


    public void Start()
    {

        WeightManager.instance.weight += w;
        //grab serialized value and keep it
        maxHealth = health;
    }

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

            WeightManager.instance.weight -= w;
        }

        //wrench repair goes upwards instead, don't let it heal infinite
        if (health > maxHealth)
        {
            health=maxHealth;
        }
    }

}
