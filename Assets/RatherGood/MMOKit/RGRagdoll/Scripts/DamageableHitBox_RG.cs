using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MultiplayerARPG.RGRagdoll;

namespace MultiplayerARPG
{

    [DisallowMultipleComponent]
    public class DamageableHitBox_RG : DamageableHitBox
    {
        [Tooltip("If empty will use RagdollBodyPart.ToString() instead")]
        public string overrideCombatText = "";

        public RagdollBodyPart ragdollBodyPart = RagdollBodyPart.Hips;

        RGRagdoll rgRagdoll;
        public void InitDamageableHitBox_RG(RGRagdoll rgRagdoll)
        {
            this.rgRagdoll = rgRagdoll;

            if (ragdollBodyPart == RagdollBodyPart.Shield)
            {
                if (Application.isPlaying)
                {
                    //TODO: Maybe something better than this hack?
                    enabled = false; //hide on start
                }
            }
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            if (Application.isPlaying && GameInstance.Singleton.debugRagdollCollidersInEditor)
                base.OnDrawGizmos();
        }
#endif

        public override void ReceiveDamage(Vector3 fromPosition, EntityInfo instigator, Dictionary<DamageElement, MinMaxFloat> damageAmounts, CharacterItem weapon, BaseSkill skill, short skillLevel, int randomSeed)
        {
            base.ReceiveDamage(fromPosition, instigator, damageAmounts, weapon, skill, skillLevel, randomSeed);

            if (GameInstance.Singleton.enableRatherGoodRagdoll)
            {
                if (string.IsNullOrEmpty(overrideCombatText))
                    DamageableEntity.CallAllAppendCombatTextStringRG(ragdollBodyPart.ToString());
                else
                    DamageableEntity.CallAllAppendCombatTextStringRG(overrideCombatText);
            }
        }



        void Update()
        {
            if ((rgRagdoll != null) && (rgRagdoll.isRagdoll))
            {
                defaultLocalPosition = transform.localPosition;
                defaultLocalRotation = transform.localRotation;
            }
        }



    }


}
