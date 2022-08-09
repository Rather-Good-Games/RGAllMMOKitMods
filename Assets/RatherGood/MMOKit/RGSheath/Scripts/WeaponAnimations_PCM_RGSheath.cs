using MultiplayerARPG;
using UnityEngine;

namespace MultiplayerARPG.GameData.Model.Playables
{
    [System.Serializable]
    public struct SheathAnimations : ISheathAnims
    {
        public WeaponType SheathweaponType;

        [Header("Sheath animations for weapons")]

        public ActionAnimation rightHandSheathAnimations;
        public ActionAnimation rightHandUnSheathAnimations;

        public ActionAnimation leftHandSheathAnimations;
        public ActionAnimation leftHandUnSheathAnimations;

        public ActionAnimation dualWeildSheathAnimations;
        public ActionAnimation dualWeildUnSheathAnimations;

        public WeaponType Data { get { return SheathweaponType; } }

        public static SheathAnimations DeepCopy(SheathAnimations copyMe)
        {
            SheathAnimations returnMe = new SheathAnimations();

            returnMe.SheathweaponType = copyMe.SheathweaponType;

            returnMe.rightHandSheathAnimations = DeepCopyActionAnimation(copyMe.rightHandSheathAnimations);
            returnMe.rightHandUnSheathAnimations = DeepCopyActionAnimation(copyMe.rightHandUnSheathAnimations);

            returnMe.leftHandSheathAnimations = DeepCopyActionAnimation(copyMe.leftHandSheathAnimations);
            returnMe.leftHandUnSheathAnimations = DeepCopyActionAnimation(copyMe.leftHandUnSheathAnimations);

            returnMe.dualWeildSheathAnimations = DeepCopyActionAnimation(copyMe.dualWeildSheathAnimations);
            returnMe.dualWeildUnSheathAnimations = DeepCopyActionAnimation(copyMe.dualWeildUnSheathAnimations);

            return returnMe;

        }

        public static ActionAnimation DeepCopyActionAnimation(ActionAnimation copyMe)
        {
            ActionAnimation returnMe = copyMe; //shallow

            returnMe.state = copyMe.state;

            if (copyMe.triggerDurationRates != null && copyMe.triggerDurationRates.Length > 0)
            {
                returnMe.triggerDurationRates = new float[copyMe.triggerDurationRates.Length];
                for (int i = 0; i < copyMe.triggerDurationRates.Length; i++)
                {
                    returnMe.triggerDurationRates[i] = copyMe.triggerDurationRates[i];
                }
            }

            if (copyMe.audioClips != null && copyMe.audioClips.Length > 0)
            {
                returnMe.audioClips = new AudioClip[copyMe.audioClips.Length];
                for (int i = 0; i < copyMe.audioClips.Length; i++)
                {
                    returnMe.audioClips[i] = copyMe.audioClips[i];
                }
            }

            return returnMe;

        }


    }
}