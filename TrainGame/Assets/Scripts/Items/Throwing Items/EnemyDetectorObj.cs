using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorObj : MonoBehaviour
{
    public LayerMask enemyLayer;
    public List<Transform> enemyPositions;
    [SerializeField] private GameObject enemyDetectorArrow;
    private List<GameObject> arrows = new List<GameObject>();
    private PlayerManager player;
    [SerializeField] private GameObject worldUICanvas;
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    
    void Update()
    {
        for (int i = 0; i < enemyPositions.Count; i++) {
            arrows[i].transform.LookAt(enemyPositions[i]);
        }
    }

    void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.layer.Equals(enemyLayer)) {
            enemyPositions.Add(collider.transform);
            arrows.Add(Instantiate(enemyDetectorArrow, player.transform.position, Quaternion.identity));
            arrows[arrows.Count-1].transform.SetParent(worldUICanvas.transform);
        }
    }

    public IEnumerator DetectEnemies(float duration) {
        yield return new WaitForSeconds(duration);
        enemyPositions.Clear();
        Destroy(arrows[arrows.Count-1]);
        arrows.RemoveAt(arrows.Count-1);
        gameObject.SetActive(false);
    }
}
