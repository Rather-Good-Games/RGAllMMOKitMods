using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;


namespace MultiplayerARPG
{
    public partial class ShooterPlayerControllerRG : ShooterPlayerCharacterController
    {



        public override void Activate()
        {
            // Priority Player -> Npc -> Buildings
            if (targetPlayer != null)
                CacheUISceneGameplay.SetActivePlayerCharacter(targetPlayer);
            else if (targetNpc != null)
                PlayerCharacterEntity.NpcAction.CallServerNpcActivate(targetNpc.ObjectId);
            else if (targetBuilding != null)
                ActivateBuilding_DoorLocks(targetBuilding); //New stuff RG
            else if (targetVehicle != null)
                PlayerCharacterEntity.CallServerEnterVehicle(targetVehicle.ObjectId);
            else if (targetWarpPortal != null)
                PlayerCharacterEntity.CallServerEnterWarp(targetWarpPortal.ObjectId);
            else if (targetItemsContainer != null)
                ShowItemsContainerDialog(targetItemsContainer);
        }



        /// <summary>
        /// Open doors with Keys instead of password.
        /// </summary>
        /// <param name="buildingEntity"></param>
        public  void ActivateBuilding_DoorLocks(BuildingEntity buildingEntity)
        {
            uint objectId = buildingEntity.ObjectId;
            if (buildingEntity is DoorEntity)
            {
                if (!(buildingEntity as DoorEntity).IsOpen)
                {
                    if (!buildingEntity.Lockable || !buildingEntity.IsLocked)
                    {
                        OwningCharacter.Building.CallServerOpenDoor(objectId, string.Empty);
                    }
                    else
                    {
                        //fully replaces password interface

                        DoorKeyLock dkl = buildingEntity.GetComponent<DoorKeyLock>();

                        if (dkl != null)
                        {

                            if (PlayerCharacterEntity.HasOneInNonEquipItems(dkl.key.DataId))
                            {
                                // password is the key name
                                OwningCharacter.Building.CallServerOpenDoor(objectId, dkl.key.Title);
                            }

                        }

                    }
                }
                else
                {
                    OwningCharacter.Building.CallServerCloseDoor(objectId);
                }
            }

            if (buildingEntity is StorageEntity)
            {
                if (!buildingEntity.Lockable || !buildingEntity.IsLocked)
                {
                    OwningCharacter.CallServerOpenStorage(objectId, string.Empty);
                }
                else
                {
                    //fully replaces password interface

                    DoorKeyLock dkl = buildingEntity.GetComponent<DoorKeyLock>();

                    if (dkl != null)
                    {

                        if (PlayerCharacterEntity.HasOneInNonEquipItems(dkl.key.DataId))
                        {
                            // password the key name
                            OwningCharacter.CallServerOpenStorage(objectId, dkl.key.Title);
                        }

                    }

                }
            }

            if (buildingEntity is WorkbenchEntity)
            {
                CacheUISceneGameplay.ShowWorkbenchDialog(buildingEntity as WorkbenchEntity);
            }

            if (buildingEntity is QueuedWorkbenchEntity)
            {
                CacheUISceneGameplay.ShowCraftingQueueItemsDialog(buildingEntity as QueuedWorkbenchEntity);
            }

            // Action when activate building for custom buildings
            // Can add event by `Awake` dev extension.
            if (onActivateBuilding != null)
                onActivateBuilding.Invoke(buildingEntity);
        }


    }

}
