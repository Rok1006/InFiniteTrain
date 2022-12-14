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
            var offset = new Vector3(Random.Range(-30.0f,30.0f), 5.0f, Random.Range(-10.0f,10.0f));
            int ranNum = Random.Range(0, 10); 
            if (ranNum <= 5)
                Instantiate(proGenManager.EnemyList[0], base.Ground.transform.position + offset, Quaternion.identity);
            else
                Instantiate(proGenManager.EnemyList[1], base.Ground.transform.position + offset, Quaternion.identity);
        }
    }
}
