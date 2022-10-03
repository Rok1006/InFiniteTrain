using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is aming to making camera taget DOESN'T follow character's y pos, so the cinimachine doesn't have glitch
  when player turn the character 180 degree*/
public class CameraTargetAdjustment : MonoBehaviour
{
    private Transform cameraTarget;
    private bool isYRotRestricted = true;
    void Start()
    {
        if (transform.Find("CameraTarget") != null) {
            cameraTarget = transform.Find("CameraTarget").transform;
        } else
            Debug.Log("can't find camera target gameobject in " + name);
    }

    
    void Update()
    {
        if (isYRotRestricted)
            cameraTarget.localRotation = Quaternion.Euler(cameraTarget.localRotation.x, -180, cameraTarget.localRotation.z);
    }
}
