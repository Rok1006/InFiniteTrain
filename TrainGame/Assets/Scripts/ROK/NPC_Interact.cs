using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;

public class NPC_Interact : MonoBehaviour
{
    public DialogueRunner dr;
    private GameObject Player;
    [SerializeField, BoxGroup("General")] private string PanelName;
    [SerializeField, BoxGroup("General")] private KeyCode input_interact;
    [SerializeField, BoxGroup("UI")] private GameObject InteractIcon;
    [SerializeField, BoxGroup("UI")] private GameObject TrainInfoGuide;
    [SerializeField] private Sprite smallIcon_sp; //the sprite of small icon
    [SerializeField] private Image smallIconImageObj;  
    [SerializeField] private TextMeshProUGUI guideDescriptTextObj;
    public string guideDescript;
    Animator iconAnim;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
