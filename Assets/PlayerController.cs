using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private CharacterController cc;


    [SerializeField] private Vector3 speed;
    private Rigidbody rb;
    [SerializeField] private float acc = 2.0f;
    [SerializeField] private float deacc = 5.0f;
    [SerializeField] private float maxSpeed = 10f;
    private KeyCode[] movementKeys = { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S };
    private Vector3[] movementVectors = { Vector3.right, Vector3.forward, Vector3.left, -Vector3.forward };

    [SerializeField] private Transform graphicsObject;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }
   
    void Update()
    {
        
        Vector3 movement = Vector3.zero;
        bool move = false;
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(movementKeys[i]))
            {
                movement += movementVectors[i];
                move = true;
            }
        }

        if (move)
        {
            speed = Vector3.ClampMagnitude(speed + movement * acc * Time.deltaTime, maxSpeed);
        }
        else
        {
            speed = Vector3.MoveTowards(speed, Vector3.zero, deacc * Time.deltaTime);
        }

        //cc.Move(movement * acc * Time.deltaTime);

        cc.Move(speed * Time.deltaTime);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if (Vector3.Dot(hit.normal, hit.moveDirection) < 0.0f)
        //{ speed = Vector3.zero; }
    }

    //void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + speed * acc * Time.deltaTime);
    //}

}
