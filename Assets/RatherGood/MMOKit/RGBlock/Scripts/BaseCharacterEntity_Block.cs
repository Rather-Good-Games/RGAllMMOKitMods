
using MultiplayerARPG.GameData.Model.Playables;
using System.Collections;
using UnityEngine;

using LiteNetLib;
using LiteNetLibManager;
using System.Collections.Generic;


namespace MultiplayerARPG
{
    public partial class BaseCharacterEntity
    {

        [Category(5600, "RGBlock Stuff")]
        [SerializeField] protected SyncFieldBool isBlocking = new SyncFieldBool();

        public event System.Action<bool> onBlockChange;


        public bool IsBlocking { get { return isBlocking.Value; } set { isBlocking.SetValue(value); } }

        [DevExtMethods("Awake")]
        protected void SetupBlocking()
        {
            onStart += BlockInit;
            onSetup += OnSetupBlockChange;
            onSetupNetElements += OnSetupBlockElements;
        }

        protected void BlockInit()
        {
            onBlockChange += StartBlockProcess; //Need to init here and not awake doesn't register
        }

        [DevExtMethods("OnDestroy")]
        protected void DeSetupBlock()
        {
            onSetup -= OnDeSetupBlockChange;
            onSetupNetElements -= OnSetupBlockElements;
        }

        protected void OnSetupBlockElements()
        {
            isBlocking.deliveryMethod = DeliveryMethod.ReliableOrdered;
            isBlocking.syncMode = LiteNetLibSyncField.SyncMode.ServerToClients;
        }

        protected void OnDeSetupBlockChange()
        {
            isBlocking.onChange -= OnBlockChange;
        }

        protected void OnSetupBlockChange()
        {
            isBlocking.onChange += OnBlockChange;
        }

        protected virtual void OnBlockChange(bool isInitial, bool isBlocking)
        {
            isRecaching = true;

            if (onBlockChange != null)
                onBlockChange.Invoke(isBlocking);
        }

        /// <summary>
        /// Call this to initiate or cancel blocking.
        /// </summary>
        /// <param name="isBlocking"></param>
        /// <returns></returns>
        public bool CallServerBlocking(bool isBlocking)
        {
            if (isBlocking == IsBlocking) //only change isf needed
                return IsBlocking;

            if (!BlockStaminaCheck()) //TODO
                return IsBlocking;

            RPC(ServerSetBlocking, isBlocking);
            return isBlocking;
        }

        [ServerRpc]
        protected void ServerSetBlocking(bool isBlocking)
        {
#if !CLIENT_BUILD
            IsBlocking = isBlocking;
#endif
        }

        private void StartBlockProcess(bool isOpen)
        {
            if (isBlocking)
            {
                ((PlayableCharacterModel_Custom)Model).StartBlock();
            }
            else
            {
                ((PlayableCharacterModel_Custom)Model).CancelBlocking();
            }
        }




        //TODO
        private bool BlockStaminaCheck()
        {
            if (!IsServer) return false;

            if (IsRecaching) return false;

            if (!CanDoActions()) return false;

            //TODO: Add some stuff to check or adjsut stamina

            return true;
        }


    }
}
