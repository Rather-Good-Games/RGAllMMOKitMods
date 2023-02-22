using LiteNetLib;
using LiteNetLibManager;
using MultiplayerARPG.GameData.Model.Playables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BaseCharacterEntity
    {

        //IsWeaponsSheathed + isWeaponsSheathed


        PitchIKMgr_RGSheath pitchIKMgr_RGSheath; //If using PithIK mod (not required).

        [DevExtMethods("Awake")]
        protected void SetupSheath()
        {
            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            pitchIKMgr_RGSheath = GetComponent<PitchIKMgr_RGSheath>();
            onIsWeaponsSheathedChange += RG_onIsWeaponsSheathedChange;
        }

        [DevExtMethods("OnDestroy")]
        protected void DeSetupSheath()
        {
            onIsWeaponsSheathedChange -= RG_onIsWeaponsSheathedChange;
        }

        protected void RG_onIsWeaponsSheathedChange(bool isSheathed)
        {
            if (pitchIKMgr_RGSheath != null && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isSheathed);
        }




    }
}