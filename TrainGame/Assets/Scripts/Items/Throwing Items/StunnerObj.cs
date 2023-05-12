using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnerObj : MonoBehaviour
{
    [SerializeField] private int enemyLayer;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider collider) {
        Debug.Log("triggered entered " + collider.gameObject.name + "\nlayer + " + collider.gameObject.layer + " compare " + enemyLayer);
        if (collider.gameObject.layer.Equals(enemyLayer)) {
            Debug.Log("Enemy entered");
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
