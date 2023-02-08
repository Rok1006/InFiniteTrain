﻿using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using UnityEngine;

namespace InventoryDoubleClick
{
    public class InventoryDoubleClick : MonoBehaviour, MMEventListener<MMInventoryEvent>
    {
        private InventorySlot _slot;
        private const float _doubleClickMaxDelay = .5f;
        private bool _clicked;
        private float _clickedTime;
        private bool DoubleClick => _clicked && Time.time - _clickedTime < _doubleClickMaxDelay;
        
        public void OnMMEvent(MMInventoryEvent inventoryEvent)
        {
            if (inventoryEvent.InventoryEventType != MMInventoryEventType.Click) return;
            if (!DoubleClick)
            {
                _clicked = true;
                _clickedTime = Time.time;
                _slot = inventoryEvent.Slot;
            }
            else
            {
                _clicked = false;
                if (inventoryEvent.Slot != _slot) return;

                //move the stuff to the fist avaliable inventory slot 
                InventoryDisplay targetInventoryDisplay = _slot.ParentInventoryDisplay.NextInventory;
                if (_slot.Movable() && targetInventoryDisplay != null)  {
                    Inventory currentInventory = _slot.ParentInventoryDisplay.TargetInventory;
                    Inventory targetInventory = targetInventoryDisplay.TargetInventory;
                    for (int i = 0; i < targetInventory.Content.Length; i++) {
                        if (targetInventory.Content[i] == null) {
                            currentInventory.MoveItemToInventory(_slot.Index, targetInventory);
                            return;
                        }
                    }
                }


                if (_slot.Unequippable()) _slot.UnEquip();
                else if (_slot.Equippable()) _slot.Equip();
                if (_slot.Usable()) _slot.Use();
            }
        }

        private void OnEnable()
        {
            this.MMEventStartListening();
        }
        
        private void OnDisable()
        {
            this.MMEventStopListening();
        }
    }
}
