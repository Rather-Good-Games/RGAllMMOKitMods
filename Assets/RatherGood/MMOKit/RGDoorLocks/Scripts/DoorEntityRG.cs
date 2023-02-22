using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorEntityRG : DoorEntity
    {
        [Category("Rather Good DoorEntityRG Key Locks.")]
        public Item key;

        public AudioClip clipUnlock;

        public AudioClip clipLock;

        public AudioClip clipOpen;

        public AudioClip clipClose;

        AudioSource audioSource;

        protected override void EntityStart()
        {
            base.EntityStart();

            audioSource = GetComponent<AudioSource>();

            if (key == null)
                return;

            LockPassword = key.Title;

            onInitialOpen.AddListener(PlayUnlock);
            onInitialClose.AddListener(PlayLock);
            onOpen.AddListener(PlayOpen);
            onClose.AddListener(PlayClose);

        }

        protected override void EntityOnDestroy()
        {
            base.EntityOnDestroy();
            onInitialOpen.RemoveListener(PlayUnlock);
            onInitialClose.RemoveListener(PlayLock);
            onOpen.RemoveListener(PlayOpen);
            onClose.RemoveListener(PlayClose);
        }

        public override void OnActivate()
        {
            if ((key != null) && IsLocked && !IsOpen)
            {
                if (GameInstance.PlayingCharacterEntity.HasOneInNonEquipItems(key.DataId))
                {
                    // password is the key name
                    GameInstance.PlayingCharacterEntity.Building.CallServerOpenDoor(ObjectId, LockPassword);
                }
            }
            else
            {
                base.OnActivate();
            }

        }


        //TODO: Auto close/lock after certian time for MMO
        public void PlayUnlock()
        {
            if (clipUnlock == null)
                return;
            audioSource.clip = clipUnlock;
            audioSource.Play();
        }

        public void PlayLock()
        {
            if (clipLock == null)
                return;
            audioSource.clip = clipLock;
            audioSource.Play();
        }

        public void PlayOpen()
        {
            if (clipOpen == null)
                return;
            audioSource.clip = clipOpen;
            audioSource.Play();
        }

        public void PlayClose()
        {
            if (clipClose == null)
                return;
            audioSource.clip = clipClose;
            audioSource.Play();
        }




    }
}