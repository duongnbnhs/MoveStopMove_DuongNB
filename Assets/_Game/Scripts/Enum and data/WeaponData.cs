using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData 
{
    public WeaponType type;
    public Material material;
    public Mesh mesh;
}
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableData/Weapon", order = 1)]
public class ListWeaponData : ScriptableObject
{
    public List<WeaponData> weapons;
}
