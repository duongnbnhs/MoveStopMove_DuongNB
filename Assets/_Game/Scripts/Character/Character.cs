using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHit
{
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Transform characterVisualize;
    //[SerializeField]
    public Transform handWeaponPos;
    [SerializeField]
    protected AttackEnemy attack;
    [SerializeField]
    internal Transform target;
    [SerializeField]
    internal Transform rangeCircle;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected LayerMask layer;

    internal Transform tf;
    protected Rigidbody rb;
    protected string currentAnimName;
    protected WeaponType curentWeaponType;
    protected bool isPlayer;

    public bool isMoving;
    public bool canAttack;
    public bool isDead;
    public WeaponType weaponType;

    public PoolType poolType;
    public GameObject weaponPrefabs;
    public int score;

    float atkTime;

    protected virtual void Start()
    {
        OnInit();
    }
    protected virtual void OnInit()
    {
        tf = transform;
        rb = GetComponent<Rigidbody>();
        isDead = false;
        canAttack = false;
        isMoving = false;
        target = null;
        curentWeaponType = weaponType;
        ChangeWeapon.Ins.ChangeCharacterWeapon(this, weaponType);
        score = 0;
    }
    protected virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }
    internal void Attack()
    {
        tf.rotation = Quaternion.LookRotation(target.position - tf.position);
        ChangeAnim(StringHelper.ANIM_ATTACK);
        //attack.Attack(target.position);
        atkTime += Time.deltaTime;
        if (atkTime > 0.3f)
        {
            atkTime = 0;
            attack.Attack(target.position);
        }
    }
    internal void ChangeCharactWeapon()
    {
        if (curentWeaponType != weaponType)
        {
            curentWeaponType = weaponType;
            ChangeWeapon.Ins.ChangeCharacterWeapon(this, weaponType);
        }
    }
    internal Transform GetEnemy()
    {
        Transform target = null;
        Collider[] colliders = Physics.OverlapSphere(tf.position, attackRange, layer);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.activeInHierarchy)
            {
                Transform trans = collider.transform;
                Character charac = collider.GetComponent<Character>();
                if (trans == tf || charac.isDead) continue;
                if (target == null)
                {
                    target = trans;
                }
                else
                {
                    if (Vector3.Distance(tf.position, trans.position) < Vector3.Distance(tf.position, target.position))
                    {
                        target = trans;
                    }
                }
            }

        }
        return target;
    }
    internal void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    void ScaleUp()
    {
        attackRange *= 1.5f;
        rangeCircle.localScale = new Vector3(rangeCircle.localScale.x *1.5f, rangeCircle.localScale.y * 1.5f, rangeCircle.localScale.z * 1.5f);
    }
    public void OnHit(Character character)
    {
        if (!isPlayer)
        {
            SpawnBot.Ins.botAlive--;
        }
        isDead = true;
        ChangeAnim(StringHelper.ANIM_DEAD);
        Invoke(nameof(OnDespawn), 2f);
    }
}
