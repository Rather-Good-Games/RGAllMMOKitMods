using LiteNetLibManager;
using MultiplayerARPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{

    /// <summary>
    /// Place this on something you want the player to look at and will display info about. 
    /// </summary>
    public class InteractableEntityRG : BaseGameEntity, IActivatableEntity
    {
        [Category("RGStuff")]
        [TextArea(5,10)]
        [SerializeField] string infoToShow;


        [SerializeField] bool _updateEntityComponents = false;
        protected override bool UpdateEntityComponents { get { return _updateEntityComponents; } }


        protected override void EntityAwake()
        {
            base.EntityAwake();
            gameObject.tag = GameInstance.Singleton.itemDropTag;
            gameObject.layer = GameInstance.Singleton.itemDropLayer;

            Title = infoToShow;
        }

        public bool CanActivate()
        {
            return false;
        }

        public float GetActivatableDistance()
        {
            return 5f;
        }

        public void OnActivate()
        {
            
        }

        public bool ShouldBeAttackTarget()
        {
            return false;
        }

        public bool ShouldClearTargetAfterActivated()
        {
            return false;
        }

        public bool ShouldNotActivateAfterFollowed()
        {
            return false;
        }




    }


}