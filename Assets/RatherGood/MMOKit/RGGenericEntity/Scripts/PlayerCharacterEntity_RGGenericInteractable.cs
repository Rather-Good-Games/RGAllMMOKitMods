using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class PlayerCharacterEntity
    {

        [DevExtMethods("Awake")]
        protected void Awake_RGGenericEntity()
        {
            if (!CurrentGameInstance.enableTargetRGGenericEntitiesInfo)
                return;

            onStart += Start_RGGenericEntity;
        }

        void Start_RGGenericEntity()
        {
            onUpdate += Update_RGGenericEntity;
        }

        [DevExtMethods("OnDestroy")]
        protected void OnDestroy_RGGenericEntity()
        {
            if (!CurrentGameInstance.enableTargetRGGenericEntitiesInfo)
                return;

            if (GameInstance.PlayingCharacterEntity == null || (GameInstance.PlayingCharacterEntity != this))
                return;

            onUpdate -= Update_RGGenericEntity;
        }

        //Update player target for generic interactable for info
        void Update_RGGenericEntity()
        {

            if (GameInstance.PlayingCharacterEntity == null || (GameInstance.PlayingCharacterEntity != this))
                return;

            BaseGameEntity bge = BasePlayerCharacterController.Singleton.SelectedEntity;

            if ((bge != null) && (bge is InteractableEntityRG))
            {
                UISceneGameplay sgp = (UISceneGameplay)BaseUISceneGameplay.Singleton;

                sgp.uiTargetItemDrop.Data = bge;
                sgp.uiTargetItemDrop.Show();
            }
        }


    }
}