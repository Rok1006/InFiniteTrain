using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Projectile : MonoBehaviour
{
    [ReadOnly, BoxGroup("Info")] public GameObject destination;
    [ReadOnly, BoxGroup("Info")] public float timeToTake;
    private float currentTime = 0.0f;
    private bool isReachedDestination = false;

    public Projectile(GameObject destination, float timeToTake) {
        this.destination = destination;
        this.timeToTake = timeToTake;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (currentTime < timeToTake) {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination.transform.position, currentTime / timeToTake);
        } else {
            Debug.Log("reached position");
        }
    }
}
