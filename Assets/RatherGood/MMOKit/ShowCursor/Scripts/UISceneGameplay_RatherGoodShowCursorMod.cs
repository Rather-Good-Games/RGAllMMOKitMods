using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace MultiplayerARPG
{
    //Rather Good Show Cursor mod will show cursor on keypress

    public partial class UISceneGameplay : BaseUISceneGameplay
    {


        bool buttonToggleCursorShowPressed;

        /// <summary>
        /// Called from ShooterPlayerCharacterController Update()
        /// Overrides BaseUISceneGameplay IsBlockController() to add user controlled show cursor button
        /// </summary>
        /// <returns></returns>
        public override bool IsBlockController()
        {

            if (!GameInstance.Singleton.enableToggleCursorOnButtonPress)
                return base.IsBlockController();

            if (base.IsBlockController())
            {
                //Toggle cursor show off once a UI takes over.
                //This will return to normal operation as soon as the UI is closed.
                buttonToggleCursorShowPressed = false;
                return true;
            }

            if (InputManager.GetButtonDown(GameInstance.Singleton.toggleCursorButtonName))
            {
                buttonToggleCursorShowPressed = !buttonToggleCursorShowPressed;
            }

            return buttonToggleCursorShowPressed;
        }

    }
}
