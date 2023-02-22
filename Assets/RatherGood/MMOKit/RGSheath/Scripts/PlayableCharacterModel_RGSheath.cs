using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLibManager;

namespace MultiplayerARPG.GameData.Model.Playables
{
    public partial class PlayableCharacterModel_Custom : PlayableCharacterModel
    {
        // socket addon
        [Header("Default material")]
        public Material defaultMaterial;

        [ArrayElementTitle("SheathweaponType")]
        public SheathAnimations[] SheathAnimations;

        public ActionAnimation shieldSheithAnimation;

        public ActionAnimation shieldUnSheithAnimation;

        [DevExtMethods("RG_Awake")]
        protected void Awake_Sheath()
        {
            MigrateSheathAnimations();
        }

        public void MigrateSheathAnimations()
        {
            Debug.Log($"Migrating sheath animations {this}");
            Dictionary<WeaponType, WeaponAnimations> newAnimDict = new Dictionary<WeaponType, WeaponAnimations>();
            for (int i = 0; i < weaponAnimations.Length; ++i)
            {
                if (weaponAnimations[i].weaponType == null)
                    continue;
                newAnimDict[weaponAnimations[i].weaponType] = weaponAnimations[i];
            }
            for (int i = 0; i < SheathAnimations.Length; ++i)
            {
                if (SheathAnimations[i].SheathweaponType == null)
                    continue;
                if (newAnimDict.TryGetValue(SheathAnimations[i].SheathweaponType, out WeaponAnimations anims))
                {
                    HolsterAnimation holsterAnimation;
                    // R
                    holsterAnimation = anims.rightHandHolsterAnimation;
                    holsterAnimation.holsterState = SheathAnimations[i].rightHandSheathAnimations.state;
                    holsterAnimation.holsteredDurationRate = SheathAnimations[i].rightHandSheathAnimations.triggerDurationRates != null && SheathAnimations[i].rightHandSheathAnimations.triggerDurationRates.Length > 0 ? SheathAnimations[i].rightHandSheathAnimations.triggerDurationRates[0] : 1f;
                    holsterAnimation.drawState = SheathAnimations[i].rightHandUnSheathAnimations.state;
                    anims.rightHandHolsterAnimation = holsterAnimation;
                    // L
                    holsterAnimation = anims.leftHandHolsterAnimation;
                    holsterAnimation.holsterState = SheathAnimations[i].leftHandSheathAnimations.state;
                    holsterAnimation.holsteredDurationRate = SheathAnimations[i].leftHandSheathAnimations.triggerDurationRates != null && SheathAnimations[i].leftHandSheathAnimations.triggerDurationRates.Length > 0 ? SheathAnimations[i].leftHandSheathAnimations.triggerDurationRates[0] : 1f;
                    holsterAnimation.drawState = SheathAnimations[i].leftHandUnSheathAnimations.state;
                    anims.leftHandHolsterAnimation = holsterAnimation;
                    newAnimDict[anims.weaponType] = anims;
                }
            }
            weaponAnimations = new List<WeaponAnimations>(newAnimDict.Values).ToArray();
            CacheAnimationsManager.SetCacheAnimations(Id, weaponAnimations, skillAnimations);
        }

        public override void SetEquipWeapons(IList<EquipWeapons> selectableWeaponSets, byte equipWeaponSet, bool isWeaponsSheated)
        {
            // Migrate weapon data
            if (selectableWeaponSets != null)
            {
                foreach (var weaponSet in selectableWeaponSets)
                {
                    IWeaponItem weaponItem = weaponSet.GetRightHandWeaponItem();
                    if (weaponItem != null)
                    {
                        weaponItem.MigrateRGSheathModels();
                    }
                }
            }
            base.SetEquipWeapons(selectableWeaponSets, equipWeaponSet, isWeaponsSheated);
        }

        public static void SetGlobalScale(Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }

