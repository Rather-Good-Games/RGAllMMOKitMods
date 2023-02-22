using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


namespace MultiplayerARPG.GameData.Model.Playables
{
    public partial class PlayableCharacterModel_Custom : PlayableCharacterModel
    {

        [ArrayElementTitle("blockWeaponType")]
        public BlockAnimations[] blockAnimations;

        public ActionAnimation shieldBlockAnimation;

        [NonSerialized] public Dictionary<WeaponType, BlockAnimations> blockAnimationDict = new Dictionary<WeaponType, BlockAnimations>();

        [NonSerialized] private bool initializedBlockDict = false;

        Coroutine blockingActionRoutineRef = null;

        public bool StartBlock(float blockDuration = 30f)
        {
            InitBockingAnimationsDict();

            EquipWeapons tempEquipWeapons = selectableWeaponSets[equipWeaponSet];

            if (tempEquipWeapons.GetLeftHandShieldItem() != null)
            {
                blockingActionRoutineRef = (PlayActionAnimationDirectly(shieldBlockAnimation));
                return true;
            }
            else
            {
                bool isLeftHand = true; //Default blocking is left hand if available

                WeaponType weaponTypeBlock = ((BaseCharacterEntity)this.CacheEntity).GetAvailableWeapon(ref isLeftHand).GetWeaponItem().WeaponType;

                if (blockAnimationDict.TryGetValue(weaponTypeBlock, out BlockAnimations tempBlockAnimations))
                {
                    tempBlockAnimations.blockAnimation.extendDuration = blockDuration; //block should last longer, TODO: Something else?
                    blockingActionRoutineRef = (PlayActionAnimationDirectly(tempBlockAnimations.blockAnimation));
                    return true;
                }
            }

            return false;
        }

        public void CancelBlocking()
        {
            if (blockingActionRoutineRef != null)
            {
                CancelPlayingActionAnimationDirectly();
                blockingActionRoutineRef = null;
            }
        }


        public void InitBockingAnimationsDict()
        {
            if (initializedBlockDict)
                return;

            blockAnimationDict.Clear();

            if (blockAnimations != null && blockAnimations.Length > 0)
            {
                foreach (BlockAnimations ba in blockAnimations)
                {
                    if (ba.BlockWeaponType != null)
                        blockAnimationDict[ba.BlockWeaponType] = ba;
                }
            }

            initializedBlockDict = true;
        }


    }
}