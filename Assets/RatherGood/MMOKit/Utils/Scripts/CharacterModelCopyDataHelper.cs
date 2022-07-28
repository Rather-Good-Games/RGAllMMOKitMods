
using MultiplayerARPG.GameData.Model.Playables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playables = MultiplayerARPG.GameData.Model.Playables;


namespace RatherGood.MMOKitUtils
{

    /// <summary>
    /// editor class to copy animations to many CharacterModels at once
    /// </summary>
    public class CharacterModelCopyDataHelper : MonoBehaviour
    {

        [Header("Animations to copy.")]

        public Playables.DefaultAnimations defaultAnimations;

        [Tooltip("Default animations will be overrided by these animations while wielding weapon with the same type")]
        [ArrayElementTitle("weaponType")]
        public Playables.WeaponAnimations[] weaponAnimations;

        [Tooltip("Weapon animations will be overrided by these animations while wielding weapon with the same type at left-hand")]
        [ArrayElementTitle("weaponType")]
        public Playables.WieldWeaponAnimations[] leftHandWieldingWeaponAnimations;

        [ArrayElementTitle("skill")]
        public Playables.SkillAnimations[] skillAnimations;

        [ArrayElementTitle("clip")]
        public Playables.ActionState[] customAnimations;

        [ArrayElementTitle("blockWeaponType")]
        public Playables.BlockAnimations[] blockAnimations;

        public Playables.ActionAnimation shieldBlockAnimation;

        [ArrayElementTitle("SheathweaponType")]
        public Playables.SheathAnimations[] SheathAnimations;

        public Playables.ActionAnimation shieldSheithAnimation;

        public Playables.ActionAnimation shieldUnSheithAnimation;

        public float chargeDurationExtra = 100f;

        [Space]


        [InspectorButton(nameof(CopyDataToCharacters))]
        [SerializeField] bool copyDataToCharacters = false;
        public void CopyDataToCharacters()
        {
            foreach (var tgtModel in targetCharacters)
            {

                tgtModel.defaultAnimations = defaultAnimations;

                tgtModel.weaponAnimations = new Playables.WeaponAnimations[weaponAnimations.Length];
                Array.Copy(weaponAnimations, tgtModel.weaponAnimations, weaponAnimations.Length);

                tgtModel.leftHandWieldingWeaponAnimations = new Playables.WieldWeaponAnimations[leftHandWieldingWeaponAnimations.Length];
                Array.Copy(leftHandWieldingWeaponAnimations, tgtModel.leftHandWieldingWeaponAnimations, leftHandWieldingWeaponAnimations.Length);

                tgtModel.skillAnimations = new Playables.SkillAnimations[skillAnimations.Length];
                Array.Copy(skillAnimations, tgtModel.skillAnimations, skillAnimations.Length);

                tgtModel.customAnimations = new Playables.ActionState[customAnimations.Length];
                Array.Copy(customAnimations, tgtModel.customAnimations, customAnimations.Length);

                tgtModel.blockAnimations = new Playables.BlockAnimations[blockAnimations.Length];
                Array.Copy(blockAnimations, tgtModel.blockAnimations, blockAnimations.Length);

                tgtModel.shieldBlockAnimation = shieldBlockAnimation;

                tgtModel.SheathAnimations = new Playables.SheathAnimations[SheathAnimations.Length];
                Array.Copy(SheathAnimations, tgtModel.SheathAnimations, SheathAnimations.Length);

                tgtModel.shieldSheithAnimation = shieldSheithAnimation;

                tgtModel.shieldUnSheithAnimation = shieldUnSheithAnimation;

            }


        }


        [ArrayElementTitle("CacheEntityTitle")] //TODO: Array title not displaying, what am I missing here?
        public PlayableCharacterModel_Custom[] targetCharacters;



    }
}
