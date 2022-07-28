using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    public class UIDamageableEntity_RGDoorStorageInfo : UIDamageableEntity
    {

        public string textToShow;

        DoorEntity door;
        DoorKeyLock doorLock;
        protected override void UpdateUI()
        {

            base.UpdateUI();

            if (!CacheCanvas.enabled)
                return;

            if (Data is DoorEntity)
            {
                if (uiTextTitle != null)
                {
                    door = (DoorEntity)Data;

                    doorLock = door.GetComponent<DoorKeyLock>();

                    textToShow = door.Title;

                    textToShow += (door.IsLocked ? " (Locked)" : " (Unlocked)");

                    if (doorLock != null)
                    {
                        textToShow += " - Requires: " + doorLock.key.Title;
                    }

                    uiTextTitle.text = textToShow;
                }
            }
            else
            {
                door = null;
                doorLock = null;

            }

        }

        protected override void Update()
        {
            base.Update();

            if (!CacheCanvas.enabled)
                return;

            if (Data is DoorEntity && door != null && doorLock != null)
            {
                textToShow = door.Title;

                textToShow += (door.IsLocked ? " (Locked)" : " (Unlocked)");

                if (doorLock != null)
                {
                    textToShow += " - Requires: " + doorLock.key.Title;
                }

                uiTextTitle.text = textToShow;
            }



        }


    }
}