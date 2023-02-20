using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = new Vector3(0, 0, rotateSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(rotation);
    }
}
