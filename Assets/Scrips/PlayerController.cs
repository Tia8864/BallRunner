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
        Vector3 jump = new Vector3(0, 50f, 0);
        _Movement();
        if (isOnGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(jump * boundForce * Time.fixedDeltaTime, ForceMode.Impulse);
                /*if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) return;
                if (Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.W)
                    ) return;*/
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
        if (collision.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
        if(collision.gameObject.tag == "wall")
        {
            //death
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
        if (other.gameObject.CompareTag("target"))
        {
            //goal

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