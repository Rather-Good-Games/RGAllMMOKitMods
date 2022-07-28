

using UnityEngine;

namespace MultiplayerARPG
{

    public partial class GameInstance
    {


        [Header("Rather Good Ragdoll")]
        [Tooltip("Rather Good Ragdoll on death.")]
        public bool enableRatherGoodRagdoll = true;

        public UICombatTextRG prefabUICombatTextRG;

        public bool debugRagdollCollidersInEditor = false;

        [Tooltip("The layer to assign ragdoll")]
        public UnityLayer ragdollLayerMask;

    }
}
