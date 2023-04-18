using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolderWhenDeath : MonoBehaviour
{
    public static ItemHolderWhenDeath instance;

    private Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
    public List<string> keys = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent between scenes
        }
        else
        {
            Destroy(gameObject); // If another DataManager object exists, destroy this one
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddData(string key, GameObject value)
    {
        // Add or update a key-value pair in the dictionary
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }
    public void SpawnItems()
    {

        for(int i = 0; i < keys.Count; i++)
        {
            var item = Instantiate(dictionary[keys[i]]) as GameObject;
            item.GetComponent<ItemDrop>().prefsKey = keys[i];
            item.GetComponent<ItemDrop>().LoadData();

        }
       
    }

}
