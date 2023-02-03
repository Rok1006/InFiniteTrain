using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Trap", menuName = "ScriptableObject/Trap")]
public class TrapType : ScriptableObject
{
   [SerializeField] private string TrapName;
   [SerializeField] private int damage; //default
   [SerializeField] private float waitTime; //default

   [SerializeField] private List<GameObject> Effects = new List<GameObject>();
   [SerializeField] private List<GameObject> Projectile = new List<GameObject>();
}
