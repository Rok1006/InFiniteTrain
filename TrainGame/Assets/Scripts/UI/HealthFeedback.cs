using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class HealthFeedback : MonoBehaviour
{
    private HealthUI healthUI;
    void Start()
    {
        healthUI = FindObjectOfType<HealthUI>();
    }

    void Update()
    {
        
    }

    public void sendHealthInfo() {
        if (healthUI == null)
            Debug.Log("cant find healthUI");
        else {
            healthUI.setHeart((int)GetComponentInParent<Health>().CurrentHealth);
        }
    }
}
