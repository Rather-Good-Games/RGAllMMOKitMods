using UnityEngine;
using LiteNetLibManager;
using LiteNetLib;
using System.Collections;

//Credit:Denarii Games for example for CombatText
//Replaces previous Ragdoll client side only text display

namespace MultiplayerARPG
{
    public partial class DamageableEntity
    {
        public void CallAllAppendCombatTextStringRG(string bodyPart)
        {
            RPC(AllAppendCombatTextStringRG, 0, DeliveryMethod.Unreliable, bodyPart);
        }

        /// <summary>
        /// This will be called on clients to display generic combat texts
        /// </summary>
        /// <param name="text"></param>
        [AllRpc]
        protected void AllAppendCombatTextStringRG(string text)
        {
            if (!IsClient || CurrentGameInstance.prefabUICombatTextRG == null) return;

            //1.75 cobat text changes
            StartCoroutine(DelayTextSpawn(combatTextTransform.position, text));

        }


        /// <summary>
        /// delay display so text doesn't overlap normal combat text
        /// </summary>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        IEnumerator DelayTextSpawn(Vector3 position, string text)
        {
            yield return new WaitForSeconds(0.25f);

            UICombatTextRG combatText = Instantiate(CurrentGameInstance.prefabUICombatTextRG);
            combatText.transform.position = position;
            combatText.Text = text;

        }

    }
}