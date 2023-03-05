using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class GUIManagerPlus : GUIManager
{
    public override void SetPauseScreen(bool state)
    {
        if (!Info.Instance.IsViewingInventory) {
            Debug.Log("esacpe");
            base.SetPauseScreen(state);
        }
    }
}
