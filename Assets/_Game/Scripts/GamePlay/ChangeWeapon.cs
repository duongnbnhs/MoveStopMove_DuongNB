using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : Singleton<ChangeWeapon>
{
    public ListWeaponData weaponData;
    public void ChangeCharacterWeapon(Character character, WeaponType weaponType)
    {
        character.weaponType = weaponType;
        character.poolType = (PoolType)(int)weaponType;
        if (character.weaponPrefabs != null)
        {
            Destroy(character.weaponPrefabs);
        }
        character.weaponPrefabs = Instantiate(weaponData.weapons[(int)weaponType].weapon, character.handWeaponPos);
    }
}
