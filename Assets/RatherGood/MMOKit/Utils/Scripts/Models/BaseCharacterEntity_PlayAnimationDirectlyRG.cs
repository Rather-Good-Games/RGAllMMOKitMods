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

        //Wrapper to access PlayActionAnimationDirectly using BaseCharacterEntity instead of getting a model reference and user knowing what model is used

        //alt(new prefered) if using PlayableCharacterModel 
        private GameData.Model.Playables.PlayableCharacterModel_Custom playableCharacterModel_ForPlayingAnimationDirectly;

        //Old(legacy?)
        private AnimatorCharacterModel animatorCharacterModel_ForPlayingAnimationDirectly;  //local reference


        [DevExtMethods("Awake")]
        protected void Awake_PlayActionAnimationDirectly()
        {
            onStart += PlayActionAnimationDirectlyInit;
        }

        void PlayActionAnimationDirectlyInit()
        {

            if (Model is PlayableCharacterModel_Custom)
            {
                playableCharacterModel_ForPlayingAnimationDirectly = ((PlayableCharacterModel_Custom)Model);
            }
            else if (Model is AnimatorCharacterModel)
            {
                animatorCharacterModel_ForPlayingAnimationDirectly = ((AnimatorCharacterModel)Model);
            }
            else
            {
                Debug.LogError("Unsupported model for PlayActionAnimationDirectly!");
            }

        }


        [DevExtMethods("OnDestroy")]
        protected void OnDestroy_PlayActionAnimationDirectly()
        {
            onStart -= PlayActionAnimationDirectlyInit;
        }


        public Coroutine PlayActionAnimationDirectly(MultiplayerARPG.ActionAnimation actionAnimation, AvatarMask avatarMask = null)
        {

            if (playableCharacterModel_ForPlayingAnimationDirectly != null)
            {
                return playableCharacterModel_ForPlayingAnimationDirectly.PlayActionAnimationDirectly(actionAnimation, avatarMask);
            }
            else if (animatorCharacterModel_ForPlayingAnimationDirectly != null)
            {
                return animatorCharacterModel_ForPlayingAnimationDirectly.PlayActionAnimationDirectly(actionAnimation);
            }
            else
            {
                Debug.LogError("Unsupported model for PlayActionAnimationDirectly!");
                return null;
            }

        }

        public Coroutine PlayActionAnimationDirectly(MultiplayerARPG.GameData.Model.Playables.ActionAnimation actionAnimation)
        {
            return ((PlayableCharacterModel_Custom)Model).PlayActionAnimationDirectly(actionAnimation);
        }

        public void CancelPlayingActionAnimationDirectly(bool stopActionAnimationIfPlaying = true)
        {

            if (playableCharacterModel_ForPlayingAnimationDirectly != null)
            {
                playableCharacterModel_ForPlayingAnimationDirectly.CancelPlayingActionAnimationDirectly();
            }
            else if (animatorCharacterModel_ForPlayingAnimationDirectly != null)
            {
                animatorCharacterModel_ForPlayingAnimationDirectly.CancelPlayingActionAnimationDirectly(stopActionAnimationIfPlaying);
            }
            else
            {
                Debug.LogError("Unsupported model for PlayActionAnimationDirectly!");
            }

        }

        public bool IsDoingActionRG()
        {

            if (playableCharacterModel_ForPlayingAnimationDirectly != null)
            {
                return playableCharacterModel_ForPlayingAnimationDirectly.IsDoingActionRG();
            }
            else if (animatorCharacterModel_ForPlayingAnimationDirectly != null)
            {
                return animatorCharacterModel_ForPlayingAnimationDirectly.IsDoingActionRG();
            }
            else
            {
                Debug.LogError("Unsupported model for PlayActionAnimationDirectly!");
                return false;
            }

        }


    }
}