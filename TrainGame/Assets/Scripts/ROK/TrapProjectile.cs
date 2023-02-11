using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProjectile : MonoBehaviour
{
    [HideInInspector]public GameObject _player;
    [HideInInspector]public float _damage;
    [HideInInspector]public float _speed;
    void Start()
    {
        AutoDestruct();
    }
    void Update()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    void AutoDestruct(){
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(_player!=null){
               // Debug.Log("injured");
                _player.GetComponent<PlayerInformation>().CurrentRadiationValue += _damage;
            }
        }
    }
}
