
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MultiplayerARPG
{

    /// <summary>
    /// Use this to do stuff like enable/disable game objects when player enters (triggers) collider trigger
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerDoStuffMMOKit : MonoBehaviour
    {

        public UnityEvent playerEnterEvent;

        public UnityEvent playerExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit(other.gameObject);
        }

        private void TriggerEnter(GameObject other)
        {
            // Improve performance by tags
            if (!other.CompareTag(GameInstance.Singleton.playerTag))
                return;

            DamageableHitBox hitBox = other.GetComponent<DamageableHitBox>();
            if (hitBox == null)
                return;

            if (hitBox.Entity == GameInstance.PlayingCharacterEntity)
            {
                playerEnterEvent.Invoke();
            }

        }

        private void TriggerExit(GameObject other)
        {
            // Improve performance by tags
            if (!other.CompareTag(GameInstance.Singleton.playerTag))
                return;

            DamageableHitBox hitBox = other.GetComponent<DamageableHitBox>();
            if (hitBox == null)
                return;

            if (hitBox.Entity == GameInstance.PlayingCharacterEntity)
            {
                playerExitEvent.Invoke();
            }
        }

    }
}