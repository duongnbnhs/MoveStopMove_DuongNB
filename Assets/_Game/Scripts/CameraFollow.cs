using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 2;

    // Update is called once per frame
    void LateUpdate()
    {
        //di chuyen camera
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);
    }
}
