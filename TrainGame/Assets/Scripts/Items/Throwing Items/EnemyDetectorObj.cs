using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorObj : MonoBehaviour
{
    public int enemyLayer;
    public List<EnemyBase> enemies;

    void Start()
    {
        
    }

    
    void Update()
    {
    }

    void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.layer.Equals(enemyLayer)) {
            enemies.Add(collider.GetComponent<EnemyBase>());
        }
    }

    public IEnumerator DetectEnemies(float duration) {
        yield return new WaitForSeconds(duration);
        foreach (EnemyBase enemy in enemies) {
            enemy.Indicator.SetActive(false);
        }
        enemies.Clear();
        gameObject.SetActive(false);
    }
}
