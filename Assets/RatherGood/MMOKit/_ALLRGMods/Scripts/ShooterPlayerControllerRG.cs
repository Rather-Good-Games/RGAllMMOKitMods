using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{

    //RGSheath mod leace combat mode when sheathed

    public partial class ShooterPlayerControllerRG : ShooterPlayerCharacterController
    {

        [Tooltip("RMB now activates block instead of secondary attack.")]
        [SerializeField] bool isBlocking;

        protected override void Update()
        {

            if (CurrentGameInstance.switchControllerModeWhenSheathed && CurrentGameInstance.enableRatherGoodSheath)
            {
                mode = (viewMode == ShooterControllerViewMode.Fps) ? ControllerMode.Combat : ((PlayerCharacterEntity.IsSheathed) ? ControllerMode.Adventure : ControllerMode.Combat);
            }

            isBlocking = InputManager.GetButton("Fire2");

            PlayerCharacterEntity.CallServerBlocking(!PlayerCharacterEntity.IsSheathed && isBlocking);

            base.Update();
        }

        /// <summary>
        /// Override me for block instead of secondary attack.
        /// </summary>
        /// <returns></returns>
        public override bool GetSecondaryAttackButton()
        {
            return false; // InputManager.GetButton("Fire2");
        }


    }
}