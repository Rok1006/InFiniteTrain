using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickUI : MonoBehaviour
{
    // Reference to the global image object
    public Image globalImage;

    // The sprite to display when this button is pressed
    public Sprite spriteToShow;

    // Called when the button is clicked
    public void OnButtonClick()
    {
        // Set the global image's sprite to the sprite for this button
        globalImage.sprite = spriteToShow;
      
    }
}
