using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _Instance;
    [SerializeField]
    private float speed;
    private Rigidbody rigidbody;
    private float vertical = 0, horizontal = 0, angleRoration = 2f;
    private Vector3 curVector;
    private Vector3 curRotation;
    public float curAngle = 0;
    private bool isOnGround = true;
    [SerializeField]
    private float boundForce;
    private float ySpeed;
    private float timeBuff = 2f;


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        curVector = new Vector3(0, 0, 1);
        this.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        _Rotation();


    }

    private void _Rotation()
    {
        if (horizontal > 0)
        {
            curAngle += angleRoration;
        }
        else if (horizontal < 0)
        {
            curAngle -= angleRoration;
        }

        if (horizontal != 0)
        {
            curRotation = new Vector3(0, -curAngle, 0);
            this.transform.rotation = Quaternion.Euler(curRotation);
            curVector = new Vector3(
                Mathf.Cos(curAngle * Mathf.Deg2Rad + Mathf.PI / 2),
                0,
                Mathf.Sin(curAngle * Mathf.Deg2Rad + Mathf.PI / 2));
        }
    }

    private void FixedUpdate()
    {
        Vector3 jump = new Vector3(0, 20f, 0);
        _Movement();
        ySpeed = 10* Physics.gravity.y * Time.deltaTime;
        if (isOnGround)
        {
            ySpeed = -.5f;
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(jump * boundForce * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }
        
    }

    private void _Movement()
    {
        if (vertical > 0)
        {
            //rigidbody.AddForce(curVector * speed * Time.fixedDeltaTime);
            rigidbody.velocity = curVector * speed * Time.fixedDeltaTime;
        }
        else if (vertical < 0)
        {
            //rigidbody.AddForce( - curVector * speed * Time.fixedDeltaTime);
            rigidbody.velocity = -1 * curVector * speed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision :" + collision.gameObject.name);
        if (collision.gameObject.tag == "ground")
        {
            isOnGround = true;
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
            isOnGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("target"))
        {
            //goat
        }

        if (other.gameObject.CompareTag("speedUp"))
        {
            //x2 speed
        }
    }
}