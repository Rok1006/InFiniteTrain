using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

[CreateAssetMenu(fileName = "Weapon Collection", menuName = "Weapon/Weapon Collection", order = 1)]
public class WeaponCollection : ScriptableObject
{
    [SerializeField] private List<Weapon> meleeWeapons, rangeWeapons;
    
    //getters & setters
    public List<Weapon> MeleeWeapons {get=>meleeWeapons;}
    public List<Weapon> RangeWeapons {get=>rangeWeapons;}
    
    public Weapon getWeaponByName(string name) {
        foreach(Weapon weapon in MeleeWeapons)
            if (weapon.WeaponName.ToLower().Trim().Equals(name.ToLower().Trim()))
                return weapon;

        foreach(Weapon weapon in RangeWeapons)
            if (weapon.WeaponName.ToLower().Trim().Equals(name.ToLower().Trim()))
                return weapon;
        
        return null;
    }
}
