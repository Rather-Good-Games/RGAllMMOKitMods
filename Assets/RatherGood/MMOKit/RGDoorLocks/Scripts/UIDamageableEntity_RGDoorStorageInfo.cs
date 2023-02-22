using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    public class UIDamageableEntity_RGDoorStorageInfo : UIDamageableEntity
    {

        public string textToShow;

        protected override void UpdateUI()
        {

            base.UpdateUI();

            if (!CacheCanvas.enabled)
                return;


            textToShow = "";

            if (Data is DoorEntityRG)
            {
                if (uiTextTitle != null)
                {
                    DoorEntityRG doorRG = (DoorEntityRG)Data;

                    textToShow = doorRG.Title;

                    textToShow += (doorRG.IsLocked ? " (Locked)" : " (Unlocked)");

                    if (doorRG.key != null)
                    {
                        textToShow += " - Requires: " + doorRG.key.Title;
                    }

                    uiTextTitle.text = textToShow;
                }
            }
     


            if (Data is StorageEntityRG)
            {
                if (uiTextTitle != null)
                {
                    StorageEntityRG storageRG = (StorageEntityRG)Data;

                    textToShow = storageRG.Title;

                    textToShow += (storageRG.IsLocked ? " (Locked)" : " (Unlocked)");

                    if (storageRG.key != null)
                    {
                        textToShow += " - Requires: " + storageRG.key.Title;
                    }

                    uiTextTitle.text = textToShow;
                }
            }
    

        }

        protected override void Update()
        {
            base.Update();

            if (!CacheCanvas.enabled)
                return;

            //Data is a DoorEntityRG or StorageEntityRG
            if (!string.IsNullOrEmpty(textToShow))
            {
                uiTextTitle.text = textToShow;
            }


        }


    }
}