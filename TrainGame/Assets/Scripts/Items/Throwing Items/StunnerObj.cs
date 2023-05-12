using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnerObj : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.layer.Equals(enemyLayer)) {
            EnemyBase enemyBase;
            collider.TryGetComponent<EnemyBase>(out enemyBase);
            if (enemyBase != null) {
                enemyBase.state = EnemyBase.State.STUN;
            } else {
                Debug.Log("cant find enemy base in " + collider.gameObject.name);
            }
        }
    }
}
