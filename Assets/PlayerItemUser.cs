using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUser : MonoBehaviour
{
    [SerializeField] private AudioSource swoosh;

    public LayerMask hitAble;

    
    public float pickAxeRange = 1.5f;
    public float pickAxeDotProductCone = 0.35f;
    public int pickAxeDamage = 1;

    public void UseItem(ScriptableItem.ItemType itemType)
    {
        //switch blerghh
        switch (itemType)
        {
            case ScriptableItem.ItemType.pickaxe:
                UsePickAxe();
                break;



            default:
                break;
        }
    
    }

    void UsePickAxe()
    {
        swoosh.Play();
        Collider[] targets = Physics.OverlapSphere(transform.position, pickAxeRange, hitAble);
        foreach(Collider c in targets)
        {
            if (Vector3.Dot(transform.forward, Vector3.Normalize(c.transform.position - transform.position)) > pickAxeDotProductCone)
            {
                c.GetComponent<Destructible>().TakeDamage(pickAxeDamage);
            }
        }
    
    }
}
