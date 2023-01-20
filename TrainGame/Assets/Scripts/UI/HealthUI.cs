using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField, InfoBox("order them from left to right")] private List<GameObject> hearts;
    

    public void setHeart(int currentHealth) {
        if (hearts.Count <= 0) {return;}
        if (!hearts[0].activeSelf) {return;}
        if (currentHealth <= 0) {
            foreach (GameObject heart in hearts)
                heart.SetActive(false);
        }
        else {
            for (int i = 1; i <= hearts.Count; i++) {
                if (i > Mathf.Max(currentHealth, 0))
                    hearts[i-1].SetActive(false);
                else
                    hearts[i-1].SetActive(true);
            }
        }
    }

    public void DecreaseHeart(int numToDecrease) {
        if (hearts.Count <= 0) {return;}
        if (!hearts[0].activeSelf) {return;}
        if (hearts.Count < numToDecrease) {
            foreach (GameObject heart in hearts)
                heart.SetActive(false);
        }
        else {
            int startIndex = hearts.Count-1;
            while (!hearts[startIndex].activeSelf) {
                startIndex--;
            }
            while (numToDecrease > 0) {
                hearts[startIndex].SetActive(false);
                startIndex -= Mathf.Max(startIndex-1, 0);
            }
        }
    }
}
