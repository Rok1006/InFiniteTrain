using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCart : Cart
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        SpawnEnemies(3);
    }

    void Update()
    {
        
    }

    public void SpawnEnemies(int enemyCount) {
        
        for (int i = 0; i < enemyCount; i++) {
            var offset = new Vector3(Random.Range(-10.0f,10.0f), 5.0f, Random.Range(-30.0f,30.0f));
            Instantiate(proGenManager.Enemy, base.Ground.transform.position + offset, Quaternion.Euler(0,-90,0));
        }
    }
}
