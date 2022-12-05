using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] 
    GameObject HandWeapon; 
    [SerializeField]
    Transform tf;

    private MovingWeapon wp;

    public Character character;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        tf = transform;
        HandWeapon.SetActive(true);
    }

    public void Attack(Vector3 target)
    {
        if (HandWeapon.activeInHierarchy)
        {
            HandWeapon.SetActive(false);

            wp = SimplePool.Spawn<MovingWeapon>(character.poolType, tf.position, tf.rotation);
            wp.SetTargetToAttack(this, target);
            wp.OnInit();
        }
    }

    public void ResetAttack()
    {
        HandWeapon.SetActive(true);
        if (wp.hit != null)
        {
            wp.hit.OnHit(character);
        }
    }

}
