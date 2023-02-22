namespace MultiplayerARPG
{
    public partial interface IWeaponItem : IEquipmentItem
    {
        EquipmentModel[] RightHandSheathEquipmentModels { get; }

        EquipmentModel[] LeftHandSheathEquipmentModels { get; }

        void MigrateRGSheathModels();
    }
}