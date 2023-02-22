using UnityEngine;

namespace MultiplayerARPG
{

    //only works with PlayableCharacterModel
    public partial class PlayerCharacterEntity
    {

        [DevExtMethods("Awake")]
        protected void PlayerSheathAwake()
        {

            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            /*if (PlayerPrefs.GetInt("isSheathed") == 1)
            {
                IsSheathed = true;
                Cursor.SetCursor(CurrentGameInstance.cursorPeaceMode, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                IsSheathed = false;
                Cursor.SetCursor(CurrentGameInstance.cursorBattleMode, Vector2.zero, CursorMode.Auto);
            }*/

            onUpdate += PlayerSheathOnUpdate;

        }

        [DevExtMethods("OnDestroy")]
        protected void PlayerSheathOnDestroy()
        {
            onUpdate -= PlayerSheathOnUpdate;

        }

        /*public override bool CanUseItem()
        {
            if (isSheathed)
            {
                BaseGameNetworkManager.Singleton.SendNotifyCustomMessage(this.ConnectionId, "You cant do that while Sheathed", Color.red);
                return false;
            }
            return base.CanUseItem();
        }*/

        protected void PlayerSheathOnUpdate()
        {
            if (!IsOwnerClient || !CurrentGameInstance.enableRatherGoodSheath)
                return;

            if (InputManager.GetButtonDown(CurrentGameInstance.sheathButtonName))
            {
                IsWeaponsSheathed = !IsWeaponsSheathed;
            }
        }
    }
}


