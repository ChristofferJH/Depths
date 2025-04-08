using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private CharacterController cc;
    public Animator anim;
    
    [SerializeField] private Vector3 speed;
    private Rigidbody rb;
    public float acc = 2.0f;
    public float deacc = 5.0f;
    public float maxSpeed = 5f;
    private KeyCode[] movementKeys = { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S };
    private Vector3[] movementVectors = { Vector3.right, Vector3.forward, Vector3.left, -Vector3.forward };

    [SerializeField] private Transform graphicsObject;

    [SerializeField] private LayerMask groundMouseHitLayer;

    public PlayerItemUser playerItemUser;
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
        playerItemUser = GetComponent<PlayerItemUser>();
    }
   
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMouseHitLayer))
        {
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0f;
            if (lookDirection.sqrMagnitude>0.001f)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
                
            }
        }

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


        anim.SetFloat("Speed",speed.magnitude/maxSpeed);
        rb.velocity = new Vector3(0f,rb.velocity.y,0f)+speed;
        //cc.Move(speed * Time.deltaTime*100f);

       

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
