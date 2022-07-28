using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class GameInstance
    {

        [Header("Rather Good Target Generic Entities.")]
        [Tooltip("If enabled, Player can target GameEntities with InteractableEntityRG on them to display general info. Extend to add more features if desired.")]
        public bool enableTargetRGGenericEntitiesInfo = true;

    }
}