using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected WeaponType type;
    Transform tf;
    Rigidbody rb;
    private void Awake()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
    public void AttackEnemy(Transform enemy)
    {
        Vector3 enemyPos = enemy.position;
        rb.velocity = enemyPos - tf.position;
        if(type == WeaponType.Hammer)
        {
            //tf.rotation = Quaternion.LookRotation();
            
        }
        if(type == WeaponType.Boomerang)
        {

        }
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringHelper.CHARACTER_TAG))
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
