using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using System;

namespace MultiplayerARPG
{

    /// <summary>
    /// Can drop any item on UI to quip
    /// </summary>
    public class UICharacterItemDropHandler_DropAnyRG : UICharacterItemDropHandler
    {

        public override void OnDrop(PointerEventData eventData)
        {

            // Validate drop position
            if (!RectTransformUtility.RectangleContainsScreenPoint(DropRect, Input.mousePosition))
                return;

            // Validate dragging UI
            UIDragHandler dragHandler = eventData.pointerDrag.GetComponent<UIDragHandler>();
            if (dragHandler == null || !dragHandler.CanDrop)
                return;

            // Get dragged item UI. If dragging item UI is UI for character item, equip the item
            UICharacterItemDragHandler draggedItemUI = dragHandler as UICharacterItemDragHandler;

            UICharacterItem droppedUiCharacterItem = draggedItemUI.uiCharacterItem;

            if (droppedUiCharacterItem != null)
            {
                switch (draggedItemUI.sourceLocation)
                {
                    case UICharacterItemDragHandler.SourceLocation.EquipItems:

                        break;
                    case UICharacterItemDragHandler.SourceLocation.NonEquipItems:

                        //Stolen from UICharacterItem:OnClickEquip()
                        // Only unequpped equipment can be equipped

                        if (droppedUiCharacterItem.selectionManager != null)
                            droppedUiCharacterItem.selectionManager.DeselectSelectedUI();

                        GameInstance.ClientInventoryHandlers.RequestEquipItem(
                            GameInstance.PlayingCharacter,
                            (short)droppedUiCharacterItem.IndexOfData,
                            GameInstance.PlayingCharacter.EquipWeaponSet,
                            ClientInventoryActions.ResponseEquipArmor,
                            ClientInventoryActions.ResponseEquipWeapon);

                        break;
                    case UICharacterItemDragHandler.SourceLocation.StorageItems:

                        break;
                }


            }

        }
    }
}