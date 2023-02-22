# All Rather Good Mods

**Version**: 2 (22 Feb 23)

See Release notes below. Major changes for this version for latest kit.

**Author:** RatherGood1

**Credits** 

ChosenOne, TidyDev, 23rdSilence, https://github.com/suriyun-production

**NOTE: This are unsupported mods but I will usually update when I can.**

**Example Scene Included**

* Run the 00Init_Shooter_AllModsRG to start demo.
* Map001_AllRGMods: View the yellow orbs for information about each mod in the scene. 

 **Compatibility:** 
 * Tested on Suriyun MMORPG Kit Version  1.80e (2023-02-20)  and Unity 2020.3.40f1

**Requirements**
* Suriyon MMORPG Kit
* Explosive RPG Mechanim Animation Pack

**Includes these mods:**
* RGDoorLocks
* RGBlock
* RGDamageAll
* RGItemDropEntity
* RGRagdoll
* RGSheath
* RGShowCursor
* RGEmoteChat
* RGItemDropEquipAny

Some utilities included
* TriggerDoStuffMMO
* CopyData helper (This is broken ATM)
* ResourcesFolderGameDatabase_RG
* PlayAnimationDirectly


[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=L7RYB7NRR78L6)

**RELEASE NOTES for Version**: 2 (22 Feb 23)

**EmoteChat**
* Add check to only trigger emote key press on client.

**RGBlock**
* Add enable RGBlock to GameInstance
* ShooterPlayerControllerRG
* Add right mouseDown block activation if enabled
* Overrides GetSecondaryAttackButton(Up/Down)

**RGDoorLocks**
* NEW: DoorEntityRG
* All key lock functions handled on DoorEntityRG. Previously keyDoorLock class was used.
* Need to update prefabs with new DoorEntityRG
* Delete "ShooterPlayerControllerRG_DoorLocks.cs"
* Delete "DoorKeyLock.cs"
* StorageEntityRG NOT FUNCTIONAL. Currently have not found a way to override storage access by non-owner cleanly.

**RGItemDropEntity**
* removed. Not useful

**RGGenericEntities** 
* Now uses the “IActivatableEntity” simply to display information.

**RGSheath** 
* Add trigger to new holster/draw system.
* Suri code should auto migrate RGSheath animations to new system.
