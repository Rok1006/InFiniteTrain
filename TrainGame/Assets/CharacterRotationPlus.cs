using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotationPlus : MonoBehaviour
{
    public float spinspeed;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && isMoving == false)
        {
            //isMoving = true;
            gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
        }

        if (Input.GetKeyDown(KeyCode.D)){
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
