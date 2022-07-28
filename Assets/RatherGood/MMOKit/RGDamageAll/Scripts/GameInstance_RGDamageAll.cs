using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class GameInstance
    {

        [Header("Rather Good Damage All")]
        [Tooltip("If enabled you will damage yourself or your allies in combat if your aim sucks.")]
        public bool allowDamageToSelfAndAlliesFromAllSources = false;


    }
}