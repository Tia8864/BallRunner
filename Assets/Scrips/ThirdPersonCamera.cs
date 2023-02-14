using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private Transform player;
    private Vector3 offset = new Vector3(-23, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(36f, 90f, 0);
        player = PlayerController._Instance.transform;
    }

    private void LateUpdate()
    {
        Vector3 target = new Vector3(player.position.x, 0, player.position.z) + offset;
        transform.position = target;
    }
}
