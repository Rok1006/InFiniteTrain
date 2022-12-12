using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class FakeCartManager : MMPersistentSingleton<FakeCartManager>
{
    [SerializeField] private GameObject bioCart, cartDoor;
    [SerializeField] private Teleporter leftTel, rightTel;
    [SerializeField] private Room targetRoom;
    private int enemyCount = 0;
    private bool gotBioCart = false, hasSetEnemyCount = false;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    
    void Update()
    {
        switch (SceneManager.GetActiveScene().name) {
            case "LeoPlayAround":
                bioCart = GameObject.Find("Fake Bio Cart");
                cartDoor = GameObject.Find("Trains_Train_Mid_window2 (To the Bio Cart)");
                leftTel = GameObject.Find("Left Tel").GetComponent<Teleporter>();
                rightTel = GameObject.Find("Right Tel").GetComponent<Teleporter>();
                targetRoom = GameObject.Find("Target Room").GetComponent<Room>();
                bioCart.SetActive(true);
                if (gotBioCart) {
                    bioCart.SetActive(true);
                    cartDoor.GetComponent<MeshRenderer>().enabled = true;
                    leftTel.Destination = rightTel;
                    leftTel.TargetRoom = targetRoom;
                }
                break;
            case "Map Test":
                enemyCount = FindObjectsOfType<AIBrain>().Length;
                if (enemyCount <= 0) {
                    gotBioCart = true;
                    SceneManager.LoadScene("LeoPlayAround");
                }
                break;
        }
    }
}
