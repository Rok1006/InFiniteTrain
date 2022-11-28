using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private CharacterHandleWeapon handleWeapon;
    [SerializeField] private Weapon smallBlade;
    void Start()
    {
        handleWeapon = GetComponent<CharacterHandleWeapon>();
    }

    void Update()
    {
        
    }
}
