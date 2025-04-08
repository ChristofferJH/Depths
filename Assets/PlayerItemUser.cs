using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUser : MonoBehaviour
{
    public static PlayerItemUser instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private AudioSource swoosh;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private ParticleSystem hitEffect;


    public LayerMask mineAble;
    public LayerMask repairAble;
    public LayerMask hitAble;

    public float weaponRange = 1.6f;
    public float weaponDotProductCone = 0.3f;


    public int pickAxeDamage = 1;
    public int batDamage = 1;
    public int wrenchDamage = -2;

    public void UseItem(ScriptableItem.ItemType itemType)
    {
        //switch blerghh
        switch (itemType)
        {
            case ScriptableItem.ItemType.pickaxe:
                UseWeapon(itemType);
                break;

            case ScriptableItem.ItemType.bat:
                UseWeapon(itemType);
                break;

            case ScriptableItem.ItemType.wrench:
                UseWeapon(itemType);
                break;

                //other item types than weapons go here...

            default:
                break;
        }
    
    }

    void UseWeapon(ScriptableItem.ItemType itemType)
    {
        PlayerController.instance.anim.SetTrigger("Attack");
        StartCoroutine(DelayedAttack(itemType));
        swoosh.Play();
    
    }

    IEnumerator DelayedAttack(ScriptableItem.ItemType itemType)
    {
        yield return new WaitForSeconds(0.16f);
        //swoosh.Play();
        bool hit = false;
        switch (itemType)
        {
            case ScriptableItem.ItemType.pickaxe:
                Collider[] targetsP = Physics.OverlapSphere(transform.position, weaponRange, mineAble);
                foreach (Collider c in targetsP)
                {
                    if (Vector3.Dot(transform.forward, Vector3.Normalize(c.transform.position - transform.position)) > weaponDotProductCone)
                    {

                        hit = true;
                        c.GetComponent<Destructible>().TakeDamage(pickAxeDamage);
                    }
                }
                break;

            case ScriptableItem.ItemType.wrench:
                Collider[] targetsW = Physics.OverlapSphere(transform.position, weaponRange, repairAble);
                foreach (Collider c in targetsW)
                {
                    if (Vector3.Dot(transform.forward, Vector3.Normalize(c.transform.position - transform.position)) > weaponDotProductCone)
                    {

                        hit = true;
                        Destructible d= c.GetComponent<Destructible>();
                        if (d is Engine)
                        {
                            c.GetComponent<Engine>().TakeDamage(wrenchDamage);
                        }
                        else {
                            d.TakeDamage(wrenchDamage);
                        }
                    }
                }
                break;

            case ScriptableItem.ItemType.bat:
                Collider[] targetsB = Physics.OverlapSphere(transform.position, weaponRange, hitAble);
                foreach (Collider c in targetsB)
                {
                    if (Vector3.Dot(transform.forward, Vector3.Normalize(c.transform.position - transform.position)) > weaponDotProductCone)
                    {

                        hit = true;
                        c.GetComponent<Destructible>().TakeDamage(batDamage);
                    }
                }
                break;

            default:
                break;
        }


        if (hit)
        {
            hitSound.pitch = Random.Range(0.95f, 1.05f);
            hitSound.Play();
            hitEffect.Play();
        }

        //Collider[] targets = Physics.OverlapSphere(transform.position, weaponRange, hitAble);
        //foreach (Collider c in targets)
        //{
        //    if (Vector3.Dot(transform.forward, Vector3.Normalize(c.transform.position - transform.position)) > weaponDotProductCone)
        //    {
                

        //        c.GetComponent<Destructible>().TakeDamage(pickAxeDamage);
        //    }
        //}
        yield return null;
    }

}
