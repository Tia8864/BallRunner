using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemet : MonoBehaviour
{
    public static Movemet _Instance;
    public float horizontal = 0;
    public float vertical = 0;
    public bool jump;
    private Rigidbody rigi; 
    private float speed;
    private float boundsForce;

    private bool isGround = true;


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigi = transform.parent.GetComponent<Rigidbody>();
        speed = PlayerController._Instance.speed;
    }

    private void FixedUpdate()
    {
        _MakeMove();
    }

    private void _MakeMove()
    {
        if (vertical < 0)
        {
            rigi.AddForce(Vector3.back * speed * Time.fixedDeltaTime);
        }else if(vertical > 0)
        {
            rigi.AddForce(Vector3.forward * speed * Time.fixedDeltaTime);
        }
    }


    void _Jump()
    {
        if(isGround && jump)
        {
            rigi.AddForce(Vector3.up * boundsForce * Time.fixedDeltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _GetInput();
    }

    private void _GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetKeyDown(KeyCode.Space);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGround = true;
        }

        if(collision.gameObject.tag == "wall")
        {
            //death
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = false;
        }
    }
}
