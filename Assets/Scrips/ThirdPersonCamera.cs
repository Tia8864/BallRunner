using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    private float distance, min, max;
    [SerializeField]
    private Vector3 offset;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        
        transform.position += offset;
    }
    private void LateUpdate()
    {
        _FollowPlayer();
    }

    private void _FollowPlayer()
    {

        Vector3 target = Vector3.zero ;

        if(Physics.Linecast(PlayerController._Instance.transform.position, transform.position, out hit))
        {
            distance = hit.point.magnitude;
        }
        else
        {
            distance = offset.magnitude;
        }
        distance = Mathf.Clamp(this.transform.rotation.z, min, max);
        target = new Vector3(
            distance * Mathf.Cos(PlayerController._Instance.curAngle * Mathf.Deg2Rad - Mathf.PI / 2f),
            offset.y,
            distance * Mathf.Sin(PlayerController._Instance.curAngle * Mathf.Deg2Rad - Mathf.PI / 2f)
        );
        this.transform.position = Vector3.Lerp(
            this.transform.position,
            PlayerController._Instance.transform.position + target, 
            Time.deltaTime);
        transform.LookAt(PlayerController._Instance.transform.position);
    }
}
