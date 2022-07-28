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

        [Category(5400, "RGSheath Stuff")]
        [SerializeField] protected SyncFieldBool isSheathed = new SyncFieldBool();

        public event System.Action<bool> onSheathChange;

        public bool IsSheathed { get { return isSheathed.Value; } set { isSheathed.SetValue(value); } }
        public bool SheathInProcess => ((PlayableCharacterModel_Custom)Model).WeaponChangeInProcess;

        PitchIKMgr_RGSheath pitchIKMgr_RGSheath; //If using PithIK mod (not required).

        [DevExtMethods("Awake")]
        protected void SetupSheath()
        {
            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            pitchIKMgr_RGSheath = GetComponent<PitchIKMgr_RGSheath>();

            onStart += SheathInit;

            onSetup += OnSetupSheathChange;
            onSetupNetElements += OnSetupElements;
        }

        [DevExtMethods("OnDestroy")]
        protected void DeSetupSheath()
        {
            onSetup -= OnDeSetupSheathChange;
            onSetupNetElements -= OnSetupElements;
            onStart -= SheathInit;
        }

        protected void OnSetupSheathChange()
        {
            isSheathed.onChange += OnSheathChange;
        }

        protected void OnSetupElements()
        {
            isSheathed.deliveryMethod = DeliveryMethod.ReliableOrdered;
            isSheathed.syncMode = LiteNetLibSyncField.SyncMode.ServerToClients;
        }

        protected void OnDeSetupSheathChange()
        {
            isSheathed.onChange -= OnSheathChange;
        }


        protected virtual void OnSheathChange(bool isInitial, bool sheath)
        {
            isRecaching = true;

            if (onSheathChange != null)
                onSheathChange.Invoke(sheath);
        }



        [DevExtMethods("OnDestroy")]
        protected void SheathOnDestroy()
        {
            onStart -= SheathInit;
            onSheathChange -= StartShiethProcess;
        }

        protected void SheathInit()
        {
            onSheathChange += StartShiethProcess; //Need to init here and not awake doesn't register
        }



        protected IEnumerator WeaponSheathOrChangeProcess(bool isInitial, byte equipWeaponSet)
        {

            if ((SelectableWeaponSets == null) || (SelectableWeaponSets.Count == 0))
                yield break;

            EquipWeapons newEquipWeapons = SelectableWeaponSets[equipWeaponSet];

            ((PlayableCharacterModel_Custom)Model).StartShiethProcess(isSheathed, newEquipWeapons);

            while (SheathInProcess)
            {
                yield return null;
            }

            //this will trigger the OnEquipWeaponSetChange event after swap for other listeners, maybe not needed?
            //OnEquipWeaponSetChange(isInitial, equipWeaponSet);

            //If using MMORPG PithIK and PitchIKMgr_RGSheath mod (not required)
            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isSheathed);

            //Need to trigger event? Change 2 Apr 22
            if (onEquipWeaponSetChange != null)
                onEquipWeaponSetChange.Invoke(equipWeaponSet);

        }


        public bool CallServerSheathWeapon(bool isSheathed)
        {
            RPC(ServerSheathWeapon, isSheathed);
            return true;
        }

        [ServerRpc]
        protected void ServerSheathWeapon(bool isSheathed)
        {
#if !CLIENT_BUILD
            if (!CurrentGameInstance.enableRatherGoodSheath)
                return;

            IsSheathed = isSheathed;
#endif
        }


        /// <summary>
        /// Sheath process does not need to trigger weapon change.
        /// </summary>
        /// <param name="isOpen"></param>
        void StartShiethProcess(bool isOpen)
        {
            ((PlayableCharacterModel_Custom)Model).StartShiethProcess(isOpen); //null equipWeaponSet will only sheith/unsheath current weapons

            if ((pitchIKMgr_RGSheath != null) && GameInstance.Singleton.disablePitchIKWhenSheathed)
                pitchIKMgr_RGSheath.UpdatePitchIKBasedOnWeaponDamageType(isSheathed);

        }



    }
}
