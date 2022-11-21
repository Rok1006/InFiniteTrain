using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* general functions that we might need across multiple scripts */
public static class GeneralFunctions
{
    public static GameObject FindChildWithTag(GameObject parent, string tag) {
        GameObject child = null;
        //Debug.Log("searching " + parent.name);
        // if (parent.transform.childCount > 0) {
        //     foreach(Transform transform in parent.transform) {
                
        //         if(transform.CompareTag(tag)) {
        //             Debug.Log(transform.name);
        //             return transform.gameObject;
        //         }
        //         Debug.Log("Working");
                
        //     }
        //     return FindChildWithTag(transform.gameObject, tag);
        // }

        // for (int i = 0; i < parent.transform.childCount; i++) {
            
        // }
    
        //dumb way


        return child;
    }

    public static Transform[] FindChildren(Transform transform, string name)
    {
        return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
    }
}
