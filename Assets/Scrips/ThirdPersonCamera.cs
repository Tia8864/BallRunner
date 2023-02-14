using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public static ThirdPersonCamera _Instance;

    private Transform player;
    public float distance;
    private float smoothTime = 0.25f;
    private Vector3 currentVelocity;
    Vector3 offer = new Vector3(0, 5, -2);

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(36f, 90f, 0);
        player = PlayerController._Instance.transform;
    }



    private void LateUpdate()
    {
        Vector3 target = player.position + (transform.position - player.position).normalized * distance;
        if (target.y < 10)
        {
            target.y = 10;
        }

        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
        transform.LookAt(player);
    }

    private void _Rotation()
    {
        float horizontal = Movemet._Instance.horizontal;

        if(horizontal > 0)
        {

        }
    }
}
