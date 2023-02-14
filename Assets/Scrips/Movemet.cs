using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemet : MonoBehaviour
{
    private float horizontal = 0;
    private float vertical = 0;
    private Vector3 dir;
    private Rigidbody rigi; 
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigi = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _MakeMove();
    }

    private void _MakeMove()
    {
        dir = new Vector3(vertical, 0f, -1 * horizontal);
        if(dir.magnitude >= 1)
        {

            rigi.AddForce(dir * speed);
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
    }
}
