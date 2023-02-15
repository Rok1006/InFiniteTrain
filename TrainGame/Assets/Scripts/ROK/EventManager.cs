using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using NaughtyAttributes;
//Player Related event manager
public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject SoundObject;
    [SerializeField, BoxGroup("Slash")] private GameObject SlashObject;
    [SerializeField, BoxGroup("Slash")]private GameObject SlashCenterPt;
    private GameObject Player;
    private CharacterOrientation2D ChOri_2D;
    //public AudioSource[] footSteps;
    public List<AudioSource> footSteps = new List<AudioSource>();
    
    void Start()
    {
        AudioSource[] footSound = SoundObject.GetComponents<AudioSource>();
        //footSteps[0] = footSound[0];
        footSteps.Add(footSound[0]);
        footSteps.Add(footSound[1]);
        SlashObject.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        ChOri_2D = Player.gameObject.GetComponent<CharacterOrientation2D>();
        //footSteps[1] = footSound[1];
        //Debug.Log(footSound[0]==null);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.W)){
            SlashObject.transform.localScale = new Vector3(3.37994f,3.37994f,3.37994f); 
        }
    }
    public void PlayeFootStep(){
        int num = Random.Range(0,2);
        footSteps[num].Play();
        //Debug.Log("num=" + num);
    }
    public void smallSlash1(){
        SlashObject.SetActive(false);
        if(ChOri_2D.IsRight){
           SlashObject.transform.localScale = new Vector3(3.37994f,3.37994f,3.37994f); 
        }else{
            SlashObject.transform.localScale = new Vector3(-3.37994f,3.37994f,3.37994f); 
        }
        
        SlashObject.transform.position = SlashCenterPt.transform.position;
        SlashObject.SetActive(true);
        //Invoke("SlashOff",0.1f);
    }
    void SlashOff(){
        SlashObject.SetActive(false);
    }
}