        /*public void AddWeaponEfx(CharacterItem characterItem, EquipmentModel[] equipmentModels)
        {
            if (equipmentModels == null || equipmentModels.Length == 0)
                return;

            EquipmentContainer tempContainer;
            EquipmentModel tempEquipmentModel;

            for (int i = 0; i < equipmentModels.Length; ++i)
            {
                tempEquipmentModel = equipmentModels[i];
                if (string.IsNullOrEmpty(tempEquipmentModel.equipSocket))
                    continue;
                if (!CacheEquipmentModelContainers.TryGetValue(tempEquipmentModel.equipSocket, out tempContainer))
                    continue;

                BaseItem tempEnhancer;
                SocketEnhancerItem socketEnhancerItem = null;

                foreach (int socketId in characterItem.sockets)
                {
                    if (GameInstance.Items.TryGetValue(socketId, out tempEnhancer))
                        socketEnhancerItem = (tempEnhancer as SocketEnhancerItem);
                    else
                        return;

                    if (socketEnhancerItem != null)
                        foreach (Transform mesh in tempContainer.transform)
                        {
                            if (socketEnhancerItem.particleEfx != null)
                            {
                                ParticleSystem particleSystem = Instantiate(socketEnhancerItem.particleEfx, mesh.transform);
                                ParticleSystem.ShapeModule shape = particleSystem.shape;
                                shape.meshRenderer = mesh.gameObject.GetComponent<MeshRenderer>();
                            }
                        }
                }

            }

        }*/

        /*public void AddshaderEffects(CharacterItem equipItem)
        {
            if (equipItem.GetArmorItem().EquipmentModels == null || equipItem.GetArmorItem().EquipmentModels.Length == 0)
                return;

            EquipmentModel[] tempEquipmentModels = equipItem.GetArmorItem().EquipmentModels;
            Dictionary<string, GameObject> tempCreatingModels = new Dictionary<string, GameObject>();
            EquipmentContainer tempContainer;
            EquipmentModel tempEquipmentModel;

            for (int i = 0; i < equipItem.GetArmorItem().EquipmentModels.Length; ++i)
            {
                tempEquipmentModel = tempEquipmentModels[i];
                if (string.IsNullOrEmpty(tempEquipmentModel.equipSocket))
                    continue;
                if (!CacheEquipmentModelContainers.TryGetValue(tempEquipmentModel.equipSocket, out tempContainer))
                    continue;


                setColors(equipItem, tempContainer);


                BaseItem tempEnhancer;
                SocketEnhancerItem socketEnhancerItem = null;
                if (equipItem.sockets.Count > 0)
                    foreach (int socketId in equipItem.sockets)
                    {
                        if (GameInstance.Items.TryGetValue(socketId, out tempEnhancer))
                            socketEnhancerItem = (tempEnhancer as SocketEnhancerItem);
                        else
                            return;

                        Material material;
                        foreach (Transform skinnedMesh in tempContainer.transform)
                        {
                            if (skinnedMesh.GetComponent<Renderer>() != null)
                            {
                                material = skinnedMesh.GetComponent<Renderer>().material;
                                Material tempMaterial = Instantiate(material);
                                foreach (ShaderColor shaderColor in socketEnhancerItem.ShaderTextureColors)
                                {
                                    tempMaterial.SetColor(shaderColor.textureName, shaderColor.color);

                                }
                                skinnedMesh.GetComponent<Renderer>().material = tempMaterial;
                            }


                        }
                    }

                if (equipItem.sockets.Count == 0)
                    for (int j = 0; j < equipItem.GetArmorItem().EquipmentModels.Length; ++j)
                    {
                        tempEquipmentModel = tempEquipmentModels[j];
                        if (string.IsNullOrEmpty(tempEquipmentModel.equipSocket))
                            continue;
                        if (!CacheEquipmentModelContainers.TryGetValue(tempEquipmentModel.equipSocket, out tempContainer))
                            continue;
                        foreach (Transform skinnedMesh in tempContainer.transform)
                        {
                            if (skinnedMesh.GetComponent<SkinnedMeshRenderer>() != null)
                            {
                                skinnedMesh.GetComponent<SkinnedMeshRenderer>().material = defaultMaterial;
                            }


                        }
                        setColors(equipItem, tempContainer);
                    }


            }
        }*/

        /*void setColors(CharacterItem equipItem, EquipmentContainer tempContainer)
        {
            if ((equipItem.GetItem() as ArmorItem) != null)
            {

                Material material;
                foreach (Transform skinnedMesh in tempContainer.transform)
                {
                    if (skinnedMesh.GetComponent<Renderer>() != null)
                    {
                        material = skinnedMesh.GetComponent<Renderer>().material;
                        Material tempMaterial = Instantiate(material);
                        foreach (ShaderColor shaderColor in (equipItem.GetItem() as ArmorItem).ShaderTextureColors)
                        {
                            tempMaterial.SetColor(shaderColor.textureName, shaderColor.color);

                        }
                        skinnedMesh.GetComponent<Renderer>().material = tempMaterial;
                    }


                }
            }
        }*/
    }
}