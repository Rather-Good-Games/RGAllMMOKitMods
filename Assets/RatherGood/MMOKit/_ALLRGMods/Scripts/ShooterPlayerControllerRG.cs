using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MultiplayerARPG
{
    public class ShooterPlayerControllerRG : ShooterPlayerCharacterController
    {

        [Header("Rather Good Block")]
        [Tooltip("Enable blocking with right mouse button press.")]
        [SerializeField] bool enableRatherGoodBlock = false;

        [SerializeField] bool isBlocking = false;


        protected override void Setup(BasePlayerCharacterEntity characterEntity)
        {
            //save the value to not check on every update
            enableRatherGoodBlock = GameInstance.Singleton.enableRatherGoodBlock;
            base.Setup(characterEntity);
        }


        protected override void Update()
        {

            if (CurrentGameInstance.switchControllerModeWhenSheathed && CurrentGameInstance.enableRatherGoodSheath)
            {
                mode = (viewMode == ShooterControllerViewMode.Fps) ? ControllerMode.Combat : (PlayingCharacterEntity.IsWeaponsSheathed ? ControllerMode.Adventure : ControllerMode.Combat);
            }

            base.Update();

            //Blocking check
            if (enableRatherGoodBlock)
            {
                bool shouldBlock = InputManager.GetButton("Fire2");

                isBlocking = OwningCharacter.CallServerBlocking(!OwningCharacter.IsWeaponsSheathed && shouldBlock);
            }

        }

        //Ignores RMB for attacks if block enabled
        public override bool GetSecondaryAttackButtonUp()
        {
            return !enableRatherGoodBlock && InputManager.GetButtonUp("Fire2");
        }


        public override bool GetSecondaryAttackButton()
        {
            return !enableRatherGoodBlock && InputManager.GetButton("Fire2");
        }


        public override bool GetSecondaryAttackButtonDown()
        {
            return !enableRatherGoodBlock && InputManager.GetButtonDown("Fire2");
        }


    }
}