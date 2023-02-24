using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _Instance;
    public float speed;
    private Rigidbody rigidbody;
    private float vertical = 0, horizontal = 0, angleRoration = 2f;
    private Vector3 curVector;
    private Vector3 curRotation;
    public float curAngle = 0;
    private bool isOnGround = true;
    private float boundForce;

    public bool flagDeath = false, flagWin = false, flagSpeedUP = false;
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
        flagDeath = false;
        flagWin = false;
        flagSpeedUP = false;
        speed = GameController._Instance.speed;
        boundForce = GameController._Instance.forceJump;
        rigidbody = this.GetComponent<Rigidbody>();
        curVector = new Vector3(0, 0, 1);
        this.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
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
        _Movement();
        _Rotation();
        if (isOnGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(Vector3.up * boundForce, ForceMode.Impulse);
            }
        }
        
    }

    private void _Movement()
    {
        if (vertical > 0)
        {
            rigidbody.AddForce(curVector * speed);
        }
        else if (vertical < 0)
        {
            rigidbody.AddForce( - curVector * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("wall"))
        {
            //death
            Debug.Log("wall is block");
            flagDeath = true;
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
        Debug.Log("collider: " + other.gameObject.name);
        if (other.gameObject.CompareTag("target"))
        {
            flagWin = true;
        }

        if (other.gameObject.CompareTag("speedUp"))
        {
            //x2 speed
            flagSpeedUP = true;
        }
    }
    public void _ResetGame()
    {
        flagDeath = false;
        flagWin = false;
        flagSpeedUP = false;
        curVector = new Vector3(0, 0, 1);
        this.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        curAngle = 0;
        isOnGround = true;
        rigidbody.velocity = Vector3.zero;
    }
}