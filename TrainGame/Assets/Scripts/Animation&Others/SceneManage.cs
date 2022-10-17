using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneManage : MonoBehaviour
{
    public static SceneManage Instance;
    [Header("Assignment")]
    [SerializeField] private GameObject FadeInOut;

    public List<GameObject> MCTrainConfiner = new List<GameObject>();

    void Awake() {
        Instance = this;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
