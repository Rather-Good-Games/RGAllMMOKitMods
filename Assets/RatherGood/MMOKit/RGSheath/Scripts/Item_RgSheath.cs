using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MultiplayerARPG
{

    public partial class Item
    {
        [Category(100, "RG SHEATH")]

        [Header("Right hand sheath models")]
        public EquipmentModel[] rightHandSheathEquipmentModels;

        public EquipmentModel[] RightHandSheathEquipmentModels
        {
            get { return rightHandSheathEquipmentModels; }
        }

        [Tooltip("Left Hand Sheath models. Also Shield")]
        public EquipmentModel[] leftHandSheathEquipmentModels;

        public EquipmentModel[] LeftHandSheathEquipmentModels
        {
            get { return leftHandSheathEquipmentModels; }
        }

        private bool alreadyMigrated = false;
        public void MigrateRGSheathModels()
        {
            if (alreadyMigrated)
                return;
            alreadyMigrated = true;
            if (RightHandSheathEquipmentModels != null && RightHandSheathEquipmentModels.Length > 0)
                SheathModels = RightHandSheathEquipmentModels;
            if (LeftHandSheathEquipmentModels != null && LeftHandSheathEquipmentModels.Length > 0)
                OffHandSheathModels = LeftHandSheathEquipmentModels;
        }
    }
}