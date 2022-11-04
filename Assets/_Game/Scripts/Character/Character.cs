using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    Animator anim;
    [SerializeField]
    List<Transform> allEnemiesInLevel;
    [SerializeField]
    List<Transform> enemiesInRange;
    [SerializeField]
    protected Transform enemy;
    [SerializeField]
    protected Transform characterVisualize;
    [SerializeField]
    protected GameObject weapon;
    [SerializeField]
    protected Transform weaponOnHand;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected Transform throwPos;
    [SerializeField]
    List<float> enemiesInRangeDistance;
    [SerializeField]
    Level level;
    [SerializeField]
    protected GameObject bullet;
    protected GameObject flyingWeapon;
    protected bool isMoving;
    protected bool canAttack;
    protected string currentAnimName;
    protected float attackTime;
    protected bool isAttackEnemy;
    protected Transform tf;
    private void Awake()
    {
        tf = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        moveSpeed = 3f;
        attackRange = 4f;
        attackTime = 0;
        weapon = Instantiate(weapon, weapon.transform.position, weapon.transform.rotation);
        weapon.SetActive(true);
        weapon.transform.SetParent(weaponOnHand);
        allEnemiesInLevel = level.allCharactersInLevel;
        isAttackEnemy = false;
    }
    protected virtual void Update()
    {
        GetAllEnemiesInRange();
        canAttack = IsEnemyInRange();
        if (isMoving)
        {
            ChangeAnim(StringHelper.ANIM_RUN);
            isAttackEnemy = false;
        }
        if (canAttack && !isMoving)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                characterVisualize.rotation = Quaternion.LookRotation(enemy.position - tf.position);
                attackTime += Time.deltaTime;
                ChangeAnim(StringHelper.ANIM_ATTACK);
                if (attackTime > 0.5f)
                {
                    attackTime = 0;
                    InstantiateWeapon();
                    weapon.SetActive(false);
                    isAttackEnemy = true;
                }
            }
        }
        if (!isMoving)
        {
            ChangeAnim(StringHelper.ANIM_IDLE);
        }


        //PlayAnim();
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    protected void GetAllEnemiesInRange()
    {
        enemiesInRange.Clear();
        enemiesInRangeDistance.Clear();
        foreach (var cEnemy in allEnemiesInLevel)
        {
            if (cEnemy.gameObject.activeInHierarchy)
            {
                var dis = Vector3.Distance(tf.position, cEnemy.position);
                if (dis < attackRange)
                {
                    enemiesInRange.Add(cEnemy);
                    enemiesInRangeDistance.Add(dis);
                }
            }
        }
        if (enemiesInRange.Count > 0)
        {
            ChooseTarget();
        }

    }
    protected void ChooseTarget()
    {
        var index = enemiesInRangeDistance.IndexOf(enemiesInRangeDistance.Min());
        enemy = enemiesInRange[index];

    }

    protected bool IsEnemyInRange()
    {
        if (enemy != null)
        {
            return true;
        }
        else return false;
    }
    protected void InstantiateWeapon()
    {
        flyingWeapon = Instantiate(weapon);
        flyingWeapon.transform.position = throwPos.position;
        flyingWeapon.SetActive(true);
        ThrowWeapon();
    }
    protected void ThrowWeapon()
    {
        Vector3 enemyPos = enemy.position;
        var flyRid = flyingWeapon.AddComponent(typeof(Rigidbody)) as Rigidbody;

        flyRid.useGravity = false;
        flyRid.velocity = enemyPos - flyingWeapon.transform.position;
        var coll = flyingWeapon.AddComponent<CapsuleCollider>() as CapsuleCollider;
        coll.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringHelper.WEAPON_TAG))
        {
            OnDespawn();
        }
    }
    protected void ChangeAnim(string animName)
    {
        /*if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }*/
        anim.ResetTrigger(animName);
        anim.SetTrigger(animName);
    }
}
