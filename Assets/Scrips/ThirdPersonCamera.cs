using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private Transform player;
    public float distance;
    private float smoothTime = 100f;
    public Vector3 offer;
    // Start is called before the first frame update
    void Start()
    {

        player = Player._Instance.transform;
        transform.position = player.position + offer;
    }
    private void LateUpdate()
    {
        _FollowPlayer();
    }

    private void _FollowPlayer()
    {
        this.transform.position = player.position + (transform.position - player.position).normalized * distance;
        
        Quaternion rotationTarget = Quaternion.LookRotation(transform.position - player.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotationTarget, smoothTime * Time.deltaTime);
    }
}
