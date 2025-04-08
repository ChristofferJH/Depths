using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.SetDestination(Engine.instance.transform.position);
        Engine.instance.enemies.Add(this);
    }

    private void OnDestroy()
    {
        Engine.instance.enemies.Remove(this);

    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        agent.enabled = true;
        agent.SetDestination(Engine.instance.transform.position);
    }

    void Update()
    {
        
    }


}
