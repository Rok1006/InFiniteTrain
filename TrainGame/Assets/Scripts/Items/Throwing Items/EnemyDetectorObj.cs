using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorObj : MonoBehaviour
{
    public int enemyLayer;
    public List<EnemyBase> enemies;
    private bool isDetecting = false;
    [SerializeField] float duration;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (!isDetecting) {
            StartCoroutine(DetectEnemies(duration));
        }
    }

    void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.layer.Equals(enemyLayer)) {
            enemies.Add(collider.GetComponent<EnemyBase>());
            enemies[enemies.Count-1].Indicator.SetActive(true);
        }
    }

    public IEnumerator DetectEnemies(float duration) {
        isDetecting = true;
        yield return new WaitForSeconds(duration);
        foreach (EnemyBase enemy in enemies) {
            enemy.Indicator.SetActive(false);
        }
        enemies.Clear();
        isDetecting = false;
        gameObject.SetActive(false);
    }
}
