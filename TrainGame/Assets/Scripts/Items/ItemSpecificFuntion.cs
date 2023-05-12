using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
//attach this scrip on items that has specific ffunction 
public class ItemSpecificFuntion : MonoBehaviour
{
    [ReadOnly] public PlayerManager Player;
    public enum Item { NONE, EnemyDetector }
    public Item currentItem = Item.NONE;
    public LayerMask enemyLayer;

    void Start()
    {
        switch(currentItem){
            case Item.EnemyDetector:
                //arrows appear within given time
            break;
        }

        Invoke("FindPlayerReference", 0.1f);
        // Player = FindObjectOfType<PlayerManager>();
    }

    public void FindPlayerReference() {
        Player = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        Physics.SphereCastAll(Player.transform.position, 5.0f, Vector3.up, 10.0f, layerMask:enemyLayer);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Debug.DrawLine(Player.transform.position, Player.transform.position + Vector3.up * 5.0f);
        Gizmos.DrawWireSphere(Player.transform.position, 10.0f);
    }
}
