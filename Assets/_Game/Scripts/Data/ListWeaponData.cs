using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataWeapon
{
    public WeaponType type;
    public GameObject weapon;
    //public PoolType type;
    /*public Material material;
    public Mesh mesh;*/
}
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableData/Weapon", order = 1)]
public class ListWeaponData : ScriptableObject
{
    public List<DataWeapon> weapons;
}
