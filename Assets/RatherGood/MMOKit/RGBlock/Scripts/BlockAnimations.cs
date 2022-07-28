
using MultiplayerARPG;
using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{

    [System.Serializable]
    public struct BlockAnimations
    {
        public WeaponType blockWeaponType;

        public ActionAnimation blockAnimation;

        public WeaponType BlockWeaponType { get { return blockWeaponType; } }

    }
}