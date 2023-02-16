using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _Instance;

    private float vertical =0, horizontal =0;
    private Rigidbody rigidbody;
    [SerializeField] private float speed = 100, rotationSpeed = 100;
    private float currentRitation = 0;

    public Vector3 curDir;

    private void Awake()
    {
        if(_Instance == null)
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
        rigidbody = transform.parent.GetComponent<Rigidbody>();
        curDir = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (horizontal > 0)
        {
            currentRitation += rotationSpeed * Time.fixedDeltaTime;
        }
        else if (horizontal < 0)
        {
            currentRitation -= rotationSpeed * Time.fixedDeltaTime;
        }

        this.transform.parent.rotation = Quaternion.AngleAxis(currentRitation, Vector3.up);


        if (vertical > 0)
        {
            rigidbody.AddForce(curDir * speed * Time.fixedDeltaTime);
        }
        else if (vertical < 0)
        {
            rigidbody.AddForce(-1 * curDir * speed * Time.fixedDeltaTime);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            //death
        }
        if (collision.gameObject.tag == "speedUP")
        {
            //speed x2
        }
        if (collision.gameObject.tag == "ground")
        {
            //isground = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "speedUP")
        {
            //speed x2
        }
        if (other.tag == "target")
        {
            //end
        }
    }
}
