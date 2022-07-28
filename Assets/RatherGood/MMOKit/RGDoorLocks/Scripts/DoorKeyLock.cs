using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    [RequireComponent(typeof(AudioSource))]
    /// <summary>
    /// Use for door or storage to unlock by item Title as password
    /// </summary>
    public class DoorKeyLock : MonoBehaviour
    {

        public Item key;

        public AudioClip clipUnlock;

        public AudioClip clipLock;

        public AudioClip clipOpen;

        public AudioClip clipClose;

        AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (key == null)
                return;

            DoorEntity door = GetComponent<DoorEntity>();
            if (door != null)
                door.LockPassword = key.Title;

            StorageEntity storage = GetComponent<StorageEntity>();
            if (storage != null)
                storage.LockPassword = key.Title;
        }


        //TODO: Auto close/lock after certian time for MMO
        public void PlayUnlock()
        {
            audioSource.clip = clipUnlock;
            audioSource.Play();
        }

        public void PlayLock()
        {
            audioSource.clip = clipLock;
            audioSource.Play();
        }

        public void PlayOpen()
        {
            audioSource.clip = clipOpen;
            audioSource.Play();
        }

        public void PlayClose()
        {
            audioSource.clip = clipClose;
            audioSource.Play();
        }

    }
}