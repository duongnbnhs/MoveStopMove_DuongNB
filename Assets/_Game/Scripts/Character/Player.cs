using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    DynamicJoystick joystick;
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    LayerMask layerMask;
    Vector3 moveDir;
    protected override void Update()
    {
        base.Update();
        Moving();
    }
    void Moving()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            moveDir = new Vector3(joystick.Horizontal * moveSpeed, rigidbody.velocity.y, joystick.Vertical * moveSpeed);
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 4f, layerMask))
            {
                Debug.DrawLine(transform.position, hit.point, Color.blue);
                Vector3 hitPoint = hit.point;
                Vector3 nextPoint = transform.position + moveDir * Time.deltaTime * moveSpeed;
                transform.position = new Vector3(nextPoint.x, hitPoint.y, nextPoint.z);
            }*/
            rigidbody.velocity = moveDir;
            characterVisualize.transform.rotation = Quaternion.LookRotation(moveDir);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
