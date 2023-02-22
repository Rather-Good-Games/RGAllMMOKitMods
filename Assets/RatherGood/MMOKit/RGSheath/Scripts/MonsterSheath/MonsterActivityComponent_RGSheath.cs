using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public class MonsterActivityComponent_RGSheath : MonsterActivityComponent
    {

        [Header("RGModTest")]

        public bool isSheithedLocal;

        public Item RightHandItem;

        [Tooltip("Items that will be added to monster on spawn. (If using bow need to add ammo!)")]
        [ArrayElementTitle("item")]
        public ItemAmount[] startItems;


        public override void EntityStart()
        {

            //adjust some stuff
            maxDistanceFromSpawnPoint = 50f;
            followTargetDuration = 60f;


            EquipWeapons equipWeapons = new EquipWeapons();
            equipWeapons.rightHand = CharacterItem.Create(RightHandItem);
            Entity.EquipWeapons = equipWeapons;
            Entity.SelectableWeaponSets[0] = equipWeapons;

            if ((startItems != null) && (startItems.Length > 0))
            {
                //Copy from PlayerCharacterDataExtension
                foreach (ItemAmount startItem in startItems)
                {
                    if (startItem.item == null || startItem.amount <= 0)
                        continue;

                    short amount = (short)startItem.amount;

                    while (amount > 0)
                    {
                        short addAmount = amount;
                        if (addAmount > startItem.item.MaxStack)
                            addAmount = (short)startItem.item.MaxStack;
                        if (!Entity.IncreasingItemsWillOverwhelming(startItem.item.DataId, addAmount))
                            Entity.AddOrSetNonEquipItems(CharacterItem.Create(startItem.item, 1, addAmount));
                        amount -= addAmount;
                    }
                }
            }

        }


        public override void EntityUpdate()
        {
            base.EntityUpdate();

            isSheithedLocal = startedFollowEnemy;

            if (isSheithedLocal != Entity.IsWeaponsSheathed)
            {
                Entity.IsWeaponsSheathed = isSheithedLocal;
            }
        }

    }

}