using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWeapon : GameUnit
{
    [HideInInspector]
    public Rigidbody rb;

    public Vector3 enemy;
    float speed;
    public IHit hit;
    float liveTime;

    AttackEnemy attack;
    public override void OnDespawn()
    {
        attack.ResetAttack();
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        hit = null;
        liveTime = 0;
        speed = 500f;
    }

    internal void SetTargetToAttack(AttackEnemy attackEnemy, Vector3 target)
    {
        attack = attackEnemy;
        enemy = target;
    }

    private void Update()
    {
        Vector3 dir = new Vector3(enemy.x, 0, enemy.z) - new Vector3(TF.position.x, 0, TF.position.z);
        rb.velocity = dir * speed * Time.deltaTime;
        transform.forward = dir - Vector3.up * -90f;
        liveTime += Time.deltaTime;
        if (liveTime > 1f)
        {
            OnDespawn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Character character = CacheComponent.GetCharacter(other);
        if (character == attack.character)
        {
            return;
        }
        IHit hit = CacheComponent.GetHit(other);
        if (hit == null)
        {
            return;
        }
        this.hit = hit;
        OnDespawn();
    }
}
