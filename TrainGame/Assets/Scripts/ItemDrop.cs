using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public string prefsKey; // Unique key to identify the item in PlayerPrefs

    private void Start()
    {
        // Generate a unique key for the item based on its position and rotation
        prefsKey = transform.position.ToString() + transform.rotation.ToString();
    }

    private void OnDestroy()
    {
        // Save the item's position and rotation in PlayerPrefs when it is destroyed
        PlayerPrefs.SetFloat(prefsKey + "_position_x", transform.position.x);
        PlayerPrefs.SetFloat(prefsKey + "_position_y", transform.position.y);
        PlayerPrefs.SetFloat(prefsKey + "_position_z", transform.position.z);
        PlayerPrefs.SetFloat(prefsKey + "_rotation_x", transform.rotation.x);
        PlayerPrefs.SetFloat(prefsKey + "_rotation_y", transform.rotation.y);
        PlayerPrefs.SetFloat(prefsKey + "_rotation_z", transform.rotation.z);
        PlayerPrefs.SetFloat(prefsKey + "_rotation_w", transform.rotation.w);
        PlayerPrefs.Save();
        Debug.Log("saved");
        ItemHolderWhenDeath.instance.AddData(prefsKey, this.gameObject);
        ItemHolderWhenDeath.instance.keys.Add(prefsKey);
       
        
        
    }

    public void LoadData()
    {
        // Load the item's position and rotation from PlayerPrefs when the scene starts
        Vector3 position = new Vector3(PlayerPrefs.GetFloat(prefsKey + "_position_x"), PlayerPrefs.GetFloat(prefsKey + "_position_y"), PlayerPrefs.GetFloat(prefsKey + "_position_z"));
        Quaternion rotation = new Quaternion(PlayerPrefs.GetFloat(prefsKey + "_rotation_x"), PlayerPrefs.GetFloat(prefsKey + "_rotation_y"), PlayerPrefs.GetFloat(prefsKey + "_rotation_z"), PlayerPrefs.GetFloat(prefsKey + "_rotation_w"));
        transform.position = position;
        transform.rotation = rotation;
    }
}
