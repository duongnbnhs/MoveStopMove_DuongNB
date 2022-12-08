using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    JoystickControl joystick;
    [SerializeField]
    Transform detectTarget;
    Vector3 moveDir;

    protected override void Start()
    {
        base.Start();
        isPlayer = true;
    }
    protected void Update()
    {
        ChangeCharactWeapon();
        if (!isDead)
        {
            if (isMoving)
            {
                ChangeAnim(StringHelper.ANIM_RUN);
            }
            if (canAttack && !isMoving)
            {
                Attack();
            }
            if (!canAttack && !isMoving)
            {
                ChangeAnim(StringHelper.ANIM_IDLE);
            }
            Move();
            if (target != GetEnemy())
            {
                if (target != null)
                {
                    detectTarget.gameObject.SetActive(false);
                    canAttack = false;
                }
                target = GetEnemy();
                if (target != null)
                {
                    detectTarget.SetParent(target);
                    detectTarget.localPosition = new Vector3(0, 0.501f, 0);
                    detectTarget.gameObject.SetActive(true);
                    canAttack = true;
                }
            }
        }
    }
    protected void Move()
    {
        if (Input.GetMouseButton(0))
        {
            moveDir = JoystickControl.direct * moveSpeed + rb.velocity.y * Vector3.up;
            rb.velocity = moveDir;
            characterVisualize.rotation = Quaternion.LookRotation(moveDir);
            isMoving = true;
        }
        else
        {
            rb.velocity = Vector3.zero;
            isMoving = false;
        }
    }
}
