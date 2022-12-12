using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeYesButton : MonoBehaviour
{
    public void getBioCart() {
        GameObject.Find("Fake Cart Manager").GetComponent<FakeCartManager>().gotBio();
    }
}
