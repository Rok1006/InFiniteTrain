using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public GameObject room;

    
    // Start is called before the first frame update
    void Start()
    {
        Spawn(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(int length)
    {
        var offset = 0;
        for(int i = 0; i < length; i++)
        {
            Instantiate(room, transform.position + new Vector3(0,0,offset), room.transform.rotation);
            offset += 100;
        }

    }
}
