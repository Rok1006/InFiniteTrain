using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProjectile : MonoBehaviour
{
    [SerializeField] private int speed;
    void Start()
    {
        AutoDestruct();
    }
    void Update()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void AutoDestruct(){
        Destroy(this.gameObject, 5f);
    }
}
