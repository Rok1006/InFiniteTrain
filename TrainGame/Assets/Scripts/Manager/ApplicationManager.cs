using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
