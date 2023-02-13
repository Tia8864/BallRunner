using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public ThirdPersonMovement _Instance;
    private Rigidbody rigid;
    [SerializeField] private Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    private float turnSmoothValecity;
    private float horizontal = 0;
    private float vertical = 0;

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
    }
    private void Start()
    {
        rigid = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _Move();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void _Move()
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f) //magnitude = do dai vector
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothValecity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            this.rigid.AddForce(moveDir.normalized * speed * Time.fixedDeltaTime);
            //rigid.MovePosition(this.transform.position + direction * speed * Time.deltaTime);
        }
    }
}   //Scource in: https://www.youtube.com/watch?v=4HpC--2iowE
