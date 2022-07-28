using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LiteNetLibManager;
using LiteNetLib;
using Cysharp.Threading.Tasks;

namespace MultiplayerARPG
{
    public class ItemDropEntityRG : ItemDropEntity
    {

        [Category("RG Drop Entity Settings")]

        [Tooltip("Can make drops non-kinematic. Don't recommend it, makes quite a mess. Do whatever you want though.")]
        public bool useKinematicRB = true;

        [Tooltip("If not trigger will interfere with movement. Physics for kit must be set to 'Queries Hit Triggers' if set as trigger")]
        public bool useTriggerColliders = true;

        protected Rigidbody cacheRigidbody;

        public Rigidbody CacheRigidbody
        {
            get
            {
                if (cacheRigidbody == null)
                    cacheRigidbody = GetComponent<Rigidbody>();
                return cacheRigidbody;
            }
        }

        protected override void OnItemDropDataChange(bool isInitial, ItemDropData itemDropData)
        {
            // Instantiate model at clients
            if (!IsClient)
                return;

            BaseItem item;
            if (CacheModelContainer != null && itemDropData.putOnPlaceholder &&
                GameInstance.Items.TryGetValue(itemDropData.dataId, out item) &&
                item.DropModel != null)
            {
                GameObject model = Instantiate(item.DropModel, CacheModelContainer);
                model.gameObject.SetLayerRecursively(CurrentGameInstance.itemDropLayer, true);
                model.gameObject.SetActive(true);
                //model.RemoveComponentsInChildren<Collider>(false);
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;

                CreateEncapsulatingCollider();

                CacheRigidbody.isKinematic = useKinematicRB;

                //add some rotation so items fall and not sit on end i.e. sword
                transform.rotation *= Quaternion.Euler(10f, 0, 10f);

            }
        }


        private void CreateEncapsulatingCollider()
        {

            BoxCollider thisCol;
            thisCol = GetComponent<BoxCollider>();  //this default collider
            thisCol.enabled = true;

            BoxCollider[] colliders = CacheModelContainer.GetComponentsInChildren<BoxCollider>();

            if ((colliders != null) && (colliders.Length > 0))
            {
                //Default collider
                thisCol.center = new Vector3(0, 0, 0);
                thisCol.size = new Vector3(0.5f, 0.5f, 0.5f);

                Bounds bounds = colliders[0].bounds;
                for (int i = 1; i < colliders.Length; ++i)
                {
                    bounds.Encapsulate(colliders[i].bounds);
                }

                thisCol.center = bounds.center - CacheModelContainer.position;
                thisCol.size = bounds.size;

                foreach (var oldCollider in colliders)
                {
                    oldCollider.enabled = false;
                }
            }

            thisCol.isTrigger = useTriggerColliders;

        }

    }
}
