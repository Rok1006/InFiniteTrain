using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbtLights : MonoBehaviour
{
    [SerializeField] List<GameObject> lights = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnLights(){
        for(int i = 0; i < lights.Count; i++){
            lights[i].SetActive(true);
        }
    }
    public void OffLights(){
        for(int i = 0; i < lights.Count; i++){
            lights[i].SetActive(false);
        }
    }
}
