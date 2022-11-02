using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ProGenManager : MonoBehaviour
{
    [SerializeField] private GameObject trainCart;
    private List<Room> trainCarts = new List<Room>();
    [SerializeField] private int trainLength; 
    void Start()
    {
        SpawnTrainCarts(trainLength);
    }

    
    void Update()
    {
        
    }

    public void SpawnTrainCarts(int length) {
        for (int i = 0; i < length; i++) {
            GameObject cart = Instantiate(trainCart, new Vector3(0,i*100,0), Quaternion.identity);
            trainCarts.Add(cart.GetComponentInChildren<Room>());
            trainCarts[trainCarts.Count-1].gameObject.AddComponent<EnemyCart>();
        }
    }
}
