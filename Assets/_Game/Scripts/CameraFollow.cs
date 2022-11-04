using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        //tim vi tri cua player
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //di chuyen camera
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);
    }
}
