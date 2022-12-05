using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    IState currentState;
    internal NavMeshAgent agent;
    Vector3 des;
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState());
        attackRange = RandomNum(4f, 8f);
        isPlayer = false;
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
        if (target != GetEnemy())
        {
            if (target != null)
            {
                canAttack = false;
            }
            target = GetEnemy();
            if (target != null)
            {
                canAttack = true;
            }
        }
        if (canAttack && !isDead)
        {
            Attack();
        }
    }
    public void MoveToTarget(Vector3 enemyPos)
    {
        des = enemyPos;
        agent.SetDestination(enemyPos);
    }
    public void ChangeState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public float RandomNum(float min, float max)
    {
        float num = Random.Range(min, max);
        return num;
    }
    public Vector3 RandomPoint()
    {
        float walkRadius = RandomNum(5f, 25f);
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }
    public bool IsTakingTarget()
    {
        return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(des.x, 0, des.z)) < 0.1f;
    }
}
